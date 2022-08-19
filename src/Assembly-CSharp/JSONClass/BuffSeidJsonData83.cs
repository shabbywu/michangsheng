using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F4 RID: 2036
	public class BuffSeidJsonData83 : IJSONClass
	{
		// Token: 0x06003DE2 RID: 15842 RVA: 0x001A7838 File Offset: 0x001A5A38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[83].list)
			{
				try
				{
					BuffSeidJsonData83 buffSeidJsonData = new BuffSeidJsonData83();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData83.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData83.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData83.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData83.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData83.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData83.OnInitFinishAction != null)
			{
				BuffSeidJsonData83.OnInitFinishAction();
			}
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038B7 RID: 14519
		public static int SEIDID = 83;

		// Token: 0x040038B8 RID: 14520
		public static Dictionary<int, BuffSeidJsonData83> DataDict = new Dictionary<int, BuffSeidJsonData83>();

		// Token: 0x040038B9 RID: 14521
		public static List<BuffSeidJsonData83> DataList = new List<BuffSeidJsonData83>();

		// Token: 0x040038BA RID: 14522
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData83.OnInitFinish);

		// Token: 0x040038BB RID: 14523
		public int id;

		// Token: 0x040038BC RID: 14524
		public int value1;
	}
}
