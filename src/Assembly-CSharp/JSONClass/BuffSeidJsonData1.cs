using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AEE RID: 2798
	public class BuffSeidJsonData1 : IJSONClass
	{
		// Token: 0x06004722 RID: 18210 RVA: 0x001E73E0 File Offset: 0x001E55E0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[1].list)
			{
				try
				{
					BuffSeidJsonData1 buffSeidJsonData = new BuffSeidJsonData1();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData1.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData1.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData1.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData1.OnInitFinishAction != null)
			{
				BuffSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004012 RID: 16402
		public static int SEIDID = 1;

		// Token: 0x04004013 RID: 16403
		public static Dictionary<int, BuffSeidJsonData1> DataDict = new Dictionary<int, BuffSeidJsonData1>();

		// Token: 0x04004014 RID: 16404
		public static List<BuffSeidJsonData1> DataList = new List<BuffSeidJsonData1>();

		// Token: 0x04004015 RID: 16405
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData1.OnInitFinish);

		// Token: 0x04004016 RID: 16406
		public int id;

		// Token: 0x04004017 RID: 16407
		public int value1;
	}
}
