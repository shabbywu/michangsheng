using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C51 RID: 3153
	public class PaiMaiCommandTips : IJSONClass
	{
		// Token: 0x06004CAD RID: 19629 RVA: 0x00206A00 File Offset: 0x00204C00
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

		// Token: 0x06004CAE RID: 19630 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B7C RID: 19324
		public static Dictionary<int, PaiMaiCommandTips> DataDict = new Dictionary<int, PaiMaiCommandTips>();

		// Token: 0x04004B7D RID: 19325
		public static List<PaiMaiCommandTips> DataList = new List<PaiMaiCommandTips>();

		// Token: 0x04004B7E RID: 19326
		public static Action OnInitFinishAction = new Action(PaiMaiCommandTips.OnInitFinish);

		// Token: 0x04004B7F RID: 19327
		public int id;

		// Token: 0x04004B80 RID: 19328
		public string Type;

		// Token: 0x04004B81 RID: 19329
		public string Text;
	}
}
