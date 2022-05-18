using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B7B RID: 2939
	public class BuffSeidJsonData65 : IJSONClass
	{
		// Token: 0x06004954 RID: 18772 RVA: 0x001F22D4 File Offset: 0x001F04D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[65].list)
			{
				try
				{
					BuffSeidJsonData65 buffSeidJsonData = new BuffSeidJsonData65();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData65.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData65.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData65.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData65.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData65.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData65.OnInitFinishAction != null)
			{
				BuffSeidJsonData65.OnInitFinishAction();
			}
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043DD RID: 17373
		public static int SEIDID = 65;

		// Token: 0x040043DE RID: 17374
		public static Dictionary<int, BuffSeidJsonData65> DataDict = new Dictionary<int, BuffSeidJsonData65>();

		// Token: 0x040043DF RID: 17375
		public static List<BuffSeidJsonData65> DataList = new List<BuffSeidJsonData65>();

		// Token: 0x040043E0 RID: 17376
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData65.OnInitFinish);

		// Token: 0x040043E1 RID: 17377
		public int id;

		// Token: 0x040043E2 RID: 17378
		public int value1;
	}
}
