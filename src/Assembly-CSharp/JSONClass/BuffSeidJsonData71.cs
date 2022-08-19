using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007EB RID: 2027
	public class BuffSeidJsonData71 : IJSONClass
	{
		// Token: 0x06003DBE RID: 15806 RVA: 0x001A6BB8 File Offset: 0x001A4DB8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[71].list)
			{
				try
				{
					BuffSeidJsonData71 buffSeidJsonData = new BuffSeidJsonData71();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData71.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData71.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData71.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData71.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData71.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData71.OnInitFinishAction != null)
			{
				BuffSeidJsonData71.OnInitFinishAction();
			}
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400387D RID: 14461
		public static int SEIDID = 71;

		// Token: 0x0400387E RID: 14462
		public static Dictionary<int, BuffSeidJsonData71> DataDict = new Dictionary<int, BuffSeidJsonData71>();

		// Token: 0x0400387F RID: 14463
		public static List<BuffSeidJsonData71> DataList = new List<BuffSeidJsonData71>();

		// Token: 0x04003880 RID: 14464
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData71.OnInitFinish);

		// Token: 0x04003881 RID: 14465
		public int id;

		// Token: 0x04003882 RID: 14466
		public int value1;
	}
}
