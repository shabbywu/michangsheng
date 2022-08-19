using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007FD RID: 2045
	public class BuffSeidJsonData92 : IJSONClass
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x001A84C8 File Offset: 0x001A66C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[92].list)
			{
				try
				{
					BuffSeidJsonData92 buffSeidJsonData = new BuffSeidJsonData92();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData92.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData92.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData92.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData92.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData92.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData92.OnInitFinishAction != null)
			{
				BuffSeidJsonData92.OnInitFinishAction();
			}
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038F1 RID: 14577
		public static int SEIDID = 92;

		// Token: 0x040038F2 RID: 14578
		public static Dictionary<int, BuffSeidJsonData92> DataDict = new Dictionary<int, BuffSeidJsonData92>();

		// Token: 0x040038F3 RID: 14579
		public static List<BuffSeidJsonData92> DataList = new List<BuffSeidJsonData92>();

		// Token: 0x040038F4 RID: 14580
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData92.OnInitFinish);

		// Token: 0x040038F5 RID: 14581
		public int id;

		// Token: 0x040038F6 RID: 14582
		public int value1;

		// Token: 0x040038F7 RID: 14583
		public int value2;
	}
}
