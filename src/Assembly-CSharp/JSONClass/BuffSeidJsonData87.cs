using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F8 RID: 2040
	public class BuffSeidJsonData87 : IJSONClass
	{
		// Token: 0x06003DF2 RID: 15858 RVA: 0x001A7DAC File Offset: 0x001A5FAC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[87].list)
			{
				try
				{
					BuffSeidJsonData87 buffSeidJsonData = new BuffSeidJsonData87();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData87.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData87.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData87.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData87.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData87.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData87.OnInitFinishAction != null)
			{
				BuffSeidJsonData87.OnInitFinishAction();
			}
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038CF RID: 14543
		public static int SEIDID = 87;

		// Token: 0x040038D0 RID: 14544
		public static Dictionary<int, BuffSeidJsonData87> DataDict = new Dictionary<int, BuffSeidJsonData87>();

		// Token: 0x040038D1 RID: 14545
		public static List<BuffSeidJsonData87> DataList = new List<BuffSeidJsonData87>();

		// Token: 0x040038D2 RID: 14546
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData87.OnInitFinish);

		// Token: 0x040038D3 RID: 14547
		public int id;

		// Token: 0x040038D4 RID: 14548
		public int value1;

		// Token: 0x040038D5 RID: 14549
		public int value2;
	}
}
