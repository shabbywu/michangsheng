using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AFC RID: 2812
	public class BuffSeidJsonData116 : IJSONClass
	{
		// Token: 0x0600475A RID: 18266 RVA: 0x001E8438 File Offset: 0x001E6638
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[116].list)
			{
				try
				{
					BuffSeidJsonData116 buffSeidJsonData = new BuffSeidJsonData116();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData116.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData116.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData116.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData116.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData116.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData116.OnInitFinishAction != null)
			{
				BuffSeidJsonData116.OnInitFinishAction();
			}
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004068 RID: 16488
		public static int SEIDID = 116;

		// Token: 0x04004069 RID: 16489
		public static Dictionary<int, BuffSeidJsonData116> DataDict = new Dictionary<int, BuffSeidJsonData116>();

		// Token: 0x0400406A RID: 16490
		public static List<BuffSeidJsonData116> DataList = new List<BuffSeidJsonData116>();

		// Token: 0x0400406B RID: 16491
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData116.OnInitFinish);

		// Token: 0x0400406C RID: 16492
		public int id;

		// Token: 0x0400406D RID: 16493
		public int value1;
	}
}
