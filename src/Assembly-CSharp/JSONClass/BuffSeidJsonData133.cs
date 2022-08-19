using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200076F RID: 1903
	public class BuffSeidJsonData133 : IJSONClass
	{
		// Token: 0x06003BD0 RID: 15312 RVA: 0x0019B764 File Offset: 0x00199964
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[133].list)
			{
				try
				{
					BuffSeidJsonData133 buffSeidJsonData = new BuffSeidJsonData133();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData133.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData133.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData133.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData133.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData133.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData133.OnInitFinishAction != null)
			{
				BuffSeidJsonData133.OnInitFinishAction();
			}
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400351B RID: 13595
		public static int SEIDID = 133;

		// Token: 0x0400351C RID: 13596
		public static Dictionary<int, BuffSeidJsonData133> DataDict = new Dictionary<int, BuffSeidJsonData133>();

		// Token: 0x0400351D RID: 13597
		public static List<BuffSeidJsonData133> DataList = new List<BuffSeidJsonData133>();

		// Token: 0x0400351E RID: 13598
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData133.OnInitFinish);

		// Token: 0x0400351F RID: 13599
		public int id;

		// Token: 0x04003520 RID: 13600
		public int target;

		// Token: 0x04003521 RID: 13601
		public int value1;

		// Token: 0x04003522 RID: 13602
		public int value3;
	}
}
