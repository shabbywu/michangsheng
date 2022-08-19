using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D0 RID: 2000
	public class BuffSeidJsonData45 : IJSONClass
	{
		// Token: 0x06003D52 RID: 15698 RVA: 0x001A44AC File Offset: 0x001A26AC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[45].list)
			{
				try
				{
					BuffSeidJsonData45 buffSeidJsonData = new BuffSeidJsonData45();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData45.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData45.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData45.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData45.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData45.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData45.OnInitFinishAction != null)
			{
				BuffSeidJsonData45.OnInitFinishAction();
			}
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037C1 RID: 14273
		public static int SEIDID = 45;

		// Token: 0x040037C2 RID: 14274
		public static Dictionary<int, BuffSeidJsonData45> DataDict = new Dictionary<int, BuffSeidJsonData45>();

		// Token: 0x040037C3 RID: 14275
		public static List<BuffSeidJsonData45> DataList = new List<BuffSeidJsonData45>();

		// Token: 0x040037C4 RID: 14276
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData45.OnInitFinish);

		// Token: 0x040037C5 RID: 14277
		public int id;

		// Token: 0x040037C6 RID: 14278
		public int value1;

		// Token: 0x040037C7 RID: 14279
		public int value2;

		// Token: 0x040037C8 RID: 14280
		public int value3;

		// Token: 0x040037C9 RID: 14281
		public int value4;
	}
}
