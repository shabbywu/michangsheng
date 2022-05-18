using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B93 RID: 2963
	public class BuffSeidJsonData91 : IJSONClass
	{
		// Token: 0x060049B4 RID: 18868 RVA: 0x001F3FCC File Offset: 0x001F21CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[91].list)
			{
				try
				{
					BuffSeidJsonData91 buffSeidJsonData = new BuffSeidJsonData91();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData91.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData91.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData91.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData91.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData91.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData91.OnInitFinishAction != null)
			{
				BuffSeidJsonData91.OnInitFinishAction();
			}
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400447B RID: 17531
		public static int SEIDID = 91;

		// Token: 0x0400447C RID: 17532
		public static Dictionary<int, BuffSeidJsonData91> DataDict = new Dictionary<int, BuffSeidJsonData91>();

		// Token: 0x0400447D RID: 17533
		public static List<BuffSeidJsonData91> DataList = new List<BuffSeidJsonData91>();

		// Token: 0x0400447E RID: 17534
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData91.OnInitFinish);

		// Token: 0x0400447F RID: 17535
		public int id;

		// Token: 0x04004480 RID: 17536
		public int value1;
	}
}
