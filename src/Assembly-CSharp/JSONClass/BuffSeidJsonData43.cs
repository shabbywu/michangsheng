using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007CE RID: 1998
	public class BuffSeidJsonData43 : IJSONClass
	{
		// Token: 0x06003D4A RID: 15690 RVA: 0x001A4178 File Offset: 0x001A2378
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[43].list)
			{
				try
				{
					BuffSeidJsonData43 buffSeidJsonData = new BuffSeidJsonData43();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData43.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData43.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData43.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData43.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData43.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData43.OnInitFinishAction != null)
			{
				BuffSeidJsonData43.OnInitFinishAction();
			}
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037B0 RID: 14256
		public static int SEIDID = 43;

		// Token: 0x040037B1 RID: 14257
		public static Dictionary<int, BuffSeidJsonData43> DataDict = new Dictionary<int, BuffSeidJsonData43>();

		// Token: 0x040037B2 RID: 14258
		public static List<BuffSeidJsonData43> DataList = new List<BuffSeidJsonData43>();

		// Token: 0x040037B3 RID: 14259
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData43.OnInitFinish);

		// Token: 0x040037B4 RID: 14260
		public int id;

		// Token: 0x040037B5 RID: 14261
		public int value1;

		// Token: 0x040037B6 RID: 14262
		public int value2;

		// Token: 0x040037B7 RID: 14263
		public int value3;
	}
}
