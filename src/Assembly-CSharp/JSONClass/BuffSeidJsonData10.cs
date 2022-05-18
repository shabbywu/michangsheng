using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AEF RID: 2799
	public class BuffSeidJsonData10 : IJSONClass
	{
		// Token: 0x06004726 RID: 18214 RVA: 0x001E7508 File Offset: 0x001E5708
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[10].list)
			{
				try
				{
					BuffSeidJsonData10 buffSeidJsonData = new BuffSeidJsonData10();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData10.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData10.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData10.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData10.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData10.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData10.OnInitFinishAction != null)
			{
				BuffSeidJsonData10.OnInitFinishAction();
			}
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004018 RID: 16408
		public static int SEIDID = 10;

		// Token: 0x04004019 RID: 16409
		public static Dictionary<int, BuffSeidJsonData10> DataDict = new Dictionary<int, BuffSeidJsonData10>();

		// Token: 0x0400401A RID: 16410
		public static List<BuffSeidJsonData10> DataList = new List<BuffSeidJsonData10>();

		// Token: 0x0400401B RID: 16411
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData10.OnInitFinish);

		// Token: 0x0400401C RID: 16412
		public int id;

		// Token: 0x0400401D RID: 16413
		public int value1;
	}
}
