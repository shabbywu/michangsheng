using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007BD RID: 1981
	public class BuffSeidJsonData30 : IJSONClass
	{
		// Token: 0x06003D06 RID: 15622 RVA: 0x001A26DC File Offset: 0x001A08DC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[30].list)
			{
				try
				{
					BuffSeidJsonData30 buffSeidJsonData = new BuffSeidJsonData30();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData30.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData30.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData30.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData30.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData30.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData30.OnInitFinishAction != null)
			{
				BuffSeidJsonData30.OnInitFinishAction();
			}
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003727 RID: 14119
		public static int SEIDID = 30;

		// Token: 0x04003728 RID: 14120
		public static Dictionary<int, BuffSeidJsonData30> DataDict = new Dictionary<int, BuffSeidJsonData30>();

		// Token: 0x04003729 RID: 14121
		public static List<BuffSeidJsonData30> DataList = new List<BuffSeidJsonData30>();

		// Token: 0x0400372A RID: 14122
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData30.OnInitFinish);

		// Token: 0x0400372B RID: 14123
		public int id;

		// Token: 0x0400372C RID: 14124
		public int value1;

		// Token: 0x0400372D RID: 14125
		public int value2;
	}
}
