using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B5F RID: 2911
	public class BuffSeidJsonData37 : IJSONClass
	{
		// Token: 0x060048E4 RID: 18660 RVA: 0x001EFE48 File Offset: 0x001EE048
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[37].list)
			{
				try
				{
					BuffSeidJsonData37 buffSeidJsonData = new BuffSeidJsonData37();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData37.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData37.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData37.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData37.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData37.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData37.OnInitFinishAction != null)
			{
				BuffSeidJsonData37.OnInitFinishAction();
			}
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400430C RID: 17164
		public static int SEIDID = 37;

		// Token: 0x0400430D RID: 17165
		public static Dictionary<int, BuffSeidJsonData37> DataDict = new Dictionary<int, BuffSeidJsonData37>();

		// Token: 0x0400430E RID: 17166
		public static List<BuffSeidJsonData37> DataList = new List<BuffSeidJsonData37>();

		// Token: 0x0400430F RID: 17167
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData37.OnInitFinish);

		// Token: 0x04004310 RID: 17168
		public int id;

		// Token: 0x04004311 RID: 17169
		public int value1;

		// Token: 0x04004312 RID: 17170
		public int value2;

		// Token: 0x04004313 RID: 17171
		public int value3;

		// Token: 0x04004314 RID: 17172
		public int value4;
	}
}
