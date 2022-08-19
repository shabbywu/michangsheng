using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008BE RID: 2238
	public class PaiMaiBiao : IJSONClass
	{
		// Token: 0x0600410B RID: 16651 RVA: 0x001BD4E4 File Offset: 0x001BB6E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiBiao.list)
			{
				try
				{
					PaiMaiBiao paiMaiBiao = new PaiMaiBiao();
					paiMaiBiao.PaiMaiID = jsonobject["PaiMaiID"].I;
					paiMaiBiao.ItemNum = jsonobject["ItemNum"].I;
					paiMaiBiao.Price = jsonobject["Price"].I;
					paiMaiBiao.RuChangFei = jsonobject["RuChangFei"].I;
					paiMaiBiao.circulation = jsonobject["circulation"].I;
					paiMaiBiao.paimaifenzu = jsonobject["paimaifenzu"].I;
					paiMaiBiao.jimainum = jsonobject["jimainum"].I;
					paiMaiBiao.IsBuShuaXin = jsonobject["IsBuShuaXin"].I;
					paiMaiBiao.level = jsonobject["level"].I;
					paiMaiBiao.Name = jsonobject["Name"].Str;
					paiMaiBiao.ChangJing = jsonobject["ChangJing"].Str;
					paiMaiBiao.StarTime = jsonobject["StarTime"].Str;
					paiMaiBiao.EndTime = jsonobject["EndTime"].Str;
					paiMaiBiao.Type = jsonobject["Type"].ToList();
					paiMaiBiao.quality = jsonobject["quality"].ToList();
					paiMaiBiao.quanzhong1 = jsonobject["quanzhong1"].ToList();
					paiMaiBiao.guding = jsonobject["guding"].ToList();
					paiMaiBiao.quanzhong2 = jsonobject["quanzhong2"].ToList();
					if (PaiMaiBiao.DataDict.ContainsKey(paiMaiBiao.PaiMaiID))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiBiao.PaiMaiID));
					}
					else
					{
						PaiMaiBiao.DataDict.Add(paiMaiBiao.PaiMaiID, paiMaiBiao);
						PaiMaiBiao.DataList.Add(paiMaiBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiBiao.OnInitFinishAction != null)
			{
				PaiMaiBiao.OnInitFinishAction();
			}
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FE5 RID: 16357
		public static Dictionary<int, PaiMaiBiao> DataDict = new Dictionary<int, PaiMaiBiao>();

		// Token: 0x04003FE6 RID: 16358
		public static List<PaiMaiBiao> DataList = new List<PaiMaiBiao>();

		// Token: 0x04003FE7 RID: 16359
		public static Action OnInitFinishAction = new Action(PaiMaiBiao.OnInitFinish);

		// Token: 0x04003FE8 RID: 16360
		public int PaiMaiID;

		// Token: 0x04003FE9 RID: 16361
		public int ItemNum;

		// Token: 0x04003FEA RID: 16362
		public int Price;

		// Token: 0x04003FEB RID: 16363
		public int RuChangFei;

		// Token: 0x04003FEC RID: 16364
		public int circulation;

		// Token: 0x04003FED RID: 16365
		public int paimaifenzu;

		// Token: 0x04003FEE RID: 16366
		public int jimainum;

		// Token: 0x04003FEF RID: 16367
		public int IsBuShuaXin;

		// Token: 0x04003FF0 RID: 16368
		public int level;

		// Token: 0x04003FF1 RID: 16369
		public string Name;

		// Token: 0x04003FF2 RID: 16370
		public string ChangJing;

		// Token: 0x04003FF3 RID: 16371
		public string StarTime;

		// Token: 0x04003FF4 RID: 16372
		public string EndTime;

		// Token: 0x04003FF5 RID: 16373
		public List<int> Type = new List<int>();

		// Token: 0x04003FF6 RID: 16374
		public List<int> quality = new List<int>();

		// Token: 0x04003FF7 RID: 16375
		public List<int> quanzhong1 = new List<int>();

		// Token: 0x04003FF8 RID: 16376
		public List<int> guding = new List<int>();

		// Token: 0x04003FF9 RID: 16377
		public List<int> quanzhong2 = new List<int>();
	}
}
