using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B8C RID: 2956
	public class BuffSeidJsonData84 : IJSONClass
	{
		// Token: 0x06004998 RID: 18840 RVA: 0x001F3764 File Offset: 0x001F1964
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[84].list)
			{
				try
				{
					BuffSeidJsonData84 buffSeidJsonData = new BuffSeidJsonData84();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData84.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData84.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData84.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData84.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData84.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData84.OnInitFinishAction != null)
			{
				BuffSeidJsonData84.OnInitFinishAction();
			}
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400444D RID: 17485
		public static int SEIDID = 84;

		// Token: 0x0400444E RID: 17486
		public static Dictionary<int, BuffSeidJsonData84> DataDict = new Dictionary<int, BuffSeidJsonData84>();

		// Token: 0x0400444F RID: 17487
		public static List<BuffSeidJsonData84> DataList = new List<BuffSeidJsonData84>();

		// Token: 0x04004450 RID: 17488
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData84.OnInitFinish);

		// Token: 0x04004451 RID: 17489
		public int id;

		// Token: 0x04004452 RID: 17490
		public int value1;
	}
}
