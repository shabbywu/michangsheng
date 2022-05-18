using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF2 RID: 2802
	public class BuffSeidJsonData103 : IJSONClass
	{
		// Token: 0x06004732 RID: 18226 RVA: 0x001E7880 File Offset: 0x001E5A80
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[103].list)
			{
				try
				{
					BuffSeidJsonData103 buffSeidJsonData = new BuffSeidJsonData103();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData103.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData103.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData103.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData103.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData103.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData103.OnInitFinishAction != null)
			{
				BuffSeidJsonData103.OnInitFinishAction();
			}
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400402A RID: 16426
		public static int SEIDID = 103;

		// Token: 0x0400402B RID: 16427
		public static Dictionary<int, BuffSeidJsonData103> DataDict = new Dictionary<int, BuffSeidJsonData103>();

		// Token: 0x0400402C RID: 16428
		public static List<BuffSeidJsonData103> DataList = new List<BuffSeidJsonData103>();

		// Token: 0x0400402D RID: 16429
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData103.OnInitFinish);

		// Token: 0x0400402E RID: 16430
		public int id;

		// Token: 0x0400402F RID: 16431
		public int value1;
	}
}
