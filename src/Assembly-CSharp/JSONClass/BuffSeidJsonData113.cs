using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AFA RID: 2810
	public class BuffSeidJsonData113 : IJSONClass
	{
		// Token: 0x06004752 RID: 18258 RVA: 0x001E81E8 File Offset: 0x001E63E8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[113].list)
			{
				try
				{
					BuffSeidJsonData113 buffSeidJsonData = new BuffSeidJsonData113();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData113.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData113.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData113.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData113.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData113.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData113.OnInitFinishAction != null)
			{
				BuffSeidJsonData113.OnInitFinishAction();
			}
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400405C RID: 16476
		public static int SEIDID = 113;

		// Token: 0x0400405D RID: 16477
		public static Dictionary<int, BuffSeidJsonData113> DataDict = new Dictionary<int, BuffSeidJsonData113>();

		// Token: 0x0400405E RID: 16478
		public static List<BuffSeidJsonData113> DataList = new List<BuffSeidJsonData113>();

		// Token: 0x0400405F RID: 16479
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData113.OnInitFinish);

		// Token: 0x04004060 RID: 16480
		public int id;

		// Token: 0x04004061 RID: 16481
		public List<int> value1 = new List<int>();
	}
}
