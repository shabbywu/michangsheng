using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000754 RID: 1876
	public class BigMapLoadTalk : IJSONClass
	{
		// Token: 0x06003B64 RID: 15204 RVA: 0x00199154 File Offset: 0x00197354
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BigMapLoadTalk.list)
			{
				try
				{
					BigMapLoadTalk bigMapLoadTalk = new BigMapLoadTalk();
					bigMapLoadTalk.id = jsonobject["id"].I;
					bigMapLoadTalk.Talk = jsonobject["Talk"].I;
					if (BigMapLoadTalk.DataDict.ContainsKey(bigMapLoadTalk.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BigMapLoadTalk.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", bigMapLoadTalk.id));
					}
					else
					{
						BigMapLoadTalk.DataDict.Add(bigMapLoadTalk.id, bigMapLoadTalk);
						BigMapLoadTalk.DataList.Add(bigMapLoadTalk);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BigMapLoadTalk.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BigMapLoadTalk.OnInitFinishAction != null)
			{
				BigMapLoadTalk.OnInitFinishAction();
			}
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400346E RID: 13422
		public static Dictionary<int, BigMapLoadTalk> DataDict = new Dictionary<int, BigMapLoadTalk>();

		// Token: 0x0400346F RID: 13423
		public static List<BigMapLoadTalk> DataList = new List<BigMapLoadTalk>();

		// Token: 0x04003470 RID: 13424
		public static Action OnInitFinishAction = new Action(BigMapLoadTalk.OnInitFinish);

		// Token: 0x04003471 RID: 13425
		public int id;

		// Token: 0x04003472 RID: 13426
		public int Talk;
	}
}
