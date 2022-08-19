using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200077F RID: 1919
	public class BuffSeidJsonData150 : IJSONClass
	{
		// Token: 0x06003C10 RID: 15376 RVA: 0x0019CED4 File Offset: 0x0019B0D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[150].list)
			{
				try
				{
					BuffSeidJsonData150 buffSeidJsonData = new BuffSeidJsonData150();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData150.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData150.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData150.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData150.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData150.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData150.OnInitFinishAction != null)
			{
				BuffSeidJsonData150.OnInitFinishAction();
			}
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400358B RID: 13707
		public static int SEIDID = 150;

		// Token: 0x0400358C RID: 13708
		public static Dictionary<int, BuffSeidJsonData150> DataDict = new Dictionary<int, BuffSeidJsonData150>();

		// Token: 0x0400358D RID: 13709
		public static List<BuffSeidJsonData150> DataList = new List<BuffSeidJsonData150>();

		// Token: 0x0400358E RID: 13710
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData150.OnInitFinish);

		// Token: 0x0400358F RID: 13711
		public int id;

		// Token: 0x04003590 RID: 13712
		public int value1;
	}
}
