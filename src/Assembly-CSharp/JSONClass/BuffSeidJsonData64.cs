using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E3 RID: 2019
	public class BuffSeidJsonData64 : IJSONClass
	{
		// Token: 0x06003D9E RID: 15774 RVA: 0x001A603C File Offset: 0x001A423C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[64].list)
			{
				try
				{
					BuffSeidJsonData64 buffSeidJsonData = new BuffSeidJsonData64();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData64.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData64.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData64.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData64.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData64.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData64.OnInitFinishAction != null)
			{
				BuffSeidJsonData64.OnInitFinishAction();
			}
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003846 RID: 14406
		public static int SEIDID = 64;

		// Token: 0x04003847 RID: 14407
		public static Dictionary<int, BuffSeidJsonData64> DataDict = new Dictionary<int, BuffSeidJsonData64>();

		// Token: 0x04003848 RID: 14408
		public static List<BuffSeidJsonData64> DataList = new List<BuffSeidJsonData64>();

		// Token: 0x04003849 RID: 14409
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData64.OnInitFinish);

		// Token: 0x0400384A RID: 14410
		public int id;

		// Token: 0x0400384B RID: 14411
		public int value1;

		// Token: 0x0400384C RID: 14412
		public int value2;
	}
}
