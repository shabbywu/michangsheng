using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F6 RID: 2038
	public class BuffSeidJsonData85 : IJSONClass
	{
		// Token: 0x06003DEA RID: 15850 RVA: 0x001A7AE8 File Offset: 0x001A5CE8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[85].list)
			{
				try
				{
					BuffSeidJsonData85 buffSeidJsonData = new BuffSeidJsonData85();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData85.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData85.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData85.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData85.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData85.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData85.OnInitFinishAction != null)
			{
				BuffSeidJsonData85.OnInitFinishAction();
			}
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038C3 RID: 14531
		public static int SEIDID = 85;

		// Token: 0x040038C4 RID: 14532
		public static Dictionary<int, BuffSeidJsonData85> DataDict = new Dictionary<int, BuffSeidJsonData85>();

		// Token: 0x040038C5 RID: 14533
		public static List<BuffSeidJsonData85> DataList = new List<BuffSeidJsonData85>();

		// Token: 0x040038C6 RID: 14534
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData85.OnInitFinish);

		// Token: 0x040038C7 RID: 14535
		public int id;

		// Token: 0x040038C8 RID: 14536
		public List<int> value1 = new List<int>();
	}
}
