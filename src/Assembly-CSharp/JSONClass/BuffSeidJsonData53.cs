using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B6F RID: 2927
	public class BuffSeidJsonData53 : IJSONClass
	{
		// Token: 0x06004924 RID: 18724 RVA: 0x001F141C File Offset: 0x001EF61C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[53].list)
			{
				try
				{
					BuffSeidJsonData53 buffSeidJsonData = new BuffSeidJsonData53();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData53.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData53.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData53.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData53.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData53.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData53.OnInitFinishAction != null)
			{
				BuffSeidJsonData53.OnInitFinishAction();
			}
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400438D RID: 17293
		public static int SEIDID = 53;

		// Token: 0x0400438E RID: 17294
		public static Dictionary<int, BuffSeidJsonData53> DataDict = new Dictionary<int, BuffSeidJsonData53>();

		// Token: 0x0400438F RID: 17295
		public static List<BuffSeidJsonData53> DataList = new List<BuffSeidJsonData53>();

		// Token: 0x04004390 RID: 17296
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData53.OnInitFinish);

		// Token: 0x04004391 RID: 17297
		public int id;

		// Token: 0x04004392 RID: 17298
		public int value1;

		// Token: 0x04004393 RID: 17299
		public int value2;

		// Token: 0x04004394 RID: 17300
		public int value3;

		// Token: 0x04004395 RID: 17301
		public int value4;
	}
}
