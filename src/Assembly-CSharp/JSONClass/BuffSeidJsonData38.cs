using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C9 RID: 1993
	public class BuffSeidJsonData38 : IJSONClass
	{
		// Token: 0x06003D36 RID: 15670 RVA: 0x001A394C File Offset: 0x001A1B4C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[38].list)
			{
				try
				{
					BuffSeidJsonData38 buffSeidJsonData = new BuffSeidJsonData38();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData38.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData38.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData38.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData38.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData38.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData38.OnInitFinishAction != null)
			{
				BuffSeidJsonData38.OnInitFinishAction();
			}
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003785 RID: 14213
		public static int SEIDID = 38;

		// Token: 0x04003786 RID: 14214
		public static Dictionary<int, BuffSeidJsonData38> DataDict = new Dictionary<int, BuffSeidJsonData38>();

		// Token: 0x04003787 RID: 14215
		public static List<BuffSeidJsonData38> DataList = new List<BuffSeidJsonData38>();

		// Token: 0x04003788 RID: 14216
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData38.OnInitFinish);

		// Token: 0x04003789 RID: 14217
		public int id;

		// Token: 0x0400378A RID: 14218
		public int value1;

		// Token: 0x0400378B RID: 14219
		public int value2;

		// Token: 0x0400378C RID: 14220
		public int value3;
	}
}
