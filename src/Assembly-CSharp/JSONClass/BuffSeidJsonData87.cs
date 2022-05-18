using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B8F RID: 2959
	public class BuffSeidJsonData87 : IJSONClass
	{
		// Token: 0x060049A4 RID: 18852 RVA: 0x001F3ADC File Offset: 0x001F1CDC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[87].list)
			{
				try
				{
					BuffSeidJsonData87 buffSeidJsonData = new BuffSeidJsonData87();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData87.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData87.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData87.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData87.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData87.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData87.OnInitFinishAction != null)
			{
				BuffSeidJsonData87.OnInitFinishAction();
			}
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400445F RID: 17503
		public static int SEIDID = 87;

		// Token: 0x04004460 RID: 17504
		public static Dictionary<int, BuffSeidJsonData87> DataDict = new Dictionary<int, BuffSeidJsonData87>();

		// Token: 0x04004461 RID: 17505
		public static List<BuffSeidJsonData87> DataList = new List<BuffSeidJsonData87>();

		// Token: 0x04004462 RID: 17506
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData87.OnInitFinish);

		// Token: 0x04004463 RID: 17507
		public int id;

		// Token: 0x04004464 RID: 17508
		public int value1;

		// Token: 0x04004465 RID: 17509
		public int value2;
	}
}
