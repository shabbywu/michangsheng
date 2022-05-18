using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B87 RID: 2951
	public class BuffSeidJsonData79 : IJSONClass
	{
		// Token: 0x06004984 RID: 18820 RVA: 0x001F315C File Offset: 0x001F135C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[79].list)
			{
				try
				{
					BuffSeidJsonData79 buffSeidJsonData = new BuffSeidJsonData79();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData79.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData79.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData79.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData79.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData79.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData79.OnInitFinishAction != null)
			{
				BuffSeidJsonData79.OnInitFinishAction();
			}
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400442C RID: 17452
		public static int SEIDID = 79;

		// Token: 0x0400442D RID: 17453
		public static Dictionary<int, BuffSeidJsonData79> DataDict = new Dictionary<int, BuffSeidJsonData79>();

		// Token: 0x0400442E RID: 17454
		public static List<BuffSeidJsonData79> DataList = new List<BuffSeidJsonData79>();

		// Token: 0x0400442F RID: 17455
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData79.OnInitFinish);

		// Token: 0x04004430 RID: 17456
		public int id;

		// Token: 0x04004431 RID: 17457
		public int value1;
	}
}
