using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C58 RID: 3160
	public class PaiMaiZhuChiBiao : IJSONClass
	{
		// Token: 0x06004CC9 RID: 19657 RVA: 0x00207398 File Offset: 0x00205598
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiZhuChiBiao.list)
			{
				try
				{
					PaiMaiZhuChiBiao paiMaiZhuChiBiao = new PaiMaiZhuChiBiao();
					paiMaiZhuChiBiao.id = jsonobject["id"].I;
					paiMaiZhuChiBiao.Type = jsonobject["Type"].I;
					paiMaiZhuChiBiao.Text = jsonobject["Text"].Str;
					if (PaiMaiZhuChiBiao.DataDict.ContainsKey(paiMaiZhuChiBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiZhuChiBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiZhuChiBiao.id));
					}
					else
					{
						PaiMaiZhuChiBiao.DataDict.Add(paiMaiZhuChiBiao.id, paiMaiZhuChiBiao);
						PaiMaiZhuChiBiao.DataList.Add(paiMaiZhuChiBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiZhuChiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiZhuChiBiao.OnInitFinishAction != null)
			{
				PaiMaiZhuChiBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BAE RID: 19374
		public static Dictionary<int, PaiMaiZhuChiBiao> DataDict = new Dictionary<int, PaiMaiZhuChiBiao>();

		// Token: 0x04004BAF RID: 19375
		public static List<PaiMaiZhuChiBiao> DataList = new List<PaiMaiZhuChiBiao>();

		// Token: 0x04004BB0 RID: 19376
		public static Action OnInitFinishAction = new Action(PaiMaiZhuChiBiao.OnInitFinish);

		// Token: 0x04004BB1 RID: 19377
		public int id;

		// Token: 0x04004BB2 RID: 19378
		public int Type;

		// Token: 0x04004BB3 RID: 19379
		public string Text;
	}
}
