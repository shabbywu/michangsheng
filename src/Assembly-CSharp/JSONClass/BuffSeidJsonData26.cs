using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B9 RID: 1977
	public class BuffSeidJsonData26 : IJSONClass
	{
		// Token: 0x06003CF6 RID: 15606 RVA: 0x001A2140 File Offset: 0x001A0340
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[26].list)
			{
				try
				{
					BuffSeidJsonData26 buffSeidJsonData = new BuffSeidJsonData26();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData26.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData26.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData26.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData26.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData26.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData26.OnInitFinishAction != null)
			{
				BuffSeidJsonData26.OnInitFinishAction();
			}
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400370C RID: 14092
		public static int SEIDID = 26;

		// Token: 0x0400370D RID: 14093
		public static Dictionary<int, BuffSeidJsonData26> DataDict = new Dictionary<int, BuffSeidJsonData26>();

		// Token: 0x0400370E RID: 14094
		public static List<BuffSeidJsonData26> DataList = new List<BuffSeidJsonData26>();

		// Token: 0x0400370F RID: 14095
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData26.OnInitFinish);

		// Token: 0x04003710 RID: 14096
		public int id;

		// Token: 0x04003711 RID: 14097
		public int value1;

		// Token: 0x04003712 RID: 14098
		public int value2;
	}
}
