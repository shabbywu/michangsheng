using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D8 RID: 2008
	public class BuffSeidJsonData53 : IJSONClass
	{
		// Token: 0x06003D72 RID: 15730 RVA: 0x001A50A0 File Offset: 0x001A32A0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[53].list)
			{
				try
				{
					BuffSeidJsonData53 buffSeidJsonData = new BuffSeidJsonData53();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData53.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData53.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData53.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData53.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData53.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData53.OnInitFinishAction != null)
			{
				BuffSeidJsonData53.OnInitFinishAction();
			}
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037FD RID: 14333
		public static int SEIDID = 53;

		// Token: 0x040037FE RID: 14334
		public static Dictionary<int, BuffSeidJsonData53> DataDict = new Dictionary<int, BuffSeidJsonData53>();

		// Token: 0x040037FF RID: 14335
		public static List<BuffSeidJsonData53> DataList = new List<BuffSeidJsonData53>();

		// Token: 0x04003800 RID: 14336
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData53.OnInitFinish);

		// Token: 0x04003801 RID: 14337
		public int id;

		// Token: 0x04003802 RID: 14338
		public int value1;

		// Token: 0x04003803 RID: 14339
		public int value2;

		// Token: 0x04003804 RID: 14340
		public int value3;

		// Token: 0x04003805 RID: 14341
		public int value4;
	}
}
