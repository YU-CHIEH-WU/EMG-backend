using AutoMapper;
using EMG.Interface;
using EMG.Model;
using EMG.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    public class ActImgController : Controller
    {
        ActImgService actimgService = new ActImgService();

        #region 首頁
        public ActionResult Index(string Sort)
        {
            //if (Title == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            ActImgView Data = new ActImgView();
            Data.actImgList = actimgService.GetDataList();
            Data.Sort = Sort;
            return View(Data);
        }
        #endregion

        #region 建立相簿
        public ActionResult CreateAlbum(string Sort)
        {
            ActImgView Data = new ActImgView();
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "請選擇分類", Value = "", Selected = true });
            list.Add(new SelectListItem() { Text = "背肌", Value = "背肌" });
            list.Add(new SelectListItem() { Text = "胸肌", Value = "胸肌" });
            list.Add(new SelectListItem() { Text = "腿部", Value = "腿部" });
            list.Add(new SelectListItem() { Text = "腹肌", Value = "腹肌" });
            list.Add(new SelectListItem() { Text = "肩部", Value = "肩部" });
            list.Add(new SelectListItem() { Text = "肱二頭肌", Value = "肱二頭肌" });
            list.Add(new SelectListItem() { Text = "肱三頭肌", Value = "肱三頭肌" });
            ViewBag.Sort = list;          
            return View(Data);
        }
        [HttpPost]
        public ActionResult CreateAlbum(ActImgView Data, string account)
        {
            if (Data.upload != null)
            {
                string Url = Path.Combine(Server.MapPath("~/FileUpload"), Data.upload.FileName);
                Data.account = User.Identity.Name;
                Data.upload.SaveAs(Url);
                actimgService.Insert(Data, account);
            }
            return RedirectToAction("Index", "ActImg");
        }
        #endregion

        #region 刪除相簿
        public ActionResult DeleteAlbum(string Sort, string Title)
        {
            actimgService.DeleteAlbum(Sort);
            return RedirectToAction("Index", "ActImg", new { Title = Title });
        }
        #endregion

        #region 上傳相片
        public ActionResult Upload(string Sort)
        {
            ImageVIew Data = new ImageVIew();
            Data.FileList = actimgService.GetData(Sort);
            ActImg Album = new ActImg();
            Album = actimgService.GetDataBySort(Sort);
            Data.actimg = Album;
            return View(Data);
        }
        [HttpPost]
        public ActionResult Upload(ImageVIew File)
        {
            if (File.upload != null)
            {
                string Url = Path.Combine(Server.MapPath("~/FileUpload"), File.upload.FileName);
                File.upload.SaveAs(Url);
                actimgService.UploadFile(File.Sort, File.upload.FileName, Url);
            }
            return RedirectToAction("Upload", "ActImg", new { Sort = File.Sort });
        }
        #endregion

        #region 上傳相片
        public ActionResult UploadPhoto(string Sort)
        {
            ImageVIew Data = new ImageVIew();
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "請選擇分類", Value = "", Selected = true });
            list.Add(new SelectListItem() { Text = "背肌", Value = "背肌" });
            list.Add(new SelectListItem() { Text = "胸肌", Value = "胸肌" });
            list.Add(new SelectListItem() { Text = "腿部", Value = "腿部" });
            list.Add(new SelectListItem() { Text = "腹肌", Value = "腹肌" });
            list.Add(new SelectListItem() { Text = "肩部", Value = "肩部" });
            list.Add(new SelectListItem() { Text = "肱二頭肌", Value = "肱二頭肌" });
            list.Add(new SelectListItem() { Text = "肱三頭肌", Value = "肱三頭肌" });
            ViewBag.Sort = list;          
            ActImg Album = new ActImg();
            Album = actimgService.GetDataBySort(Sort);
            Data.actimg = Album;
            return View(Data);
        }
        [HttpPost]
        public ActionResult UploadPhoto(ImageVIew File)
        {
            if (File.upload != null)
            {
                string Url = "http://163.17.136.197:8080/EMG/FileUpload/" + File.upload.FileName;
                string url = Path.Combine(Server.MapPath("~/FileUpload"), File.upload.FileName);
                File.upload.SaveAs(url);
                actimgService.UploadFile(File.Sort, File.upload.FileName, Url);
            }
            return RedirectToAction("Index", "ActImg", new { Sort = File.Sort });
        }
        #endregion

        #region 刪除相片
        public ActionResult DeleteUpload(int Act_Id, string Sort)
        {
            actimgService.DeleteFile(Act_Id);
            return RedirectToAction("Upload", "ActImg", new { Sort = Sort });
        }
        #endregion

    }
}