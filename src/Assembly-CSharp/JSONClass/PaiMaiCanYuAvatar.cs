using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008BF RID: 2239
	public class PaiMaiCanYuAvatar : IJSONClass
	{
		// Token: 0x0600410F RID: 16655 RVA: 0x001BD7E8 File Offset: 0x001BB9E8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiCanYuAvatar.list)
			{
				try
				{
					PaiMaiCanYuAvatar paiMaiCanYuAvatar = new PaiMaiCanYuAvatar();
					paiMaiCanYuAvatar.id = jsonobject["id"].I;
					paiMaiCanYuAvatar.PaiMaiID = jsonobject["PaiMaiID"].I;
					paiMaiCanYuAvatar.AvatrNum = jsonobject["AvatrNum"].I;
					paiMaiCanYuAvatar.CompereID = jsonobject["CompereID"].I;
					paiMaiCanYuAvatar.Jie = jsonobject["Jie"].I;
					paiMaiCanYuAvatar.StarTime = jsonobject["StarTime"].Str;
					paiMaiCanYuAvatar.EndTime = jsonobject["EndTime"].Str;
					paiMaiCanYuAvatar.FuYou = jsonobject["FuYou"].ToList();
					paiMaiCanYuAvatar.AvatrID = jsonobject["AvatrID"].ToList();
					paiMaiCanYuAvatar.JinJie = jsonobject["JinJie"].ToList();
					if (PaiMaiCanYuAvatar.DataDict.ContainsKey(paiMaiCanYuAvatar.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiCanYuAvatar.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiCanYuAvatar.id));
					}
					else
					{
						PaiMaiCanYuAvatar.DataDict.Add(paiMaiCanYuAvatar.id, paiMaiCanYuAvatar);
						PaiMaiCanYuAvatar.DataList.Add(paiMaiCanYuAvatar);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiCanYuAvatar.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiCanYuAvatar.OnInitFinishAction != null)
			{
				PaiMaiCanYuAvatar.OnInitFinishAction();
			}
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FFA RID: 16378
		public static Dictionary<int, PaiMaiCanYuAvatar> DataDict = new Dictionary<int, PaiMaiCanYuAvatar>();

		// Token: 0x04003FFB RID: 16379
		public static List<PaiMaiCanYuAvatar> DataList = new List<PaiMaiCanYuAvatar>();

		// Token: 0x04003FFC RID: 16380
		public static Action OnInitFinishAction = new Action(PaiMaiCanYuAvatar.OnInitFinish);

		// Token: 0x04003FFD RID: 16381
		public int id;

		// Token: 0x04003FFE RID: 16382
		public int PaiMaiID;

		// Token: 0x04003FFF RID: 16383
		public int AvatrNum;

		// Token: 0x04004000 RID: 16384
		public int CompereID;

		// Token: 0x04004001 RID: 16385
		public int Jie;

		// Token: 0x04004002 RID: 16386
		public string StarTime;

		// Token: 0x04004003 RID: 16387
		public string EndTime;

		// Token: 0x04004004 RID: 16388
		public List<int> FuYou = new List<int>();

		// Token: 0x04004005 RID: 16389
		public List<int> AvatrID = new List<int>();

		// Token: 0x04004006 RID: 16390
		public List<int> JinJie = new List<int>();
	}
}
