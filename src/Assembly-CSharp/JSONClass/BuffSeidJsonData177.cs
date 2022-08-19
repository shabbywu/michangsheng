using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000794 RID: 1940
	public class BuffSeidJsonData177 : IJSONClass
	{
		// Token: 0x06003C62 RID: 15458 RVA: 0x0019EC04 File Offset: 0x0019CE04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[177].list)
			{
				try
				{
					BuffSeidJsonData177 buffSeidJsonData = new BuffSeidJsonData177();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData177.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData177.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData177.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData177.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData177.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData177.OnInitFinishAction != null)
			{
				BuffSeidJsonData177.OnInitFinishAction();
			}
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003616 RID: 13846
		public static int SEIDID = 177;

		// Token: 0x04003617 RID: 13847
		public static Dictionary<int, BuffSeidJsonData177> DataDict = new Dictionary<int, BuffSeidJsonData177>();

		// Token: 0x04003618 RID: 13848
		public static List<BuffSeidJsonData177> DataList = new List<BuffSeidJsonData177>();

		// Token: 0x04003619 RID: 13849
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData177.OnInitFinish);

		// Token: 0x0400361A RID: 13850
		public int id;

		// Token: 0x0400361B RID: 13851
		public int value1;

		// Token: 0x0400361C RID: 13852
		public int value2;
	}
}
