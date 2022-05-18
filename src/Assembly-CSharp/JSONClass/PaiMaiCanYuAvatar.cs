using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C4D RID: 3149
	public class PaiMaiCanYuAvatar : IJSONClass
	{
		// Token: 0x06004C9D RID: 19613 RVA: 0x0020624C File Offset: 0x0020444C
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

		// Token: 0x06004C9E RID: 19614 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B4E RID: 19278
		public static Dictionary<int, PaiMaiCanYuAvatar> DataDict = new Dictionary<int, PaiMaiCanYuAvatar>();

		// Token: 0x04004B4F RID: 19279
		public static List<PaiMaiCanYuAvatar> DataList = new List<PaiMaiCanYuAvatar>();

		// Token: 0x04004B50 RID: 19280
		public static Action OnInitFinishAction = new Action(PaiMaiCanYuAvatar.OnInitFinish);

		// Token: 0x04004B51 RID: 19281
		public int id;

		// Token: 0x04004B52 RID: 19282
		public int PaiMaiID;

		// Token: 0x04004B53 RID: 19283
		public int AvatrNum;

		// Token: 0x04004B54 RID: 19284
		public int CompereID;

		// Token: 0x04004B55 RID: 19285
		public int Jie;

		// Token: 0x04004B56 RID: 19286
		public string StarTime;

		// Token: 0x04004B57 RID: 19287
		public string EndTime;

		// Token: 0x04004B58 RID: 19288
		public List<int> FuYou = new List<int>();

		// Token: 0x04004B59 RID: 19289
		public List<int> AvatrID = new List<int>();

		// Token: 0x04004B5A RID: 19290
		public List<int> JinJie = new List<int>();
	}
}
