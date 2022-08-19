using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007DD RID: 2013
	public class BuffSeidJsonData58 : IJSONClass
	{
		// Token: 0x06003D86 RID: 15750 RVA: 0x001A581C File Offset: 0x001A3A1C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[58].list)
			{
				try
				{
					BuffSeidJsonData58 buffSeidJsonData = new BuffSeidJsonData58();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData58.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData58.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData58.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData58.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData58.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData58.OnInitFinishAction != null)
			{
				BuffSeidJsonData58.OnInitFinishAction();
			}
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003822 RID: 14370
		public static int SEIDID = 58;

		// Token: 0x04003823 RID: 14371
		public static Dictionary<int, BuffSeidJsonData58> DataDict = new Dictionary<int, BuffSeidJsonData58>();

		// Token: 0x04003824 RID: 14372
		public static List<BuffSeidJsonData58> DataList = new List<BuffSeidJsonData58>();

		// Token: 0x04003825 RID: 14373
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData58.OnInitFinish);

		// Token: 0x04003826 RID: 14374
		public int id;

		// Token: 0x04003827 RID: 14375
		public int value1;
	}
}
