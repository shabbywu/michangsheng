using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B6C RID: 2924
	public class BuffSeidJsonData50 : IJSONClass
	{
		// Token: 0x06004918 RID: 18712 RVA: 0x001F1050 File Offset: 0x001EF250
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[50].list)
			{
				try
				{
					BuffSeidJsonData50 buffSeidJsonData = new BuffSeidJsonData50();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData50.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData50.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData50.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData50.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData50.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData50.OnInitFinishAction != null)
			{
				BuffSeidJsonData50.OnInitFinishAction();
			}
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004377 RID: 17271
		public static int SEIDID = 50;

		// Token: 0x04004378 RID: 17272
		public static Dictionary<int, BuffSeidJsonData50> DataDict = new Dictionary<int, BuffSeidJsonData50>();

		// Token: 0x04004379 RID: 17273
		public static List<BuffSeidJsonData50> DataList = new List<BuffSeidJsonData50>();

		// Token: 0x0400437A RID: 17274
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData50.OnInitFinish);

		// Token: 0x0400437B RID: 17275
		public int id;

		// Token: 0x0400437C RID: 17276
		public int value1;

		// Token: 0x0400437D RID: 17277
		public int value2;
	}
}
