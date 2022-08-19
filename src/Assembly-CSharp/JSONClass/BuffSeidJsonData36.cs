using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C7 RID: 1991
	public class BuffSeidJsonData36 : IJSONClass
	{
		// Token: 0x06003D2E RID: 15662 RVA: 0x001A3618 File Offset: 0x001A1818
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[36].list)
			{
				try
				{
					BuffSeidJsonData36 buffSeidJsonData = new BuffSeidJsonData36();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData36.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData36.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData36.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData36.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData36.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData36.OnInitFinishAction != null)
			{
				BuffSeidJsonData36.OnInitFinishAction();
			}
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003774 RID: 14196
		public static int SEIDID = 36;

		// Token: 0x04003775 RID: 14197
		public static Dictionary<int, BuffSeidJsonData36> DataDict = new Dictionary<int, BuffSeidJsonData36>();

		// Token: 0x04003776 RID: 14198
		public static List<BuffSeidJsonData36> DataList = new List<BuffSeidJsonData36>();

		// Token: 0x04003777 RID: 14199
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData36.OnInitFinish);

		// Token: 0x04003778 RID: 14200
		public int id;

		// Token: 0x04003779 RID: 14201
		public int value1;

		// Token: 0x0400377A RID: 14202
		public int value2;

		// Token: 0x0400377B RID: 14203
		public int value3;
	}
}
