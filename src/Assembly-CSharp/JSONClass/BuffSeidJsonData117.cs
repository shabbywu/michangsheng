using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AFD RID: 2813
	public class BuffSeidJsonData117 : IJSONClass
	{
		// Token: 0x0600475E RID: 18270 RVA: 0x001E8560 File Offset: 0x001E6760
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[117].list)
			{
				try
				{
					BuffSeidJsonData117 buffSeidJsonData = new BuffSeidJsonData117();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData117.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData117.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData117.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData117.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData117.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData117.OnInitFinishAction != null)
			{
				BuffSeidJsonData117.OnInitFinishAction();
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400406E RID: 16494
		public static int SEIDID = 117;

		// Token: 0x0400406F RID: 16495
		public static Dictionary<int, BuffSeidJsonData117> DataDict = new Dictionary<int, BuffSeidJsonData117>();

		// Token: 0x04004070 RID: 16496
		public static List<BuffSeidJsonData117> DataList = new List<BuffSeidJsonData117>();

		// Token: 0x04004071 RID: 16497
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData117.OnInitFinish);

		// Token: 0x04004072 RID: 16498
		public int id;

		// Token: 0x04004073 RID: 16499
		public int value1;

		// Token: 0x04004074 RID: 16500
		public int value2;
	}
}
