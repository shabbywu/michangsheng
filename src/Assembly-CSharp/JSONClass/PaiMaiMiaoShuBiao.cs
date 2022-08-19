using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C6 RID: 2246
	public class PaiMaiMiaoShuBiao : IJSONClass
	{
		// Token: 0x0600412B RID: 16683 RVA: 0x001BE524 File Offset: 0x001BC724
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiMiaoShuBiao.list)
			{
				try
				{
					PaiMaiMiaoShuBiao paiMaiMiaoShuBiao = new PaiMaiMiaoShuBiao();
					paiMaiMiaoShuBiao.id = jsonobject["id"].I;
					paiMaiMiaoShuBiao.Type = jsonobject["Type"].I;
					paiMaiMiaoShuBiao.Type2 = jsonobject["Type2"].I;
					paiMaiMiaoShuBiao.Text = jsonobject["Text"].Str;
					paiMaiMiaoShuBiao.Text2 = jsonobject["Text2"].Str;
					if (PaiMaiMiaoShuBiao.DataDict.ContainsKey(paiMaiMiaoShuBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiMiaoShuBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiMiaoShuBiao.id));
					}
					else
					{
						PaiMaiMiaoShuBiao.DataDict.Add(paiMaiMiaoShuBiao.id, paiMaiMiaoShuBiao);
						PaiMaiMiaoShuBiao.DataList.Add(paiMaiMiaoShuBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiMiaoShuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiMiaoShuBiao.OnInitFinishAction != null)
			{
				PaiMaiMiaoShuBiao.OnInitFinishAction();
			}
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400403D RID: 16445
		public static Dictionary<int, PaiMaiMiaoShuBiao> DataDict = new Dictionary<int, PaiMaiMiaoShuBiao>();

		// Token: 0x0400403E RID: 16446
		public static List<PaiMaiMiaoShuBiao> DataList = new List<PaiMaiMiaoShuBiao>();

		// Token: 0x0400403F RID: 16447
		public static Action OnInitFinishAction = new Action(PaiMaiMiaoShuBiao.OnInitFinish);

		// Token: 0x04004040 RID: 16448
		public int id;

		// Token: 0x04004041 RID: 16449
		public int Type;

		// Token: 0x04004042 RID: 16450
		public int Type2;

		// Token: 0x04004043 RID: 16451
		public string Text;

		// Token: 0x04004044 RID: 16452
		public string Text2;
	}
}
