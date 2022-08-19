using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007DA RID: 2010
	public class BuffSeidJsonData55 : IJSONClass
	{
		// Token: 0x06003D7A RID: 15738 RVA: 0x001A5400 File Offset: 0x001A3600
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[55].list)
			{
				try
				{
					BuffSeidJsonData55 buffSeidJsonData = new BuffSeidJsonData55();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData55.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData55.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData55.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData55.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData55.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData55.OnInitFinishAction != null)
			{
				BuffSeidJsonData55.OnInitFinishAction();
			}
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400380F RID: 14351
		public static int SEIDID = 55;

		// Token: 0x04003810 RID: 14352
		public static Dictionary<int, BuffSeidJsonData55> DataDict = new Dictionary<int, BuffSeidJsonData55>();

		// Token: 0x04003811 RID: 14353
		public static List<BuffSeidJsonData55> DataList = new List<BuffSeidJsonData55>();

		// Token: 0x04003812 RID: 14354
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData55.OnInitFinish);

		// Token: 0x04003813 RID: 14355
		public int id;

		// Token: 0x04003814 RID: 14356
		public int value1;
	}
}
