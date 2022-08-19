using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F0 RID: 2032
	public class BuffSeidJsonData79 : IJSONClass
	{
		// Token: 0x06003DD2 RID: 15826 RVA: 0x001A7298 File Offset: 0x001A5498
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[79].list)
			{
				try
				{
					BuffSeidJsonData79 buffSeidJsonData = new BuffSeidJsonData79();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData79.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData79.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData79.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData79.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData79.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData79.OnInitFinishAction != null)
			{
				BuffSeidJsonData79.OnInitFinishAction();
			}
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400389C RID: 14492
		public static int SEIDID = 79;

		// Token: 0x0400389D RID: 14493
		public static Dictionary<int, BuffSeidJsonData79> DataDict = new Dictionary<int, BuffSeidJsonData79>();

		// Token: 0x0400389E RID: 14494
		public static List<BuffSeidJsonData79> DataList = new List<BuffSeidJsonData79>();

		// Token: 0x0400389F RID: 14495
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData79.OnInitFinish);

		// Token: 0x040038A0 RID: 14496
		public int id;

		// Token: 0x040038A1 RID: 14497
		public int value1;
	}
}
