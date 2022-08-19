using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007FF RID: 2047
	public class BuffSeidJsonData94 : IJSONClass
	{
		// Token: 0x06003E0E RID: 15886 RVA: 0x001A878C File Offset: 0x001A698C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[94].list)
			{
				try
				{
					BuffSeidJsonData94 buffSeidJsonData = new BuffSeidJsonData94();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData94.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData94.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData94.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData94.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData94.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData94.OnInitFinishAction != null)
			{
				BuffSeidJsonData94.OnInitFinishAction();
			}
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038FE RID: 14590
		public static int SEIDID = 94;

		// Token: 0x040038FF RID: 14591
		public static Dictionary<int, BuffSeidJsonData94> DataDict = new Dictionary<int, BuffSeidJsonData94>();

		// Token: 0x04003900 RID: 14592
		public static List<BuffSeidJsonData94> DataList = new List<BuffSeidJsonData94>();

		// Token: 0x04003901 RID: 14593
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData94.OnInitFinish);

		// Token: 0x04003902 RID: 14594
		public int id;

		// Token: 0x04003903 RID: 14595
		public int value1;
	}
}
