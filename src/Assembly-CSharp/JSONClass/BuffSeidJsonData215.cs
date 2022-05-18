using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B4B RID: 2891
	public class BuffSeidJsonData215 : IJSONClass
	{
		// Token: 0x06004894 RID: 18580 RVA: 0x001EE4A4 File Offset: 0x001EC6A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[215].list)
			{
				try
				{
					BuffSeidJsonData215 buffSeidJsonData = new BuffSeidJsonData215();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData215.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData215.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData215.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData215.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData215.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData215.OnInitFinishAction != null)
			{
				BuffSeidJsonData215.OnInitFinishAction();
			}
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400427A RID: 17018
		public static int SEIDID = 215;

		// Token: 0x0400427B RID: 17019
		public static Dictionary<int, BuffSeidJsonData215> DataDict = new Dictionary<int, BuffSeidJsonData215>();

		// Token: 0x0400427C RID: 17020
		public static List<BuffSeidJsonData215> DataList = new List<BuffSeidJsonData215>();

		// Token: 0x0400427D RID: 17021
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData215.OnInitFinish);

		// Token: 0x0400427E RID: 17022
		public int id;

		// Token: 0x0400427F RID: 17023
		public int value1;

		// Token: 0x04004280 RID: 17024
		public int value2;

		// Token: 0x04004281 RID: 17025
		public int value3;

		// Token: 0x04004282 RID: 17026
		public int value4;
	}
}
