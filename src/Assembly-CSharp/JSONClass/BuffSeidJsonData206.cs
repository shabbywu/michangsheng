using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007AB RID: 1963
	public class BuffSeidJsonData206 : IJSONClass
	{
		// Token: 0x06003CBE RID: 15550 RVA: 0x001A0CF8 File Offset: 0x0019EEF8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[206].list)
			{
				try
				{
					BuffSeidJsonData206 buffSeidJsonData = new BuffSeidJsonData206();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData206.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData206.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData206.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData206.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData206.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData206.OnInitFinishAction != null)
			{
				BuffSeidJsonData206.OnInitFinishAction();
			}
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036AD RID: 13997
		public static int SEIDID = 206;

		// Token: 0x040036AE RID: 13998
		public static Dictionary<int, BuffSeidJsonData206> DataDict = new Dictionary<int, BuffSeidJsonData206>();

		// Token: 0x040036AF RID: 13999
		public static List<BuffSeidJsonData206> DataList = new List<BuffSeidJsonData206>();

		// Token: 0x040036B0 RID: 14000
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData206.OnInitFinish);

		// Token: 0x040036B1 RID: 14001
		public int id;

		// Token: 0x040036B2 RID: 14002
		public int value1;

		// Token: 0x040036B3 RID: 14003
		public int value2;

		// Token: 0x040036B4 RID: 14004
		public string panduan;
	}
}
