using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C4C RID: 3148
	public class PaiMaiBiao : IJSONClass
	{
		// Token: 0x06004C99 RID: 19609 RVA: 0x00205FB0 File Offset: 0x002041B0
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

		// Token: 0x06004C9A RID: 19610 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B39 RID: 19257
		public static Dictionary<int, PaiMaiBiao> DataDict = new Dictionary<int, PaiMaiBiao>();

		// Token: 0x04004B3A RID: 19258
		public static List<PaiMaiBiao> DataList = new List<PaiMaiBiao>();

		// Token: 0x04004B3B RID: 19259
		public static Action OnInitFinishAction = new Action(PaiMaiBiao.OnInitFinish);

		// Token: 0x04004B3C RID: 19260
		public int PaiMaiID;

		// Token: 0x04004B3D RID: 19261
		public int ItemNum;

		// Token: 0x04004B3E RID: 19262
		public int Price;

		// Token: 0x04004B3F RID: 19263
		public int RuChangFei;

		// Token: 0x04004B40 RID: 19264
		public int circulation;

		// Token: 0x04004B41 RID: 19265
		public int paimaifenzu;

		// Token: 0x04004B42 RID: 19266
		public int jimainum;

		// Token: 0x04004B43 RID: 19267
		public int IsBuShuaXin;

		// Token: 0x04004B44 RID: 19268
		public int level;

		// Token: 0x04004B45 RID: 19269
		public string Name;

		// Token: 0x04004B46 RID: 19270
		public string ChangJing;

		// Token: 0x04004B47 RID: 19271
		public string StarTime;

		// Token: 0x04004B48 RID: 19272
		public string EndTime;

		// Token: 0x04004B49 RID: 19273
		public List<int> Type = new List<int>();

		// Token: 0x04004B4A RID: 19274
		public List<int> quality = new List<int>();

		// Token: 0x04004B4B RID: 19275
		public List<int> quanzhong1 = new List<int>();

		// Token: 0x04004B4C RID: 19276
		public List<int> guding = new List<int>();

		// Token: 0x04004B4D RID: 19277
		public List<int> quanzhong2 = new List<int>();
	}
}
