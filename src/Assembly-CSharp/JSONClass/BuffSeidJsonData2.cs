using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A3 RID: 1955
	public class BuffSeidJsonData2 : IJSONClass
	{
		// Token: 0x06003C9E RID: 15518 RVA: 0x001A01E0 File Offset: 0x0019E3E0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[2].list)
			{
				try
				{
					BuffSeidJsonData2 buffSeidJsonData = new BuffSeidJsonData2();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData2.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData2.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData2.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData2.OnInitFinishAction != null)
			{
				BuffSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400367C RID: 13948
		public static int SEIDID = 2;

		// Token: 0x0400367D RID: 13949
		public static Dictionary<int, BuffSeidJsonData2> DataDict = new Dictionary<int, BuffSeidJsonData2>();

		// Token: 0x0400367E RID: 13950
		public static List<BuffSeidJsonData2> DataList = new List<BuffSeidJsonData2>();

		// Token: 0x0400367F RID: 13951
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData2.OnInitFinish);

		// Token: 0x04003680 RID: 13952
		public int id;

		// Token: 0x04003681 RID: 13953
		public int value1;
	}
}
