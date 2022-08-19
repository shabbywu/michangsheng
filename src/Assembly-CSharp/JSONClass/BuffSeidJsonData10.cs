using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000757 RID: 1879
	public class BuffSeidJsonData10 : IJSONClass
	{
		// Token: 0x06003B70 RID: 15216 RVA: 0x0019955C File Offset: 0x0019775C
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

		// Token: 0x06003B71 RID: 15217 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400347F RID: 13439
		public static int SEIDID = 10;

		// Token: 0x04003480 RID: 13440
		public static Dictionary<int, BuffSeidJsonData10> DataDict = new Dictionary<int, BuffSeidJsonData10>();

		// Token: 0x04003481 RID: 13441
		public static List<BuffSeidJsonData10> DataList = new List<BuffSeidJsonData10>();

		// Token: 0x04003482 RID: 13442
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData10.OnInitFinish);

		// Token: 0x04003483 RID: 13443
		public int id;

		// Token: 0x04003484 RID: 13444
		public int value1;
	}
}
