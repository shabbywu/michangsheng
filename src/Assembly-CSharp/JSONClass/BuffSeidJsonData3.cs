using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007BC RID: 1980
	public class BuffSeidJsonData3 : IJSONClass
	{
		// Token: 0x06003D02 RID: 15618 RVA: 0x001A2584 File Offset: 0x001A0784
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[3].list)
			{
				try
				{
					BuffSeidJsonData3 buffSeidJsonData = new BuffSeidJsonData3();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData3.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData3.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData3.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData3.OnInitFinishAction != null)
			{
				BuffSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003721 RID: 14113
		public static int SEIDID = 3;

		// Token: 0x04003722 RID: 14114
		public static Dictionary<int, BuffSeidJsonData3> DataDict = new Dictionary<int, BuffSeidJsonData3>();

		// Token: 0x04003723 RID: 14115
		public static List<BuffSeidJsonData3> DataList = new List<BuffSeidJsonData3>();

		// Token: 0x04003724 RID: 14116
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData3.OnInitFinish);

		// Token: 0x04003725 RID: 14117
		public int id;

		// Token: 0x04003726 RID: 14118
		public int value1;
	}
}
