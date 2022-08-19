using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200079E RID: 1950
	public class BuffSeidJsonData192 : IJSONClass
	{
		// Token: 0x06003C8A RID: 15498 RVA: 0x0019FA58 File Offset: 0x0019DC58
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[192].list)
			{
				try
				{
					BuffSeidJsonData192 buffSeidJsonData = new BuffSeidJsonData192();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData192.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData192.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData192.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData192.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData192.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData192.OnInitFinishAction != null)
			{
				BuffSeidJsonData192.OnInitFinishAction();
			}
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003657 RID: 13911
		public static int SEIDID = 192;

		// Token: 0x04003658 RID: 13912
		public static Dictionary<int, BuffSeidJsonData192> DataDict = new Dictionary<int, BuffSeidJsonData192>();

		// Token: 0x04003659 RID: 13913
		public static List<BuffSeidJsonData192> DataList = new List<BuffSeidJsonData192>();

		// Token: 0x0400365A RID: 13914
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData192.OnInitFinish);

		// Token: 0x0400365B RID: 13915
		public int id;

		// Token: 0x0400365C RID: 13916
		public int value1;

		// Token: 0x0400365D RID: 13917
		public int value2;
	}
}
