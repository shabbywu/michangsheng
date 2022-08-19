using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C2 RID: 1986
	public class BuffSeidJsonData315 : IJSONClass
	{
		// Token: 0x06003D1A RID: 15642 RVA: 0x001A2E3C File Offset: 0x001A103C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[315].list)
			{
				try
				{
					BuffSeidJsonData315 buffSeidJsonData = new BuffSeidJsonData315();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData315.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData315.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData315.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData315.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData315.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData315.OnInitFinishAction != null)
			{
				BuffSeidJsonData315.OnInitFinishAction();
			}
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400374B RID: 14155
		public static int SEIDID = 315;

		// Token: 0x0400374C RID: 14156
		public static Dictionary<int, BuffSeidJsonData315> DataDict = new Dictionary<int, BuffSeidJsonData315>();

		// Token: 0x0400374D RID: 14157
		public static List<BuffSeidJsonData315> DataList = new List<BuffSeidJsonData315>();

		// Token: 0x0400374E RID: 14158
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData315.OnInitFinish);

		// Token: 0x0400374F RID: 14159
		public int id;

		// Token: 0x04003750 RID: 14160
		public int value1;

		// Token: 0x04003751 RID: 14161
		public int value2;
	}
}
