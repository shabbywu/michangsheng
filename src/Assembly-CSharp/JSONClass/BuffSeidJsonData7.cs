using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E9 RID: 2025
	public class BuffSeidJsonData7 : IJSONClass
	{
		// Token: 0x06003DB6 RID: 15798 RVA: 0x001A68E0 File Offset: 0x001A4AE0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[7].list)
			{
				try
				{
					BuffSeidJsonData7 buffSeidJsonData = new BuffSeidJsonData7();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData7.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData7.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData7.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData7.OnInitFinishAction != null)
			{
				BuffSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003870 RID: 14448
		public static int SEIDID = 7;

		// Token: 0x04003871 RID: 14449
		public static Dictionary<int, BuffSeidJsonData7> DataDict = new Dictionary<int, BuffSeidJsonData7>();

		// Token: 0x04003872 RID: 14450
		public static List<BuffSeidJsonData7> DataList = new List<BuffSeidJsonData7>();

		// Token: 0x04003873 RID: 14451
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData7.OnInitFinish);

		// Token: 0x04003874 RID: 14452
		public int id;

		// Token: 0x04003875 RID: 14453
		public int value1;
	}
}
