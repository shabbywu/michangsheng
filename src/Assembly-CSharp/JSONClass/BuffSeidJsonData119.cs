using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000767 RID: 1895
	public class BuffSeidJsonData119 : IJSONClass
	{
		// Token: 0x06003BB0 RID: 15280 RVA: 0x0019AB68 File Offset: 0x00198D68
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[119].list)
			{
				try
				{
					BuffSeidJsonData119 buffSeidJsonData = new BuffSeidJsonData119();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData119.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData119.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData119.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData119.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData119.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData119.OnInitFinishAction != null)
			{
				BuffSeidJsonData119.OnInitFinishAction();
			}
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034E2 RID: 13538
		public static int SEIDID = 119;

		// Token: 0x040034E3 RID: 13539
		public static Dictionary<int, BuffSeidJsonData119> DataDict = new Dictionary<int, BuffSeidJsonData119>();

		// Token: 0x040034E4 RID: 13540
		public static List<BuffSeidJsonData119> DataList = new List<BuffSeidJsonData119>();

		// Token: 0x040034E5 RID: 13541
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData119.OnInitFinish);

		// Token: 0x040034E6 RID: 13542
		public int id;

		// Token: 0x040034E7 RID: 13543
		public int target;

		// Token: 0x040034E8 RID: 13544
		public int value1;

		// Token: 0x040034E9 RID: 13545
		public int value2;
	}
}
