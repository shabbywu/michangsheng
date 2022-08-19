using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007DE RID: 2014
	public class BuffSeidJsonData6 : IJSONClass
	{
		// Token: 0x06003D8A RID: 15754 RVA: 0x001A5974 File Offset: 0x001A3B74
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[6].list)
			{
				try
				{
					BuffSeidJsonData6 buffSeidJsonData = new BuffSeidJsonData6();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData6.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData6.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData6.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData6.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData6.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData6.OnInitFinishAction != null)
			{
				BuffSeidJsonData6.OnInitFinishAction();
			}
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003828 RID: 14376
		public static int SEIDID = 6;

		// Token: 0x04003829 RID: 14377
		public static Dictionary<int, BuffSeidJsonData6> DataDict = new Dictionary<int, BuffSeidJsonData6>();

		// Token: 0x0400382A RID: 14378
		public static List<BuffSeidJsonData6> DataList = new List<BuffSeidJsonData6>();

		// Token: 0x0400382B RID: 14379
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData6.OnInitFinish);

		// Token: 0x0400382C RID: 14380
		public int id;

		// Token: 0x0400382D RID: 14381
		public List<int> value1 = new List<int>();
	}
}
