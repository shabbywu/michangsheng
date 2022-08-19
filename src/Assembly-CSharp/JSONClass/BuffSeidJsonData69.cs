using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E8 RID: 2024
	public class BuffSeidJsonData69 : IJSONClass
	{
		// Token: 0x06003DB2 RID: 15794 RVA: 0x001A6774 File Offset: 0x001A4974
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[69].list)
			{
				try
				{
					BuffSeidJsonData69 buffSeidJsonData = new BuffSeidJsonData69();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData69.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData69.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData69.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData69.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData69.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData69.OnInitFinishAction != null)
			{
				BuffSeidJsonData69.OnInitFinishAction();
			}
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003869 RID: 14441
		public static int SEIDID = 69;

		// Token: 0x0400386A RID: 14442
		public static Dictionary<int, BuffSeidJsonData69> DataDict = new Dictionary<int, BuffSeidJsonData69>();

		// Token: 0x0400386B RID: 14443
		public static List<BuffSeidJsonData69> DataList = new List<BuffSeidJsonData69>();

		// Token: 0x0400386C RID: 14444
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData69.OnInitFinish);

		// Token: 0x0400386D RID: 14445
		public int id;

		// Token: 0x0400386E RID: 14446
		public int value1;

		// Token: 0x0400386F RID: 14447
		public int value2;
	}
}
