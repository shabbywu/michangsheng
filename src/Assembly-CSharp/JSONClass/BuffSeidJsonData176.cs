using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000793 RID: 1939
	public class BuffSeidJsonData176 : IJSONClass
	{
		// Token: 0x06003C5E RID: 15454 RVA: 0x0019EA90 File Offset: 0x0019CC90
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[176].list)
			{
				try
				{
					BuffSeidJsonData176 buffSeidJsonData = new BuffSeidJsonData176();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData176.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData176.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData176.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData176.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData176.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData176.OnInitFinishAction != null)
			{
				BuffSeidJsonData176.OnInitFinishAction();
			}
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400360F RID: 13839
		public static int SEIDID = 176;

		// Token: 0x04003610 RID: 13840
		public static Dictionary<int, BuffSeidJsonData176> DataDict = new Dictionary<int, BuffSeidJsonData176>();

		// Token: 0x04003611 RID: 13841
		public static List<BuffSeidJsonData176> DataList = new List<BuffSeidJsonData176>();

		// Token: 0x04003612 RID: 13842
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData176.OnInitFinish);

		// Token: 0x04003613 RID: 13843
		public int id;

		// Token: 0x04003614 RID: 13844
		public int target;

		// Token: 0x04003615 RID: 13845
		public int value1;
	}
}
