using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007ED RID: 2029
	public class BuffSeidJsonData74 : IJSONClass
	{
		// Token: 0x06003DC6 RID: 15814 RVA: 0x001A6E7C File Offset: 0x001A507C
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

		// Token: 0x06003DC7 RID: 15815 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400388A RID: 14474
		public static int SEIDID = 74;

		// Token: 0x0400388B RID: 14475
		public static Dictionary<int, BuffSeidJsonData74> DataDict = new Dictionary<int, BuffSeidJsonData74>();

		// Token: 0x0400388C RID: 14476
		public static List<BuffSeidJsonData74> DataList = new List<BuffSeidJsonData74>();

		// Token: 0x0400388D RID: 14477
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData74.OnInitFinish);

		// Token: 0x0400388E RID: 14478
		public int id;

		// Token: 0x0400388F RID: 14479
		public int value1;
	}
}
