using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000769 RID: 1897
	public class BuffSeidJsonData123 : IJSONClass
	{
		// Token: 0x06003BB8 RID: 15288 RVA: 0x0019AE44 File Offset: 0x00199044
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[123].list)
			{
				try
				{
					BuffSeidJsonData123 buffSeidJsonData = new BuffSeidJsonData123();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData123.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData123.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData123.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData123.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData123.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData123.OnInitFinishAction != null)
			{
				BuffSeidJsonData123.OnInitFinishAction();
			}
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034F0 RID: 13552
		public static int SEIDID = 123;

		// Token: 0x040034F1 RID: 13553
		public static Dictionary<int, BuffSeidJsonData123> DataDict = new Dictionary<int, BuffSeidJsonData123>();

		// Token: 0x040034F2 RID: 13554
		public static List<BuffSeidJsonData123> DataList = new List<BuffSeidJsonData123>();

		// Token: 0x040034F3 RID: 13555
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData123.OnInitFinish);

		// Token: 0x040034F4 RID: 13556
		public int id;

		// Token: 0x040034F5 RID: 13557
		public int value1;
	}
}
