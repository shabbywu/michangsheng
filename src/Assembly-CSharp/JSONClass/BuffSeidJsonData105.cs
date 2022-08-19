using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200075C RID: 1884
	public class BuffSeidJsonData105 : IJSONClass
	{
		// Token: 0x06003B84 RID: 15236 RVA: 0x00199C28 File Offset: 0x00197E28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[105].list)
			{
				try
				{
					BuffSeidJsonData105 buffSeidJsonData = new BuffSeidJsonData105();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData105.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData105.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData105.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData105.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData105.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData105.OnInitFinishAction != null)
			{
				BuffSeidJsonData105.OnInitFinishAction();
			}
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400349E RID: 13470
		public static int SEIDID = 105;

		// Token: 0x0400349F RID: 13471
		public static Dictionary<int, BuffSeidJsonData105> DataDict = new Dictionary<int, BuffSeidJsonData105>();

		// Token: 0x040034A0 RID: 13472
		public static List<BuffSeidJsonData105> DataList = new List<BuffSeidJsonData105>();

		// Token: 0x040034A1 RID: 13473
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData105.OnInitFinish);

		// Token: 0x040034A2 RID: 13474
		public int id;

		// Token: 0x040034A3 RID: 13475
		public int value1;

		// Token: 0x040034A4 RID: 13476
		public int value2;
	}
}
