using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B78 RID: 2936
	public class BuffSeidJsonData62 : IJSONClass
	{
		// Token: 0x06004948 RID: 18760 RVA: 0x001F1F48 File Offset: 0x001F0148
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[62].list)
			{
				try
				{
					BuffSeidJsonData62 buffSeidJsonData = new BuffSeidJsonData62();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData62.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData62.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData62.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData62.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData62.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData62.OnInitFinishAction != null)
			{
				BuffSeidJsonData62.OnInitFinishAction();
			}
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043CA RID: 17354
		public static int SEIDID = 62;

		// Token: 0x040043CB RID: 17355
		public static Dictionary<int, BuffSeidJsonData62> DataDict = new Dictionary<int, BuffSeidJsonData62>();

		// Token: 0x040043CC RID: 17356
		public static List<BuffSeidJsonData62> DataList = new List<BuffSeidJsonData62>();

		// Token: 0x040043CD RID: 17357
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData62.OnInitFinish);

		// Token: 0x040043CE RID: 17358
		public int id;

		// Token: 0x040043CF RID: 17359
		public int value1;
	}
}
