using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000768 RID: 1896
	public class BuffSeidJsonData122 : IJSONClass
	{
		// Token: 0x06003BB4 RID: 15284 RVA: 0x0019ACEC File Offset: 0x00198EEC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[122].list)
			{
				try
				{
					BuffSeidJsonData122 buffSeidJsonData = new BuffSeidJsonData122();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData122.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData122.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData122.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData122.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData122.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData122.OnInitFinishAction != null)
			{
				BuffSeidJsonData122.OnInitFinishAction();
			}
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034EA RID: 13546
		public static int SEIDID = 122;

		// Token: 0x040034EB RID: 13547
		public static Dictionary<int, BuffSeidJsonData122> DataDict = new Dictionary<int, BuffSeidJsonData122>();

		// Token: 0x040034EC RID: 13548
		public static List<BuffSeidJsonData122> DataList = new List<BuffSeidJsonData122>();

		// Token: 0x040034ED RID: 13549
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData122.OnInitFinish);

		// Token: 0x040034EE RID: 13550
		public int id;

		// Token: 0x040034EF RID: 13551
		public int value1;
	}
}
