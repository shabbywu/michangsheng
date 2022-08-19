using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200076D RID: 1901
	public class BuffSeidJsonData130 : IJSONClass
	{
		// Token: 0x06003BC8 RID: 15304 RVA: 0x0019B414 File Offset: 0x00199614
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[130].list)
			{
				try
				{
					BuffSeidJsonData130 buffSeidJsonData = new BuffSeidJsonData130();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					buffSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (BuffSeidJsonData130.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData130.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData130.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData130.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData130.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData130.OnInitFinishAction != null)
			{
				BuffSeidJsonData130.OnInitFinishAction();
			}
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400350B RID: 13579
		public static int SEIDID = 130;

		// Token: 0x0400350C RID: 13580
		public static Dictionary<int, BuffSeidJsonData130> DataDict = new Dictionary<int, BuffSeidJsonData130>();

		// Token: 0x0400350D RID: 13581
		public static List<BuffSeidJsonData130> DataList = new List<BuffSeidJsonData130>();

		// Token: 0x0400350E RID: 13582
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData130.OnInitFinish);

		// Token: 0x0400350F RID: 13583
		public int id;

		// Token: 0x04003510 RID: 13584
		public int value1;

		// Token: 0x04003511 RID: 13585
		public List<int> value2 = new List<int>();

		// Token: 0x04003512 RID: 13586
		public List<int> value3 = new List<int>();
	}
}
