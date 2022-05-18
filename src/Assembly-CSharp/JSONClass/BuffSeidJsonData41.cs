using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B63 RID: 2915
	public class BuffSeidJsonData41 : IJSONClass
	{
		// Token: 0x060048F4 RID: 18676 RVA: 0x001F03F0 File Offset: 0x001EE5F0
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

		// Token: 0x060048F5 RID: 18677 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400432E RID: 17198
		public static int SEIDID = 41;

		// Token: 0x0400432F RID: 17199
		public static Dictionary<int, BuffSeidJsonData41> DataDict = new Dictionary<int, BuffSeidJsonData41>();

		// Token: 0x04004330 RID: 17200
		public static List<BuffSeidJsonData41> DataList = new List<BuffSeidJsonData41>();

		// Token: 0x04004331 RID: 17201
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData41.OnInitFinish);

		// Token: 0x04004332 RID: 17202
		public int id;

		// Token: 0x04004333 RID: 17203
		public int value1;

		// Token: 0x04004334 RID: 17204
		public int value2;

		// Token: 0x04004335 RID: 17205
		public int value3;

		// Token: 0x04004336 RID: 17206
		public List<int> value4 = new List<int>();
	}
}
