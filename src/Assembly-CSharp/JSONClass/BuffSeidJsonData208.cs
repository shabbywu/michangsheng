using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B44 RID: 2884
	public class BuffSeidJsonData208 : IJSONClass
	{
		// Token: 0x06004878 RID: 18552 RVA: 0x001EDC4C File Offset: 0x001EBE4C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[208].list)
			{
				try
				{
					BuffSeidJsonData208 buffSeidJsonData = new BuffSeidJsonData208();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData208.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData208.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData208.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData208.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData208.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData208.OnInitFinishAction != null)
			{
				BuffSeidJsonData208.OnInitFinishAction();
			}
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400424E RID: 16974
		public static int SEIDID = 208;

		// Token: 0x0400424F RID: 16975
		public static Dictionary<int, BuffSeidJsonData208> DataDict = new Dictionary<int, BuffSeidJsonData208>();

		// Token: 0x04004250 RID: 16976
		public static List<BuffSeidJsonData208> DataList = new List<BuffSeidJsonData208>();

		// Token: 0x04004251 RID: 16977
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData208.OnInitFinish);

		// Token: 0x04004252 RID: 16978
		public int id;

		// Token: 0x04004253 RID: 16979
		public int value1;
	}
}
