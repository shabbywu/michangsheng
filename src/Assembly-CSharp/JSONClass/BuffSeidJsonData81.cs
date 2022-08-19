using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F2 RID: 2034
	public class BuffSeidJsonData81 : IJSONClass
	{
		// Token: 0x06003DDA RID: 15834 RVA: 0x001A7574 File Offset: 0x001A5774
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[81].list)
			{
				try
				{
					BuffSeidJsonData81 buffSeidJsonData = new BuffSeidJsonData81();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData81.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData81.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData81.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData81.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData81.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData81.OnInitFinishAction != null)
			{
				BuffSeidJsonData81.OnInitFinishAction();
			}
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038AA RID: 14506
		public static int SEIDID = 81;

		// Token: 0x040038AB RID: 14507
		public static Dictionary<int, BuffSeidJsonData81> DataDict = new Dictionary<int, BuffSeidJsonData81>();

		// Token: 0x040038AC RID: 14508
		public static List<BuffSeidJsonData81> DataList = new List<BuffSeidJsonData81>();

		// Token: 0x040038AD RID: 14509
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData81.OnInitFinish);

		// Token: 0x040038AE RID: 14510
		public int id;

		// Token: 0x040038AF RID: 14511
		public int value1;
	}
}
