using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B71 RID: 2929
	public class BuffSeidJsonData55 : IJSONClass
	{
		// Token: 0x0600492C RID: 18732 RVA: 0x001F171C File Offset: 0x001EF91C
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

		// Token: 0x0600492D RID: 18733 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400439F RID: 17311
		public static int SEIDID = 55;

		// Token: 0x040043A0 RID: 17312
		public static Dictionary<int, BuffSeidJsonData55> DataDict = new Dictionary<int, BuffSeidJsonData55>();

		// Token: 0x040043A1 RID: 17313
		public static List<BuffSeidJsonData55> DataList = new List<BuffSeidJsonData55>();

		// Token: 0x040043A2 RID: 17314
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData55.OnInitFinish);

		// Token: 0x040043A3 RID: 17315
		public int id;

		// Token: 0x040043A4 RID: 17316
		public int value1;
	}
}
