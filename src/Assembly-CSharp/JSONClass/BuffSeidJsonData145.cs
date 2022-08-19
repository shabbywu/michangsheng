using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200077A RID: 1914
	public class BuffSeidJsonData145 : IJSONClass
	{
		// Token: 0x06003BFC RID: 15356 RVA: 0x0019C7C4 File Offset: 0x0019A9C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[145].list)
			{
				try
				{
					BuffSeidJsonData145 buffSeidJsonData = new BuffSeidJsonData145();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData145.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData145.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData145.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData145.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData145.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData145.OnInitFinishAction != null)
			{
				BuffSeidJsonData145.OnInitFinishAction();
			}
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400356B RID: 13675
		public static int SEIDID = 145;

		// Token: 0x0400356C RID: 13676
		public static Dictionary<int, BuffSeidJsonData145> DataDict = new Dictionary<int, BuffSeidJsonData145>();

		// Token: 0x0400356D RID: 13677
		public static List<BuffSeidJsonData145> DataList = new List<BuffSeidJsonData145>();

		// Token: 0x0400356E RID: 13678
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData145.OnInitFinish);

		// Token: 0x0400356F RID: 13679
		public int id;

		// Token: 0x04003570 RID: 13680
		public int value1;

		// Token: 0x04003571 RID: 13681
		public int value2;
	}
}
