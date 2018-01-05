using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class FameServices : IFameServices
    {
        private IUnitOfWork _db;
        private IRepository<RecordEffect> _RecordEffectrepository;
        private IRepository<User> _Userrepository;

        public FameServices(IUnitOfWork uow, IRepository<RecordEffect> RecordEffectrepository, IRepository<User> Userrepository)
        {
            this._db = uow;
            this._RecordEffectrepository = RecordEffectrepository;
            this._Userrepository = Userrepository;
        }

        #region 取得名人榜
        public List<FameView> getFame()
        {
            List<FameView> fvList = new List<FameView>();
            List<User> userAllList = _Userrepository.GetAll().ToList();
            List<RecordEffect> reAllList = _RecordEffectrepository.GetAll().ToList();

            //會員各部位訓練成效陣列
            List<EffectTable> chestList = new List<EffectTable>();
            List<EffectTable> backList = new List<EffectTable>();
            List<EffectTable> shoulderList = new List<EffectTable>();
            List<EffectTable> bellyList = new List<EffectTable>();
            List<EffectTable> footList = new List<EffectTable>();
            List<EffectTable> twoList = new List<EffectTable>();
            List<EffectTable> threeList = new List<EffectTable>();

            List<EffectTable> chestList_Rank = new List<EffectTable>();
            List<EffectTable> backList_Rank = new List<EffectTable>();
            List<EffectTable> shoulderList_Rank = new List<EffectTable>();
            List<EffectTable> bellyList_Rank = new List<EffectTable>();
            List<EffectTable> footList_Rank = new List<EffectTable>();
            List<EffectTable> twoList_Rank = new List<EffectTable>();
            List<EffectTable> threeList_Rank = new List<EffectTable>();

            List<EffectTable> totalList_Rank = new List<EffectTable>();

            #region 取得會員各部位訓練成效
            for (int i = 0; i < userAllList.Count; i++)
            {
                EffectTable chest = new EffectTable();
                EffectTable back = new EffectTable();
                EffectTable shoulder = new EffectTable();
                EffectTable belly = new EffectTable();
                EffectTable foot = new EffectTable();
                EffectTable two = new EffectTable();
                EffectTable three = new EffectTable();
                var AllList = reAllList.Where(p => p.Account == userAllList[i].Account).ToList();

                //取得胸肌成效
                var chestEffect = AllList.Where(p => p.Part == "胸").Sum(p => p.Effect);
                chest.account = userAllList[i].Account;
                chest.name = userAllList[i].Name;
                chest.sumEffect = chestEffect;
                chestList.Add(chest);

                //取得背肌成效
                var backEffect = AllList.Where(p => p.Part == "背").Sum(p => p.Effect);
                back.account = userAllList[i].Account;
                back.name = userAllList[i].Name;
                back.sumEffect = backEffect;
                backList.Add(back);

                //取得肩肌成效
                var shoulderEffect = AllList.Where(p => p.Part == "肩").Sum(p => p.Effect);
                shoulder.account = userAllList[i].Account;
                shoulder.name = userAllList[i].Name;
                shoulder.sumEffect = shoulderEffect;
                shoulderList.Add(shoulder);

                //取得腹肌成效
                var bellyEffect = AllList.Where(p => p.Part == "腹").Sum(p => p.Effect);
                belly.account = userAllList[i].Account;
                belly.name = userAllList[i].Name;
                belly.sumEffect = bellyEffect;
                bellyList.Add(belly);

                //取得腿肌成效
                var footEffect = AllList.Where(p => p.Part == "腳").Sum(p => p.Effect);
                foot.account = userAllList[i].Account;
                foot.name = userAllList[i].Name;
                foot.sumEffect = footEffect;
                footList.Add(foot);

                //取得二頭肌成效
                var twoEffect = AllList.Where(p => p.Part == "肱二頭").Sum(p => p.Effect);
                two.account = userAllList[i].Account;
                two.name = userAllList[i].Name;
                two.sumEffect = twoEffect;
                twoList.Add(two);

                //取得三頭肌成效
                var threeEffect = AllList.Where(p => p.Part == "肱三頭").Sum(p => p.Effect);
                three.account = userAllList[i].Account;
                three.name = userAllList[i].Name;
                three.sumEffect = threeEffect;
                threeList.Add(three);
            }
            #endregion

            #region 計算各部位排名
            for (int i = 0; i < userAllList.Count; i++)
            {
                int chestRankCount = 1;
                int backRankCount = 1;
                int shoulderRankCount = 1;
                int bellyRankCount = 1;
                int footRankCount = 1;
                int twoRankCount = 1;
                int threeRankCount = 1;

                if (i == 0)
                {
                    chestList[i].rank = 1;
                    backList[i].rank = 1;
                    shoulderList[i].rank = 1;
                    bellyList[i].rank = 1;
                    footList[i].rank = 1;
                    twoList[i].rank = 1;
                    threeList[i].rank = 1;

                    chestList_Rank.Add(chestList[i]);
                    backList_Rank.Add(backList[i]);
                    shoulderList_Rank.Add(shoulderList[i]);
                    bellyList_Rank.Add(bellyList[i]);
                    footList_Rank.Add(footList[i]);
                    twoList_Rank.Add(twoList[i]);
                    threeList_Rank.Add(threeList[i]);
                }
                else
                {
                    for (int j = 0; j < chestList_Rank.Count; j++)
                    {
                        //胸
                        if (chestList[i].sumEffect > chestList_Rank[j].sumEffect)
                        {
                            chestList_Rank[j].rank += 1;
                        }
                        else
                        {
                            chestRankCount++;
                        }

                        //背
                        if (backList[i].sumEffect > backList_Rank[j].sumEffect)
                        {
                            backList_Rank[j].rank += 1;
                        }
                        else
                        {
                            backRankCount++;
                        }

                        //肩
                        if (shoulderList[i].sumEffect > shoulderList_Rank[j].sumEffect)
                        {
                            shoulderList_Rank[j].rank += 1;
                        }
                        else
                        {
                            shoulderRankCount++;
                        }

                        //腹
                        if (bellyList[i].sumEffect > bellyList_Rank[j].sumEffect)
                        {
                            bellyList_Rank[j].rank += 1;
                        }
                        else
                        {
                            bellyRankCount++;
                        }

                        //腳
                        if (footList[i].sumEffect > footList_Rank[j].sumEffect)
                        {
                            footList_Rank[j].rank += 1;
                        }
                        else
                        {
                            footRankCount++;
                        }

                        //二頭
                        if (twoList[i].sumEffect > twoList_Rank[j].sumEffect)
                        {
                            twoList_Rank[j].rank += 1;
                        }
                        else
                        {
                            twoRankCount++;
                        }

                        //三頭
                        if (threeList[i].sumEffect > threeList_Rank[j].sumEffect)
                        {
                            threeList_Rank[j].rank += 1;
                        }
                        else
                        {
                            threeRankCount++;
                        }
                    }

                    chestList[i].rank = chestRankCount;
                    backList[i].rank = backRankCount;
                    shoulderList[i].rank = shoulderRankCount;
                    bellyList[i].rank = bellyRankCount;
                    footList[i].rank = footRankCount;
                    twoList[i].rank = twoRankCount;
                    threeList[i].rank = threeRankCount;

                    chestList_Rank.Add(chestList[i]);
                    backList_Rank.Add(backList[i]);
                    shoulderList_Rank.Add(shoulderList[i]);
                    bellyList_Rank.Add(bellyList[i]);
                    footList_Rank.Add(footList[i]);
                    twoList_Rank.Add(twoList[i]);
                    threeList_Rank.Add(threeList[i]);
                }
            }
            #endregion

            #region 計算各排名加總平均
            List<EffectTable> sumRankTable = new List<EffectTable>();
            for (int i = 0; i < userAllList.Count; i++)
            {
                EffectTable data = new EffectTable();
                var sum = (chestList_Rank[i].rank +
                            backList_Rank[i].rank +
                            shoulderList_Rank[i].rank +
                            bellyList_Rank[i].rank +
                            footList_Rank[i].rank +
                            twoList_Rank[i].rank +
                            threeList_Rank[i].rank) / 7;
                data.account = userAllList[i].Account;
                data.name = userAllList[i].Name;
                data.Url = userAllList[i].Url;
                data.rank = 0;
                data.sumRank = sum;
            }
            #endregion

            for (int i = 0; i < sumRankTable.Count; i++)
            {
                int RankCount = 1;
                if (i == 0)
                {
                    sumRankTable[i].rank = 1;
                    totalList_Rank.Add(sumRankTable[i]);
                }
                else
                {
                    for (int j = 0; j < totalList_Rank.Count; j++)
                    {
                        if (sumRankTable[i].sumRank > totalList_Rank[j].sumRank)
                        {
                            totalList_Rank[j].rank += 1;
                        }
                        else
                        {
                            RankCount++;
                        }
                    }
                    sumRankTable[i].rank = RankCount;
                    totalList_Rank.Add(sumRankTable[i]);
                }
            }

            foreach (var item in totalList_Rank.OrderBy(p => p.rank).Take(5).ToList())
            {
                FameView fv = new FameView();
                fv.name = item.name;
                fv.rank = item.rank;
                fv.Url = item.Url;
                fvList.Add(fv);
            }
            return fvList;
        }
        #endregion

        public class EffectTable
        {
            public string account { get; set; }
            public string name { get; set; }
            public double? sumEffect { get; set; }
            public int rank { get; set; }
            public double sumRank { get; set; }
            public string Url { get; set; }
        }
    }
}