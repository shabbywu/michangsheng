using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B3 RID: 1971
	public class BuffSeidJsonData215 : IJSONClass
	{
		// Token: 0x06003CDE RID: 15582 RVA: 0x001A1844 File Offset: 0x0019FA44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[215].list)
			{
				try
				{
					BuffSeidJsonData215 buffSeidJsonData = new BuffSeidJsonData215();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData215.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData215.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData215.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData215.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData215.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData215.OnInitFinishAction != null)
			{
				BuffSeidJsonData215.OnInitFinishAction();
			}
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036E1 RID: 14049
		public static int SEIDID = 215;

		// Token: 0x040036E2 RID: 14050
		public static Dictionary<int, BuffSeidJsonData215> DataDict = new Dictionary<int, BuffSeidJsonData215>();

		// Token: 0x040036E3 RID: 14051
		public static List<BuffSeidJsonData215> DataList = new List<BuffSeidJsonData215>();

		// Token: 0x040036E4 RID: 14052
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData215.OnInitFinish);

		// Token: 0x040036E5 RID: 14053
		public int id;

		// Token: 0x040036E6 RID: 14054
		public int value1;

		// Token: 0x040036E7 RID: 14055
		public int value2;

		// Token: 0x040036E8 RID: 14056
		public int value3;

		// Token: 0x040036E9 RID: 14057
		public int value4;
	}
}
