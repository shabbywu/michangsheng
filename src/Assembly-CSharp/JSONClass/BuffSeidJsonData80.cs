using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F1 RID: 2033
	public class BuffSeidJsonData80 : IJSONClass
	{
		// Token: 0x06003DD6 RID: 15830 RVA: 0x001A73F0 File Offset: 0x001A55F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[80].list)
			{
				try
				{
					BuffSeidJsonData80 buffSeidJsonData = new BuffSeidJsonData80();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData80.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData80.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData80.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData80.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData80.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData80.OnInitFinishAction != null)
			{
				BuffSeidJsonData80.OnInitFinishAction();
			}
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038A2 RID: 14498
		public static int SEIDID = 80;

		// Token: 0x040038A3 RID: 14499
		public static Dictionary<int, BuffSeidJsonData80> DataDict = new Dictionary<int, BuffSeidJsonData80>();

		// Token: 0x040038A4 RID: 14500
		public static List<BuffSeidJsonData80> DataList = new List<BuffSeidJsonData80>();

		// Token: 0x040038A5 RID: 14501
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData80.OnInitFinish);

		// Token: 0x040038A6 RID: 14502
		public int id;

		// Token: 0x040038A7 RID: 14503
		public int value1;

		// Token: 0x040038A8 RID: 14504
		public int value2;

		// Token: 0x040038A9 RID: 14505
		public int value3;
	}
}
