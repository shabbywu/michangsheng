using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007EC RID: 2028
	public class BuffSeidJsonData72 : IJSONClass
	{
		// Token: 0x06003DC2 RID: 15810 RVA: 0x001A6D10 File Offset: 0x001A4F10
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[72].list)
			{
				try
				{
					BuffSeidJsonData72 buffSeidJsonData = new BuffSeidJsonData72();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData72.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData72.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData72.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData72.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData72.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData72.OnInitFinishAction != null)
			{
				BuffSeidJsonData72.OnInitFinishAction();
			}
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003883 RID: 14467
		public static int SEIDID = 72;

		// Token: 0x04003884 RID: 14468
		public static Dictionary<int, BuffSeidJsonData72> DataDict = new Dictionary<int, BuffSeidJsonData72>();

		// Token: 0x04003885 RID: 14469
		public static List<BuffSeidJsonData72> DataList = new List<BuffSeidJsonData72>();

		// Token: 0x04003886 RID: 14470
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData72.OnInitFinish);

		// Token: 0x04003887 RID: 14471
		public int id;

		// Token: 0x04003888 RID: 14472
		public int value1;

		// Token: 0x04003889 RID: 14473
		public int value2;
	}
}
