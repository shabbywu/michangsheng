using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000773 RID: 1907
	public class BuffSeidJsonData137 : IJSONClass
	{
		// Token: 0x06003BE0 RID: 15328 RVA: 0x0019BD74 File Offset: 0x00199F74
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[137].list)
			{
				try
				{
					BuffSeidJsonData137 buffSeidJsonData = new BuffSeidJsonData137();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData137.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData137.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData137.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData137.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData137.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData137.OnInitFinishAction != null)
			{
				BuffSeidJsonData137.OnInitFinishAction();
			}
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003539 RID: 13625
		public static int SEIDID = 137;

		// Token: 0x0400353A RID: 13626
		public static Dictionary<int, BuffSeidJsonData137> DataDict = new Dictionary<int, BuffSeidJsonData137>();

		// Token: 0x0400353B RID: 13627
		public static List<BuffSeidJsonData137> DataList = new List<BuffSeidJsonData137>();

		// Token: 0x0400353C RID: 13628
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData137.OnInitFinish);

		// Token: 0x0400353D RID: 13629
		public int id;

		// Token: 0x0400353E RID: 13630
		public int value1;
	}
}
