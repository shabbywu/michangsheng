using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007EA RID: 2026
	public class BuffSeidJsonData70 : IJSONClass
	{
		// Token: 0x06003DBA RID: 15802 RVA: 0x001A6A38 File Offset: 0x001A4C38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[70].list)
			{
				try
				{
					BuffSeidJsonData70 buffSeidJsonData = new BuffSeidJsonData70();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData70.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData70.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData70.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData70.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData70.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData70.OnInitFinishAction != null)
			{
				BuffSeidJsonData70.OnInitFinishAction();
			}
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003876 RID: 14454
		public static int SEIDID = 70;

		// Token: 0x04003877 RID: 14455
		public static Dictionary<int, BuffSeidJsonData70> DataDict = new Dictionary<int, BuffSeidJsonData70>();

		// Token: 0x04003878 RID: 14456
		public static List<BuffSeidJsonData70> DataList = new List<BuffSeidJsonData70>();

		// Token: 0x04003879 RID: 14457
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData70.OnInitFinish);

		// Token: 0x0400387A RID: 14458
		public int id;

		// Token: 0x0400387B RID: 14459
		public int value2;

		// Token: 0x0400387C RID: 14460
		public List<int> value1 = new List<int>();
	}
}
