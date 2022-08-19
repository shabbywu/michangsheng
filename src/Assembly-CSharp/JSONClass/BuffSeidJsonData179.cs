using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000795 RID: 1941
	public class BuffSeidJsonData179 : IJSONClass
	{
		// Token: 0x06003C66 RID: 15462 RVA: 0x0019ED78 File Offset: 0x0019CF78
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[179].list)
			{
				try
				{
					BuffSeidJsonData179 buffSeidJsonData = new BuffSeidJsonData179();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData179.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData179.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData179.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData179.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData179.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData179.OnInitFinishAction != null)
			{
				BuffSeidJsonData179.OnInitFinishAction();
			}
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400361D RID: 13853
		public static int SEIDID = 179;

		// Token: 0x0400361E RID: 13854
		public static Dictionary<int, BuffSeidJsonData179> DataDict = new Dictionary<int, BuffSeidJsonData179>();

		// Token: 0x0400361F RID: 13855
		public static List<BuffSeidJsonData179> DataList = new List<BuffSeidJsonData179>();

		// Token: 0x04003620 RID: 13856
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData179.OnInitFinish);

		// Token: 0x04003621 RID: 13857
		public int id;

		// Token: 0x04003622 RID: 13858
		public int value1;
	}
}
