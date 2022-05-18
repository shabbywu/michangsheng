using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B83 RID: 2947
	public class BuffSeidJsonData72 : IJSONClass
	{
		// Token: 0x06004974 RID: 18804 RVA: 0x001F2CA8 File Offset: 0x001F0EA8
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

		// Token: 0x06004975 RID: 18805 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004413 RID: 17427
		public static int SEIDID = 72;

		// Token: 0x04004414 RID: 17428
		public static Dictionary<int, BuffSeidJsonData72> DataDict = new Dictionary<int, BuffSeidJsonData72>();

		// Token: 0x04004415 RID: 17429
		public static List<BuffSeidJsonData72> DataList = new List<BuffSeidJsonData72>();

		// Token: 0x04004416 RID: 17430
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData72.OnInitFinish);

		// Token: 0x04004417 RID: 17431
		public int id;

		// Token: 0x04004418 RID: 17432
		public int value1;

		// Token: 0x04004419 RID: 17433
		public int value2;
	}
}
