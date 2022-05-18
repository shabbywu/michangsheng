using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B64 RID: 2916
	public class BuffSeidJsonData42 : IJSONClass
	{
		// Token: 0x060048F8 RID: 18680 RVA: 0x001F0570 File Offset: 0x001EE770
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[42].list)
			{
				try
				{
					BuffSeidJsonData42 buffSeidJsonData = new BuffSeidJsonData42();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData42.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData42.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData42.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData42.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData42.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData42.OnInitFinishAction != null)
			{
				BuffSeidJsonData42.OnInitFinishAction();
			}
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004337 RID: 17207
		public static int SEIDID = 42;

		// Token: 0x04004338 RID: 17208
		public static Dictionary<int, BuffSeidJsonData42> DataDict = new Dictionary<int, BuffSeidJsonData42>();

		// Token: 0x04004339 RID: 17209
		public static List<BuffSeidJsonData42> DataList = new List<BuffSeidJsonData42>();

		// Token: 0x0400433A RID: 17210
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData42.OnInitFinish);

		// Token: 0x0400433B RID: 17211
		public int id;

		// Token: 0x0400433C RID: 17212
		public int value1;

		// Token: 0x0400433D RID: 17213
		public int value2;

		// Token: 0x0400433E RID: 17214
		public int value3;

		// Token: 0x0400433F RID: 17215
		public int value4;
	}
}
