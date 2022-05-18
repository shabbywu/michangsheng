using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B67 RID: 2919
	public class BuffSeidJsonData45 : IJSONClass
	{
		// Token: 0x06004904 RID: 18692 RVA: 0x001F09C4 File Offset: 0x001EEBC4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[45].list)
			{
				try
				{
					BuffSeidJsonData45 buffSeidJsonData = new BuffSeidJsonData45();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData45.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData45.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData45.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData45.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData45.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData45.OnInitFinishAction != null)
			{
				BuffSeidJsonData45.OnInitFinishAction();
			}
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004351 RID: 17233
		public static int SEIDID = 45;

		// Token: 0x04004352 RID: 17234
		public static Dictionary<int, BuffSeidJsonData45> DataDict = new Dictionary<int, BuffSeidJsonData45>();

		// Token: 0x04004353 RID: 17235
		public static List<BuffSeidJsonData45> DataList = new List<BuffSeidJsonData45>();

		// Token: 0x04004354 RID: 17236
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData45.OnInitFinish);

		// Token: 0x04004355 RID: 17237
		public int id;

		// Token: 0x04004356 RID: 17238
		public int value1;

		// Token: 0x04004357 RID: 17239
		public int value2;

		// Token: 0x04004358 RID: 17240
		public int value3;

		// Token: 0x04004359 RID: 17241
		public int value4;
	}
}
