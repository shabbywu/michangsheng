using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007BE RID: 1982
	public class BuffSeidJsonData31 : IJSONClass
	{
		// Token: 0x06003D0A RID: 15626 RVA: 0x001A2848 File Offset: 0x001A0A48
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[31].list)
			{
				try
				{
					BuffSeidJsonData31 buffSeidJsonData = new BuffSeidJsonData31();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData31.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData31.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData31.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData31.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData31.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData31.OnInitFinishAction != null)
			{
				BuffSeidJsonData31.OnInitFinishAction();
			}
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400372E RID: 14126
		public static int SEIDID = 31;

		// Token: 0x0400372F RID: 14127
		public static Dictionary<int, BuffSeidJsonData31> DataDict = new Dictionary<int, BuffSeidJsonData31>();

		// Token: 0x04003730 RID: 14128
		public static List<BuffSeidJsonData31> DataList = new List<BuffSeidJsonData31>();

		// Token: 0x04003731 RID: 14129
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData31.OnInitFinish);

		// Token: 0x04003732 RID: 14130
		public int id;

		// Token: 0x04003733 RID: 14131
		public int value2;

		// Token: 0x04003734 RID: 14132
		public List<int> value1 = new List<int>();
	}
}
