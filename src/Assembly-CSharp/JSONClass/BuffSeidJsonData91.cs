using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007FC RID: 2044
	public class BuffSeidJsonData91 : IJSONClass
	{
		// Token: 0x06003E02 RID: 15874 RVA: 0x001A8370 File Offset: 0x001A6570
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[91].list)
			{
				try
				{
					BuffSeidJsonData91 buffSeidJsonData = new BuffSeidJsonData91();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData91.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData91.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData91.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData91.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData91.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData91.OnInitFinishAction != null)
			{
				BuffSeidJsonData91.OnInitFinishAction();
			}
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038EB RID: 14571
		public static int SEIDID = 91;

		// Token: 0x040038EC RID: 14572
		public static Dictionary<int, BuffSeidJsonData91> DataDict = new Dictionary<int, BuffSeidJsonData91>();

		// Token: 0x040038ED RID: 14573
		public static List<BuffSeidJsonData91> DataList = new List<BuffSeidJsonData91>();

		// Token: 0x040038EE RID: 14574
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData91.OnInitFinish);

		// Token: 0x040038EF RID: 14575
		public int id;

		// Token: 0x040038F0 RID: 14576
		public int value1;
	}
}
