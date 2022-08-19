using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E1 RID: 2017
	public class BuffSeidJsonData62 : IJSONClass
	{
		// Token: 0x06003D96 RID: 15766 RVA: 0x001A5D8C File Offset: 0x001A3F8C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[62].list)
			{
				try
				{
					BuffSeidJsonData62 buffSeidJsonData = new BuffSeidJsonData62();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData62.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData62.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData62.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData62.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData62.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData62.OnInitFinishAction != null)
			{
				BuffSeidJsonData62.OnInitFinishAction();
			}
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400383A RID: 14394
		public static int SEIDID = 62;

		// Token: 0x0400383B RID: 14395
		public static Dictionary<int, BuffSeidJsonData62> DataDict = new Dictionary<int, BuffSeidJsonData62>();

		// Token: 0x0400383C RID: 14396
		public static List<BuffSeidJsonData62> DataList = new List<BuffSeidJsonData62>();

		// Token: 0x0400383D RID: 14397
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData62.OnInitFinish);

		// Token: 0x0400383E RID: 14398
		public int id;

		// Token: 0x0400383F RID: 14399
		public int value1;
	}
}
