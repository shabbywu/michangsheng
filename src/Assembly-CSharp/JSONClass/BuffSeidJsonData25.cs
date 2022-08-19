using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B8 RID: 1976
	public class BuffSeidJsonData25 : IJSONClass
	{
		// Token: 0x06003CF2 RID: 15602 RVA: 0x001A1FD4 File Offset: 0x001A01D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[25].list)
			{
				try
				{
					BuffSeidJsonData25 buffSeidJsonData = new BuffSeidJsonData25();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData25.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData25.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData25.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData25.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData25.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData25.OnInitFinishAction != null)
			{
				BuffSeidJsonData25.OnInitFinishAction();
			}
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003705 RID: 14085
		public static int SEIDID = 25;

		// Token: 0x04003706 RID: 14086
		public static Dictionary<int, BuffSeidJsonData25> DataDict = new Dictionary<int, BuffSeidJsonData25>();

		// Token: 0x04003707 RID: 14087
		public static List<BuffSeidJsonData25> DataList = new List<BuffSeidJsonData25>();

		// Token: 0x04003708 RID: 14088
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData25.OnInitFinish);

		// Token: 0x04003709 RID: 14089
		public int id;

		// Token: 0x0400370A RID: 14090
		public int value1;

		// Token: 0x0400370B RID: 14091
		public int value2;
	}
}
