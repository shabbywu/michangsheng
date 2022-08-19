using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007CD RID: 1997
	public class BuffSeidJsonData42 : IJSONClass
	{
		// Token: 0x06003D46 RID: 15686 RVA: 0x001A3FC8 File Offset: 0x001A21C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[42].list)
			{
				try
				{
					BuffSeidJsonData42 buffSeidJsonData = new BuffSeidJsonData42();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData42.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData42.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData42.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData42.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData42.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData42.OnInitFinishAction != null)
			{
				BuffSeidJsonData42.OnInitFinishAction();
			}
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037A7 RID: 14247
		public static int SEIDID = 42;

		// Token: 0x040037A8 RID: 14248
		public static Dictionary<int, BuffSeidJsonData42> DataDict = new Dictionary<int, BuffSeidJsonData42>();

		// Token: 0x040037A9 RID: 14249
		public static List<BuffSeidJsonData42> DataList = new List<BuffSeidJsonData42>();

		// Token: 0x040037AA RID: 14250
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData42.OnInitFinish);

		// Token: 0x040037AB RID: 14251
		public int id;

		// Token: 0x040037AC RID: 14252
		public int value1;

		// Token: 0x040037AD RID: 14253
		public int value2;

		// Token: 0x040037AE RID: 14254
		public int value3;

		// Token: 0x040037AF RID: 14255
		public int value4;
	}
}
