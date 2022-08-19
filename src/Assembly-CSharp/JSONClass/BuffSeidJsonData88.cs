using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F9 RID: 2041
	public class BuffSeidJsonData88 : IJSONClass
	{
		// Token: 0x06003DF6 RID: 15862 RVA: 0x001A7F18 File Offset: 0x001A6118
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[88].list)
			{
				try
				{
					BuffSeidJsonData88 buffSeidJsonData = new BuffSeidJsonData88();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData88.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData88.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData88.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData88.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData88.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData88.OnInitFinishAction != null)
			{
				BuffSeidJsonData88.OnInitFinishAction();
			}
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038D6 RID: 14550
		public static int SEIDID = 88;

		// Token: 0x040038D7 RID: 14551
		public static Dictionary<int, BuffSeidJsonData88> DataDict = new Dictionary<int, BuffSeidJsonData88>();

		// Token: 0x040038D8 RID: 14552
		public static List<BuffSeidJsonData88> DataList = new List<BuffSeidJsonData88>();

		// Token: 0x040038D9 RID: 14553
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData88.OnInitFinish);

		// Token: 0x040038DA RID: 14554
		public int id;

		// Token: 0x040038DB RID: 14555
		public int value1;

		// Token: 0x040038DC RID: 14556
		public int value2;
	}
}
