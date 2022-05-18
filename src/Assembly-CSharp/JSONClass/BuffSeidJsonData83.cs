using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B8B RID: 2955
	public class BuffSeidJsonData83 : IJSONClass
	{
		// Token: 0x06004994 RID: 18836 RVA: 0x001F363C File Offset: 0x001F183C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[83].list)
			{
				try
				{
					BuffSeidJsonData83 buffSeidJsonData = new BuffSeidJsonData83();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData83.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData83.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData83.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData83.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData83.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData83.OnInitFinishAction != null)
			{
				BuffSeidJsonData83.OnInitFinishAction();
			}
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004447 RID: 17479
		public static int SEIDID = 83;

		// Token: 0x04004448 RID: 17480
		public static Dictionary<int, BuffSeidJsonData83> DataDict = new Dictionary<int, BuffSeidJsonData83>();

		// Token: 0x04004449 RID: 17481
		public static List<BuffSeidJsonData83> DataList = new List<BuffSeidJsonData83>();

		// Token: 0x0400444A RID: 17482
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData83.OnInitFinish);

		// Token: 0x0400444B RID: 17483
		public int id;

		// Token: 0x0400444C RID: 17484
		public int value1;
	}
}
