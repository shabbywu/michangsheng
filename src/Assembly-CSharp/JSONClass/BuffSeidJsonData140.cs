using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000776 RID: 1910
	public class BuffSeidJsonData140 : IJSONClass
	{
		// Token: 0x06003BEC RID: 15340 RVA: 0x0019C1C0 File Offset: 0x0019A3C0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[140].list)
			{
				try
				{
					BuffSeidJsonData140 buffSeidJsonData = new BuffSeidJsonData140();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData140.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData140.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData140.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData140.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData140.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData140.OnInitFinishAction != null)
			{
				BuffSeidJsonData140.OnInitFinishAction();
			}
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400354D RID: 13645
		public static int SEIDID = 140;

		// Token: 0x0400354E RID: 13646
		public static Dictionary<int, BuffSeidJsonData140> DataDict = new Dictionary<int, BuffSeidJsonData140>();

		// Token: 0x0400354F RID: 13647
		public static List<BuffSeidJsonData140> DataList = new List<BuffSeidJsonData140>();

		// Token: 0x04003550 RID: 13648
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData140.OnInitFinish);

		// Token: 0x04003551 RID: 13649
		public int id;

		// Token: 0x04003552 RID: 13650
		public int value1;

		// Token: 0x04003553 RID: 13651
		public int value2;

		// Token: 0x04003554 RID: 13652
		public int value3;
	}
}
