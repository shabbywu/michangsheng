using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007CF RID: 1999
	public class BuffSeidJsonData44 : IJSONClass
	{
		// Token: 0x06003D4E RID: 15694 RVA: 0x001A42FC File Offset: 0x001A24FC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[44].list)
			{
				try
				{
					BuffSeidJsonData44 buffSeidJsonData = new BuffSeidJsonData44();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData44.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData44.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData44.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData44.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData44.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData44.OnInitFinishAction != null)
			{
				BuffSeidJsonData44.OnInitFinishAction();
			}
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037B8 RID: 14264
		public static int SEIDID = 44;

		// Token: 0x040037B9 RID: 14265
		public static Dictionary<int, BuffSeidJsonData44> DataDict = new Dictionary<int, BuffSeidJsonData44>();

		// Token: 0x040037BA RID: 14266
		public static List<BuffSeidJsonData44> DataList = new List<BuffSeidJsonData44>();

		// Token: 0x040037BB RID: 14267
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData44.OnInitFinish);

		// Token: 0x040037BC RID: 14268
		public int id;

		// Token: 0x040037BD RID: 14269
		public int value1;

		// Token: 0x040037BE RID: 14270
		public int value2;

		// Token: 0x040037BF RID: 14271
		public int value3;

		// Token: 0x040037C0 RID: 14272
		public int value4;
	}
}
