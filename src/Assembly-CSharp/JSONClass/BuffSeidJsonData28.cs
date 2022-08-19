using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007BB RID: 1979
	public class BuffSeidJsonData28 : IJSONClass
	{
		// Token: 0x06003CFE RID: 15614 RVA: 0x001A2418 File Offset: 0x001A0618
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[28].list)
			{
				try
				{
					BuffSeidJsonData28 buffSeidJsonData = new BuffSeidJsonData28();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData28.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData28.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData28.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData28.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData28.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData28.OnInitFinishAction != null)
			{
				BuffSeidJsonData28.OnInitFinishAction();
			}
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400371A RID: 14106
		public static int SEIDID = 28;

		// Token: 0x0400371B RID: 14107
		public static Dictionary<int, BuffSeidJsonData28> DataDict = new Dictionary<int, BuffSeidJsonData28>();

		// Token: 0x0400371C RID: 14108
		public static List<BuffSeidJsonData28> DataList = new List<BuffSeidJsonData28>();

		// Token: 0x0400371D RID: 14109
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData28.OnInitFinish);

		// Token: 0x0400371E RID: 14110
		public int id;

		// Token: 0x0400371F RID: 14111
		public int value1;

		// Token: 0x04003720 RID: 14112
		public int value2;
	}
}
