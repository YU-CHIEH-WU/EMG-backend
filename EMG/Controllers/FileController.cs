using AutoMapper;
using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    public class FileController : Controller
    {
        private IFileServices _fileServices;
        private IUserServices _userServices;

        public FileController(IFileServices fileServices, IUserServices userServices)
        {
            this._fileServices = fileServices;
            this._userServices = userServices;
        }

        #region 首頁
        public ActionResult Index()
        {
            List<Model.File> fileList = _fileServices.GetAll();
            FileListView fileListView = new FileListView();
            fileListView.FirstFile = new List<FileView>();
            fileListView.FirstFile = Mapper.Map<List<FileView>>(fileList);
            foreach (var item in fileListView.FirstFile)
            {
                item.UserName = _userServices.GetNameByAccount(item.Account);
            }
            return View(fileListView);
        }

        #endregion

        #region 新增檔案

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var type = Path.GetExtension(file.FileName);
                var pathName = fileName + "_0" + type;
                var path = Path.Combine(Server.MapPath("~/FileUpload"), pathName);
                var size = file.ContentLength;
                //將檔案存入資料庫
                _fileServices.Create(fileName, type, path, size);
                //將檔案存放至實體路徑
                file.SaveAs(path);
            }
            //更新檔案列表
            List<Model.File> fileList = _fileServices.GetAll();
            FileListView fileListView = new FileListView();
            fileListView.FirstFile = new List<FileView>();
            fileListView.FirstFile = Mapper.Map<List<FileView>>(fileList);
            foreach (var item in fileListView.FirstFile)
            {
                item.UserName = _userServices.GetNameByAccount(item.Account);
            }
            //更新部分頁面
            return PartialView("_IndexPartial", fileListView);
        }

        #endregion

        #region 版本列表
        public ActionResult Edition(int id)
        {
            //根據檔案編號取得所有版本
            List<Model.File> editionList = _fileServices.GetEdition(id);
            EditionView editionListView = new EditionView();
            editionListView.fileList = new List<FileView>();
            editionListView.fileList = Mapper.Map<List<FileView>>(editionList);
            foreach (var item in editionListView.fileList)
            {
                item.UserName = _userServices.GetNameByAccount(item.Account);
            }
            ViewBag.id = id;
            return View(editionListView);
        }

        #endregion

        #region 新增版本

        [HttpPost]
        public ActionResult UploadEdition(HttpPostedFileBase edition, int id)
        {
            if (edition != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(edition.FileName);
                //取得版本號
                var editioncount = _fileServices.GetEditionCount(id);
                var type = Path.GetExtension(edition.FileName);
                var pathName = fileName + "_" + editioncount + type;
                var path = Path.Combine(Server.MapPath("~/FileUpload"), pathName);
                var size = edition.ContentLength;
                // 將檔案存入資料庫
                _fileServices.Update(id, fileName, editioncount, type, path, size);
                //將檔案存放至實體路徑
                edition.SaveAs(path);
            }
            //更新版本列表
            List<Model.File> editionList = _fileServices.GetEdition(id);
            EditionView editionListView = new EditionView();
            editionListView.fileList = new List<FileView>();
            editionListView.fileList = Mapper.Map<List<FileView>>(editionList);
            foreach (var item in editionListView.fileList)
            {
                item.UserName = _userServices.GetNameByAccount(item.Account);
            }
            ViewBag.id = id;
            //更新部分頁面
            return PartialView("_EditionPartial", editionListView);
        }

        #endregion

        #region 下載檔案

        public ActionResult Download(int id, int edition)
        {
            Model.File downloadFile = _fileServices.Get(id, edition);
            return File(downloadFile.Url, "text/plain", downloadFile.Name + downloadFile.Type);
        }

        #endregion

        #region 待刪檔案列表

        public ActionResult Trash()
        {
            List<Model.File> fileList = _fileServices.GetTrash();
            FileListView fileListView = new FileListView();
            fileListView.FirstFile = new List<FileView>();
            //將檔案轉型
            fileListView.FirstFile = Mapper.Map<List<FileView>>(fileList);
            foreach (var item in fileListView.FirstFile)
            {
                //透過帳號取得姓名
                item.UserName = _userServices.GetNameByAccount(item.Account);
            }
            return View(fileListView);
        }

        #endregion

        #region 刪除檔案(待刪)

        public ActionResult Delete(int id, int edition)
        {
            _fileServices.Delete(id, edition);
            return RedirectToAction("Trash", "File");
        }

        #endregion

        #region 確認刪除檔案

        public ActionResult DeleteConfirm(int id, int edition)
        {
            Model.File deleteFile = _fileServices.Get(id, edition);
            System.IO.File.Delete(deleteFile.Url);
            _fileServices.DeleteConfirm(id, edition);
            return RedirectToAction("Trash", "File");
        }

        #endregion
    }
}