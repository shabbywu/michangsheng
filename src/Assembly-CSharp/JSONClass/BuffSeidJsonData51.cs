using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B6D RID: 2925
	public class BuffSeidJsonData51 : IJSONClass
	{
		// Token: 0x0600491C RID: 18716 RVA: 0x001F118C File Offset: 0x001EF38C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[51].list)
			{
				try
				{
					BuffSeidJsonData51 buffSeidJsonData = new BuffSeidJsonData51();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData51.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData51.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData51.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData51.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData51.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData51.OnInitFinishAction != null)
			{
				BuffSeidJsonData51.OnInitFinishAction();
			}
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400437E RID: 17278
		public static int SEIDID = 51;

		// Token: 0x0400437F RID: 17279
		public static Dictionary<int, BuffSeidJsonData51> DataDict = new Dictionary<int, BuffSeidJsonData51>();

		// Token: 0x04004380 RID: 17280
		public static List<BuffSeidJsonData51> DataList = new List<BuffSeidJsonData51>();

		// Token: 0x04004381 RID: 17281
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData51.OnInitFinish);

		// Token: 0x04004382 RID: 17282
		public int id;

		// Token: 0x04004383 RID: 17283
		public int value1;

		// Token: 0x04004384 RID: 17284
		public int value2;
	}
}
