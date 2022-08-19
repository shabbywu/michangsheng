using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D7 RID: 2007
	public class BuffSeidJsonData52 : IJSONClass
	{
		// Token: 0x06003D6E RID: 15726 RVA: 0x001A4F1C File Offset: 0x001A311C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[52].list)
			{
				try
				{
					BuffSeidJsonData52 buffSeidJsonData = new BuffSeidJsonData52();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData52.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData52.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData52.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData52.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData52.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData52.OnInitFinishAction != null)
			{
				BuffSeidJsonData52.OnInitFinishAction();
			}
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037F5 RID: 14325
		public static int SEIDID = 52;

		// Token: 0x040037F6 RID: 14326
		public static Dictionary<int, BuffSeidJsonData52> DataDict = new Dictionary<int, BuffSeidJsonData52>();

		// Token: 0x040037F7 RID: 14327
		public static List<BuffSeidJsonData52> DataList = new List<BuffSeidJsonData52>();

		// Token: 0x040037F8 RID: 14328
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData52.OnInitFinish);

		// Token: 0x040037F9 RID: 14329
		public int id;

		// Token: 0x040037FA RID: 14330
		public int value1;

		// Token: 0x040037FB RID: 14331
		public int value2;

		// Token: 0x040037FC RID: 14332
		public int value3;
	}
}
