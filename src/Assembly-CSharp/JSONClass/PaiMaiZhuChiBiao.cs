using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008CA RID: 2250
	public class PaiMaiZhuChiBiao : IJSONClass
	{
		// Token: 0x0600413B RID: 16699 RVA: 0x001BEB64 File Offset: 0x001BCD64
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

		// Token: 0x0600413C RID: 16700 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400405A RID: 16474
		public static Dictionary<int, PaiMaiZhuChiBiao> DataDict = new Dictionary<int, PaiMaiZhuChiBiao>();

		// Token: 0x0400405B RID: 16475
		public static List<PaiMaiZhuChiBiao> DataList = new List<PaiMaiZhuChiBiao>();

		// Token: 0x0400405C RID: 16476
		public static Action OnInitFinishAction = new Action(PaiMaiZhuChiBiao.OnInitFinish);

		// Token: 0x0400405D RID: 16477
		public int id;

		// Token: 0x0400405E RID: 16478
		public int Type;

		// Token: 0x0400405F RID: 16479
		public string Text;
	}
}
