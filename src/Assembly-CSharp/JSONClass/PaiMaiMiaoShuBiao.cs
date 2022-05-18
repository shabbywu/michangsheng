using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C54 RID: 3156
	public class PaiMaiMiaoShuBiao : IJSONClass
	{
		// Token: 0x06004CB9 RID: 19641 RVA: 0x00206E0C File Offset: 0x0020500C
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

		// Token: 0x06004CBA RID: 19642 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B91 RID: 19345
		public static Dictionary<int, PaiMaiMiaoShuBiao> DataDict = new Dictionary<int, PaiMaiMiaoShuBiao>();

		// Token: 0x04004B92 RID: 19346
		public static List<PaiMaiMiaoShuBiao> DataList = new List<PaiMaiMiaoShuBiao>();

		// Token: 0x04004B93 RID: 19347
		public static Action OnInitFinishAction = new Action(PaiMaiMiaoShuBiao.OnInitFinish);

		// Token: 0x04004B94 RID: 19348
		public int id;

		// Token: 0x04004B95 RID: 19349
		public int Type;

		// Token: 0x04004B96 RID: 19350
		public int Type2;

		// Token: 0x04004B97 RID: 19351
		public string Text;

		// Token: 0x04004B98 RID: 19352
		public string Text2;
	}
}
