using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AFF RID: 2815
	public class BuffSeidJsonData119 : IJSONClass
	{
		// Token: 0x06004766 RID: 18278 RVA: 0x001E87C4 File Offset: 0x001E69C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[119].list)
			{
				try
				{
					BuffSeidJsonData119 buffSeidJsonData = new BuffSeidJsonData119();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData119.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData119.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData119.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData119.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData119.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData119.OnInitFinishAction != null)
			{
				BuffSeidJsonData119.OnInitFinishAction();
			}
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400407B RID: 16507
		public static int SEIDID = 119;

		// Token: 0x0400407C RID: 16508
		public static Dictionary<int, BuffSeidJsonData119> DataDict = new Dictionary<int, BuffSeidJsonData119>();

		// Token: 0x0400407D RID: 16509
		public static List<BuffSeidJsonData119> DataList = new List<BuffSeidJsonData119>();

		// Token: 0x0400407E RID: 16510
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData119.OnInitFinish);

		// Token: 0x0400407F RID: 16511
		public int id;

		// Token: 0x04004080 RID: 16512
		public int target;

		// Token: 0x04004081 RID: 16513
		public int value1;

		// Token: 0x04004082 RID: 16514
		public int value2;
	}
}
