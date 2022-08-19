using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C0 RID: 1984
	public class BuffSeidJsonData313 : IJSONClass
	{
		// Token: 0x06003D12 RID: 15634 RVA: 0x001A2B3C File Offset: 0x001A0D3C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[313].list)
			{
				try
				{
					BuffSeidJsonData313 buffSeidJsonData = new BuffSeidJsonData313();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData313.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData313.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData313.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData313.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData313.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData313.OnInitFinishAction != null)
			{
				BuffSeidJsonData313.OnInitFinishAction();
			}
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400373C RID: 14140
		public static int SEIDID = 313;

		// Token: 0x0400373D RID: 14141
		public static Dictionary<int, BuffSeidJsonData313> DataDict = new Dictionary<int, BuffSeidJsonData313>();

		// Token: 0x0400373E RID: 14142
		public static List<BuffSeidJsonData313> DataList = new List<BuffSeidJsonData313>();

		// Token: 0x0400373F RID: 14143
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData313.OnInitFinish);

		// Token: 0x04003740 RID: 14144
		public int id;

		// Token: 0x04003741 RID: 14145
		public int target;

		// Token: 0x04003742 RID: 14146
		public int value1;
	}
}
