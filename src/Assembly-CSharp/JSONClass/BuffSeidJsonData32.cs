using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C3 RID: 1987
	public class BuffSeidJsonData32 : IJSONClass
	{
		// Token: 0x06003D1E RID: 15646 RVA: 0x001A2FB0 File Offset: 0x001A11B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[32].list)
			{
				try
				{
					BuffSeidJsonData32 buffSeidJsonData = new BuffSeidJsonData32();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData32.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData32.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData32.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData32.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData32.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData32.OnInitFinishAction != null)
			{
				BuffSeidJsonData32.OnInitFinishAction();
			}
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003752 RID: 14162
		public static int SEIDID = 32;

		// Token: 0x04003753 RID: 14163
		public static Dictionary<int, BuffSeidJsonData32> DataDict = new Dictionary<int, BuffSeidJsonData32>();

		// Token: 0x04003754 RID: 14164
		public static List<BuffSeidJsonData32> DataList = new List<BuffSeidJsonData32>();

		// Token: 0x04003755 RID: 14165
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData32.OnInitFinish);

		// Token: 0x04003756 RID: 14166
		public int id;

		// Token: 0x04003757 RID: 14167
		public int value1;

		// Token: 0x04003758 RID: 14168
		public int value2;

		// Token: 0x04003759 RID: 14169
		public int value3;
	}
}
