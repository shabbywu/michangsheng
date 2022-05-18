using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B84 RID: 2948
	public class BuffSeidJsonData74 : IJSONClass
	{
		// Token: 0x06004978 RID: 18808 RVA: 0x001F2DE4 File Offset: 0x001F0FE4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[74].list)
			{
				try
				{
					BuffSeidJsonData74 buffSeidJsonData = new BuffSeidJsonData74();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData74.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData74.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData74.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData74.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData74.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData74.OnInitFinishAction != null)
			{
				BuffSeidJsonData74.OnInitFinishAction();
			}
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400441A RID: 17434
		public static int SEIDID = 74;

		// Token: 0x0400441B RID: 17435
		public static Dictionary<int, BuffSeidJsonData74> DataDict = new Dictionary<int, BuffSeidJsonData74>();

		// Token: 0x0400441C RID: 17436
		public static List<BuffSeidJsonData74> DataList = new List<BuffSeidJsonData74>();

		// Token: 0x0400441D RID: 17437
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData74.OnInitFinish);

		// Token: 0x0400441E RID: 17438
		public int id;

		// Token: 0x0400441F RID: 17439
		public int value1;
	}
}
