using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A8 RID: 1960
	public class BuffSeidJsonData203 : IJSONClass
	{
		// Token: 0x06003CB2 RID: 15538 RVA: 0x001A08C4 File Offset: 0x0019EAC4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[203].list)
			{
				try
				{
					BuffSeidJsonData203 buffSeidJsonData = new BuffSeidJsonData203();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData203.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData203.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData203.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData203.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData203.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData203.OnInitFinishAction != null)
			{
				BuffSeidJsonData203.OnInitFinishAction();
			}
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400369A RID: 13978
		public static int SEIDID = 203;

		// Token: 0x0400369B RID: 13979
		public static Dictionary<int, BuffSeidJsonData203> DataDict = new Dictionary<int, BuffSeidJsonData203>();

		// Token: 0x0400369C RID: 13980
		public static List<BuffSeidJsonData203> DataList = new List<BuffSeidJsonData203>();

		// Token: 0x0400369D RID: 13981
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData203.OnInitFinish);

		// Token: 0x0400369E RID: 13982
		public int id;

		// Token: 0x0400369F RID: 13983
		public int value1;
	}
}
