using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B21 RID: 2849
	public class BuffSeidJsonData166 : IJSONClass
	{
		// Token: 0x060047EC RID: 18412 RVA: 0x001EB14C File Offset: 0x001E934C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[166].list)
			{
				try
				{
					BuffSeidJsonData166 buffSeidJsonData = new BuffSeidJsonData166();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData166.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData166.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData166.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData166.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData166.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData166.OnInitFinishAction != null)
			{
				BuffSeidJsonData166.OnInitFinishAction();
			}
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004165 RID: 16741
		public static int SEIDID = 166;

		// Token: 0x04004166 RID: 16742
		public static Dictionary<int, BuffSeidJsonData166> DataDict = new Dictionary<int, BuffSeidJsonData166>();

		// Token: 0x04004167 RID: 16743
		public static List<BuffSeidJsonData166> DataList = new List<BuffSeidJsonData166>();

		// Token: 0x04004168 RID: 16744
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData166.OnInitFinish);

		// Token: 0x04004169 RID: 16745
		public int id;

		// Token: 0x0400416A RID: 16746
		public int value1;
	}
}
