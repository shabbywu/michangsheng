using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008C3 RID: 2243
	public class PaiMaiCommandTips : IJSONClass
	{
		// Token: 0x0600411F RID: 16671 RVA: 0x001BE08C File Offset: 0x001BC28C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PaiMaiCommandTips.list)
			{
				try
				{
					PaiMaiCommandTips paiMaiCommandTips = new PaiMaiCommandTips();
					paiMaiCommandTips.id = jsonobject["id"].I;
					paiMaiCommandTips.Type = jsonobject["Type"].Str;
					paiMaiCommandTips.Text = jsonobject["Text"].Str;
					if (PaiMaiCommandTips.DataDict.ContainsKey(paiMaiCommandTips.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PaiMaiCommandTips.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", paiMaiCommandTips.id));
					}
					else
					{
						PaiMaiCommandTips.DataDict.Add(paiMaiCommandTips.id, paiMaiCommandTips);
						PaiMaiCommandTips.DataList.Add(paiMaiCommandTips);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PaiMaiCommandTips.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PaiMaiCommandTips.OnInitFinishAction != null)
			{
				PaiMaiCommandTips.OnInitFinishAction();
			}
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004028 RID: 16424
		public static Dictionary<int, PaiMaiCommandTips> DataDict = new Dictionary<int, PaiMaiCommandTips>();

		// Token: 0x04004029 RID: 16425
		public static List<PaiMaiCommandTips> DataList = new List<PaiMaiCommandTips>();

		// Token: 0x0400402A RID: 16426
		public static Action OnInitFinishAction = new Action(PaiMaiCommandTips.OnInitFinish);

		// Token: 0x0400402B RID: 16427
		public int id;

		// Token: 0x0400402C RID: 16428
		public string Type;

		// Token: 0x0400402D RID: 16429
		public string Text;
	}
}
