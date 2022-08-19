using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E4 RID: 2020
	public class BuffSeidJsonData65 : IJSONClass
	{
		// Token: 0x06003DA2 RID: 15778 RVA: 0x001A61A8 File Offset: 0x001A43A8
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

		// Token: 0x06003DA3 RID: 15779 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400384D RID: 14413
		public static int SEIDID = 65;

		// Token: 0x0400384E RID: 14414
		public static Dictionary<int, BuffSeidJsonData65> DataDict = new Dictionary<int, BuffSeidJsonData65>();

		// Token: 0x0400384F RID: 14415
		public static List<BuffSeidJsonData65> DataList = new List<BuffSeidJsonData65>();

		// Token: 0x04003850 RID: 14416
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData65.OnInitFinish);

		// Token: 0x04003851 RID: 14417
		public int id;

		// Token: 0x04003852 RID: 14418
		public int value1;
	}
}
