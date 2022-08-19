using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C8 RID: 1992
	public class BuffSeidJsonData37 : IJSONClass
	{
		// Token: 0x06003D32 RID: 15666 RVA: 0x001A379C File Offset: 0x001A199C
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

		// Token: 0x06003D33 RID: 15667 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400377C RID: 14204
		public static int SEIDID = 37;

		// Token: 0x0400377D RID: 14205
		public static Dictionary<int, BuffSeidJsonData37> DataDict = new Dictionary<int, BuffSeidJsonData37>();

		// Token: 0x0400377E RID: 14206
		public static List<BuffSeidJsonData37> DataList = new List<BuffSeidJsonData37>();

		// Token: 0x0400377F RID: 14207
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData37.OnInitFinish);

		// Token: 0x04003780 RID: 14208
		public int id;

		// Token: 0x04003781 RID: 14209
		public int value1;

		// Token: 0x04003782 RID: 14210
		public int value2;

		// Token: 0x04003783 RID: 14211
		public int value3;

		// Token: 0x04003784 RID: 14212
		public int value4;
	}
}
