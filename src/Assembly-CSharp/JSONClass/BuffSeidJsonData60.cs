using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007DF RID: 2015
	public class BuffSeidJsonData60 : IJSONClass
	{
		// Token: 0x06003D8E RID: 15758 RVA: 0x001A5ADC File Offset: 0x001A3CDC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[60].list)
			{
				try
				{
					BuffSeidJsonData60 buffSeidJsonData = new BuffSeidJsonData60();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData60.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData60.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData60.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData60.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData60.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData60.OnInitFinishAction != null)
			{
				BuffSeidJsonData60.OnInitFinishAction();
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400382E RID: 14382
		public static int SEIDID = 60;

		// Token: 0x0400382F RID: 14383
		public static Dictionary<int, BuffSeidJsonData60> DataDict = new Dictionary<int, BuffSeidJsonData60>();

		// Token: 0x04003830 RID: 14384
		public static List<BuffSeidJsonData60> DataList = new List<BuffSeidJsonData60>();

		// Token: 0x04003831 RID: 14385
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData60.OnInitFinish);

		// Token: 0x04003832 RID: 14386
		public int id;

		// Token: 0x04003833 RID: 14387
		public int value1;
	}
}
