using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B82 RID: 2946
	public class BuffSeidJsonData71 : IJSONClass
	{
		// Token: 0x06004970 RID: 18800 RVA: 0x001F2B80 File Offset: 0x001F0D80
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[71].list)
			{
				try
				{
					BuffSeidJsonData71 buffSeidJsonData = new BuffSeidJsonData71();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData71.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData71.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData71.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData71.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData71.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData71.OnInitFinishAction != null)
			{
				BuffSeidJsonData71.OnInitFinishAction();
			}
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400440D RID: 17421
		public static int SEIDID = 71;

		// Token: 0x0400440E RID: 17422
		public static Dictionary<int, BuffSeidJsonData71> DataDict = new Dictionary<int, BuffSeidJsonData71>();

		// Token: 0x0400440F RID: 17423
		public static List<BuffSeidJsonData71> DataList = new List<BuffSeidJsonData71>();

		// Token: 0x04004410 RID: 17424
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData71.OnInitFinish);

		// Token: 0x04004411 RID: 17425
		public int id;

		// Token: 0x04004412 RID: 17426
		public int value1;
	}
}
