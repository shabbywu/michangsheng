using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B90 RID: 2960
	public class BuffSeidJsonData88 : IJSONClass
	{
		// Token: 0x060049A8 RID: 18856 RVA: 0x001F3C18 File Offset: 0x001F1E18
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[88].list)
			{
				try
				{
					BuffSeidJsonData88 buffSeidJsonData = new BuffSeidJsonData88();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData88.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData88.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData88.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData88.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData88.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData88.OnInitFinishAction != null)
			{
				BuffSeidJsonData88.OnInitFinishAction();
			}
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004466 RID: 17510
		public static int SEIDID = 88;

		// Token: 0x04004467 RID: 17511
		public static Dictionary<int, BuffSeidJsonData88> DataDict = new Dictionary<int, BuffSeidJsonData88>();

		// Token: 0x04004468 RID: 17512
		public static List<BuffSeidJsonData88> DataList = new List<BuffSeidJsonData88>();

		// Token: 0x04004469 RID: 17513
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData88.OnInitFinish);

		// Token: 0x0400446A RID: 17514
		public int id;

		// Token: 0x0400446B RID: 17515
		public int value1;

		// Token: 0x0400446C RID: 17516
		public int value2;
	}
}
