using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B4F RID: 2895
	public class BuffSeidJsonData25 : IJSONClass
	{
		// Token: 0x060048A4 RID: 18596 RVA: 0x001EE9A4 File Offset: 0x001ECBA4
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

		// Token: 0x060048A5 RID: 18597 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004295 RID: 17045
		public static int SEIDID = 25;

		// Token: 0x04004296 RID: 17046
		public static Dictionary<int, BuffSeidJsonData25> DataDict = new Dictionary<int, BuffSeidJsonData25>();

		// Token: 0x04004297 RID: 17047
		public static List<BuffSeidJsonData25> DataList = new List<BuffSeidJsonData25>();

		// Token: 0x04004298 RID: 17048
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData25.OnInitFinish);

		// Token: 0x04004299 RID: 17049
		public int id;

		// Token: 0x0400429A RID: 17050
		public int value1;

		// Token: 0x0400429B RID: 17051
		public int value2;
	}
}
