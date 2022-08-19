using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000789 RID: 1929
	public class BuffSeidJsonData166 : IJSONClass
	{
		// Token: 0x06003C36 RID: 15414 RVA: 0x0019DC2C File Offset: 0x0019BE2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[166].list)
			{
				try
				{
					BuffSeidJsonData166 buffSeidJsonData = new BuffSeidJsonData166();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData166.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData166.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData166.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData166.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData166.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData166.OnInitFinishAction != null)
			{
				BuffSeidJsonData166.OnInitFinishAction();
			}
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035CC RID: 13772
		public static int SEIDID = 166;

		// Token: 0x040035CD RID: 13773
		public static Dictionary<int, BuffSeidJsonData166> DataDict = new Dictionary<int, BuffSeidJsonData166>();

		// Token: 0x040035CE RID: 13774
		public static List<BuffSeidJsonData166> DataList = new List<BuffSeidJsonData166>();

		// Token: 0x040035CF RID: 13775
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData166.OnInitFinish);

		// Token: 0x040035D0 RID: 13776
		public int id;

		// Token: 0x040035D1 RID: 13777
		public int value1;
	}
}
