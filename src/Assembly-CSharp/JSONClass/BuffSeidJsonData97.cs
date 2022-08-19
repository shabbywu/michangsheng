using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000800 RID: 2048
	public class BuffSeidJsonData97 : IJSONClass
	{
		// Token: 0x06003E12 RID: 15890 RVA: 0x001A88E4 File Offset: 0x001A6AE4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[97].list)
			{
				try
				{
					BuffSeidJsonData97 buffSeidJsonData = new BuffSeidJsonData97();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData97.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData97.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData97.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData97.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData97.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData97.OnInitFinishAction != null)
			{
				BuffSeidJsonData97.OnInitFinishAction();
			}
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003904 RID: 14596
		public static int SEIDID = 97;

		// Token: 0x04003905 RID: 14597
		public static Dictionary<int, BuffSeidJsonData97> DataDict = new Dictionary<int, BuffSeidJsonData97>();

		// Token: 0x04003906 RID: 14598
		public static List<BuffSeidJsonData97> DataList = new List<BuffSeidJsonData97>();

		// Token: 0x04003907 RID: 14599
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData97.OnInitFinish);

		// Token: 0x04003908 RID: 14600
		public int id;

		// Token: 0x04003909 RID: 14601
		public int value1;
	}
}
