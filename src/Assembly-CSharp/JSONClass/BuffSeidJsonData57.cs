using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007DC RID: 2012
	public class BuffSeidJsonData57 : IJSONClass
	{
		// Token: 0x06003D82 RID: 15746 RVA: 0x001A56B0 File Offset: 0x001A38B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[57].list)
			{
				try
				{
					BuffSeidJsonData57 buffSeidJsonData = new BuffSeidJsonData57();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData57.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData57.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData57.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData57.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData57.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData57.OnInitFinishAction != null)
			{
				BuffSeidJsonData57.OnInitFinishAction();
			}
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400381B RID: 14363
		public static int SEIDID = 57;

		// Token: 0x0400381C RID: 14364
		public static Dictionary<int, BuffSeidJsonData57> DataDict = new Dictionary<int, BuffSeidJsonData57>();

		// Token: 0x0400381D RID: 14365
		public static List<BuffSeidJsonData57> DataList = new List<BuffSeidJsonData57>();

		// Token: 0x0400381E RID: 14366
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData57.OnInitFinish);

		// Token: 0x0400381F RID: 14367
		public int id;

		// Token: 0x04003820 RID: 14368
		public int value1;

		// Token: 0x04003821 RID: 14369
		public int value2;
	}
}
