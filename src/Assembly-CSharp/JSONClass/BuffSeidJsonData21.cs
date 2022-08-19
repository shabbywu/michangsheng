using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007AE RID: 1966
	public class BuffSeidJsonData21 : IJSONClass
	{
		// Token: 0x06003CCA RID: 15562 RVA: 0x001A1144 File Offset: 0x0019F344
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[21].list)
			{
				try
				{
					BuffSeidJsonData21 buffSeidJsonData = new BuffSeidJsonData21();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData21.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData21.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData21.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData21.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData21.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData21.OnInitFinishAction != null)
			{
				BuffSeidJsonData21.OnInitFinishAction();
			}
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036C1 RID: 14017
		public static int SEIDID = 21;

		// Token: 0x040036C2 RID: 14018
		public static Dictionary<int, BuffSeidJsonData21> DataDict = new Dictionary<int, BuffSeidJsonData21>();

		// Token: 0x040036C3 RID: 14019
		public static List<BuffSeidJsonData21> DataList = new List<BuffSeidJsonData21>();

		// Token: 0x040036C4 RID: 14020
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData21.OnInitFinish);

		// Token: 0x040036C5 RID: 14021
		public int id;

		// Token: 0x040036C6 RID: 14022
		public int value1;
	}
}
