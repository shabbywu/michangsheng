using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007CC RID: 1996
	public class BuffSeidJsonData41 : IJSONClass
	{
		// Token: 0x06003D42 RID: 15682 RVA: 0x001A3E04 File Offset: 0x001A2004
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[41].list)
			{
				try
				{
					BuffSeidJsonData41 buffSeidJsonData = new BuffSeidJsonData41();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].ToList();
					if (BuffSeidJsonData41.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData41.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData41.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData41.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData41.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData41.OnInitFinishAction != null)
			{
				BuffSeidJsonData41.OnInitFinishAction();
			}
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400379E RID: 14238
		public static int SEIDID = 41;

		// Token: 0x0400379F RID: 14239
		public static Dictionary<int, BuffSeidJsonData41> DataDict = new Dictionary<int, BuffSeidJsonData41>();

		// Token: 0x040037A0 RID: 14240
		public static List<BuffSeidJsonData41> DataList = new List<BuffSeidJsonData41>();

		// Token: 0x040037A1 RID: 14241
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData41.OnInitFinish);

		// Token: 0x040037A2 RID: 14242
		public int id;

		// Token: 0x040037A3 RID: 14243
		public int value1;

		// Token: 0x040037A4 RID: 14244
		public int value2;

		// Token: 0x040037A5 RID: 14245
		public int value3;

		// Token: 0x040037A6 RID: 14246
		public List<int> value4 = new List<int>();
	}
}
