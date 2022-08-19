using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007FA RID: 2042
	public class BuffSeidJsonData89 : IJSONClass
	{
		// Token: 0x06003DFA RID: 15866 RVA: 0x001A8084 File Offset: 0x001A6284
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[89].list)
			{
				try
				{
					BuffSeidJsonData89 buffSeidJsonData = new BuffSeidJsonData89();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData89.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData89.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData89.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData89.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData89.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData89.OnInitFinishAction != null)
			{
				BuffSeidJsonData89.OnInitFinishAction();
			}
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038DD RID: 14557
		public static int SEIDID = 89;

		// Token: 0x040038DE RID: 14558
		public static Dictionary<int, BuffSeidJsonData89> DataDict = new Dictionary<int, BuffSeidJsonData89>();

		// Token: 0x040038DF RID: 14559
		public static List<BuffSeidJsonData89> DataList = new List<BuffSeidJsonData89>();

		// Token: 0x040038E0 RID: 14560
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData89.OnInitFinish);

		// Token: 0x040038E1 RID: 14561
		public int id;

		// Token: 0x040038E2 RID: 14562
		public int value1;

		// Token: 0x040038E3 RID: 14563
		public int value2;
	}
}
