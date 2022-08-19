using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C6 RID: 1990
	public class BuffSeidJsonData35 : IJSONClass
	{
		// Token: 0x06003D2A RID: 15658 RVA: 0x001A3468 File Offset: 0x001A1668
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[35].list)
			{
				try
				{
					BuffSeidJsonData35 buffSeidJsonData = new BuffSeidJsonData35();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData35.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData35.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData35.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData35.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData35.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData35.OnInitFinishAction != null)
			{
				BuffSeidJsonData35.OnInitFinishAction();
			}
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400376B RID: 14187
		public static int SEIDID = 35;

		// Token: 0x0400376C RID: 14188
		public static Dictionary<int, BuffSeidJsonData35> DataDict = new Dictionary<int, BuffSeidJsonData35>();

		// Token: 0x0400376D RID: 14189
		public static List<BuffSeidJsonData35> DataList = new List<BuffSeidJsonData35>();

		// Token: 0x0400376E RID: 14190
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData35.OnInitFinish);

		// Token: 0x0400376F RID: 14191
		public int id;

		// Token: 0x04003770 RID: 14192
		public int value1;

		// Token: 0x04003771 RID: 14193
		public int value2;

		// Token: 0x04003772 RID: 14194
		public int value3;

		// Token: 0x04003773 RID: 14195
		public int value4;
	}
}
