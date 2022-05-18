using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B4D RID: 2893
	public class BuffSeidJsonData22 : IJSONClass
	{
		// Token: 0x0600489C RID: 18588 RVA: 0x001EE754 File Offset: 0x001EC954
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[22].list)
			{
				try
				{
					BuffSeidJsonData22 buffSeidJsonData = new BuffSeidJsonData22();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData22.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData22.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData22.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData22.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData22.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData22.OnInitFinishAction != null)
			{
				BuffSeidJsonData22.OnInitFinishAction();
			}
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004289 RID: 17033
		public static int SEIDID = 22;

		// Token: 0x0400428A RID: 17034
		public static Dictionary<int, BuffSeidJsonData22> DataDict = new Dictionary<int, BuffSeidJsonData22>();

		// Token: 0x0400428B RID: 17035
		public static List<BuffSeidJsonData22> DataList = new List<BuffSeidJsonData22>();

		// Token: 0x0400428C RID: 17036
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData22.OnInitFinish);

		// Token: 0x0400428D RID: 17037
		public int id;

		// Token: 0x0400428E RID: 17038
		public int value1;
	}
}
