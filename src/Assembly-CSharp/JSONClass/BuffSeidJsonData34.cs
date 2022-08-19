using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C5 RID: 1989
	public class BuffSeidJsonData34 : IJSONClass
	{
		// Token: 0x06003D26 RID: 15654 RVA: 0x001A32B8 File Offset: 0x001A14B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[34].list)
			{
				try
				{
					BuffSeidJsonData34 buffSeidJsonData = new BuffSeidJsonData34();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData34.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData34.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData34.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData34.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData34.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData34.OnInitFinishAction != null)
			{
				BuffSeidJsonData34.OnInitFinishAction();
			}
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003762 RID: 14178
		public static int SEIDID = 34;

		// Token: 0x04003763 RID: 14179
		public static Dictionary<int, BuffSeidJsonData34> DataDict = new Dictionary<int, BuffSeidJsonData34>();

		// Token: 0x04003764 RID: 14180
		public static List<BuffSeidJsonData34> DataList = new List<BuffSeidJsonData34>();

		// Token: 0x04003765 RID: 14181
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData34.OnInitFinish);

		// Token: 0x04003766 RID: 14182
		public int id;

		// Token: 0x04003767 RID: 14183
		public int value1;

		// Token: 0x04003768 RID: 14184
		public int value2;

		// Token: 0x04003769 RID: 14185
		public int value3;

		// Token: 0x0400376A RID: 14186
		public int value4;
	}
}
