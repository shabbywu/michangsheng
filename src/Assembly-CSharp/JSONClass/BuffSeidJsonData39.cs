using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B61 RID: 2913
	public class BuffSeidJsonData39 : IJSONClass
	{
		// Token: 0x060048EC RID: 18668 RVA: 0x001F011C File Offset: 0x001EE31C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[39].list)
			{
				try
				{
					BuffSeidJsonData39 buffSeidJsonData = new BuffSeidJsonData39();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData39.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData39.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData39.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData39.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData39.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData39.OnInitFinishAction != null)
			{
				BuffSeidJsonData39.OnInitFinishAction();
			}
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400431D RID: 17181
		public static int SEIDID = 39;

		// Token: 0x0400431E RID: 17182
		public static Dictionary<int, BuffSeidJsonData39> DataDict = new Dictionary<int, BuffSeidJsonData39>();

		// Token: 0x0400431F RID: 17183
		public static List<BuffSeidJsonData39> DataList = new List<BuffSeidJsonData39>();

		// Token: 0x04004320 RID: 17184
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData39.OnInitFinish);

		// Token: 0x04004321 RID: 17185
		public int id;

		// Token: 0x04004322 RID: 17186
		public int value1;

		// Token: 0x04004323 RID: 17187
		public int value2;

		// Token: 0x04004324 RID: 17188
		public int value3;

		// Token: 0x04004325 RID: 17189
		public int value4;
	}
}
