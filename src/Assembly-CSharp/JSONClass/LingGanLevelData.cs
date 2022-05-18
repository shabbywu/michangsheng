using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C0C RID: 3084
	public class LingGanLevelData : IJSONClass
	{
		// Token: 0x06004B99 RID: 19353 RVA: 0x001FEC20 File Offset: 0x001FCE20
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LingGanLevelData.list)
			{
				try
				{
					LingGanLevelData lingGanLevelData = new LingGanLevelData();
					lingGanLevelData.id = jsonobject["id"].I;
					lingGanLevelData.lingGanQuJian = jsonobject["lingGanQuJian"].I;
					if (LingGanLevelData.DataDict.ContainsKey(lingGanLevelData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LingGanLevelData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lingGanLevelData.id));
					}
					else
					{
						LingGanLevelData.DataDict.Add(lingGanLevelData.id, lingGanLevelData);
						LingGanLevelData.DataList.Add(lingGanLevelData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LingGanLevelData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LingGanLevelData.OnInitFinishAction != null)
			{
				LingGanLevelData.OnInitFinishAction();
			}
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400485A RID: 18522
		public static Dictionary<int, LingGanLevelData> DataDict = new Dictionary<int, LingGanLevelData>();

		// Token: 0x0400485B RID: 18523
		public static List<LingGanLevelData> DataList = new List<LingGanLevelData>();

		// Token: 0x0400485C RID: 18524
		public static Action OnInitFinishAction = new Action(LingGanLevelData.OnInitFinish);

		// Token: 0x0400485D RID: 18525
		public int id;

		// Token: 0x0400485E RID: 18526
		public int lingGanQuJian;
	}
}
