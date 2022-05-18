using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AEC RID: 2796
	public class BigMapLoadTalk : IJSONClass
	{
		// Token: 0x0600471A RID: 18202 RVA: 0x001E7180 File Offset: 0x001E5380
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

		// Token: 0x0600471B RID: 18203 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004007 RID: 16391
		public static Dictionary<int, BigMapLoadTalk> DataDict = new Dictionary<int, BigMapLoadTalk>();

		// Token: 0x04004008 RID: 16392
		public static List<BigMapLoadTalk> DataList = new List<BigMapLoadTalk>();

		// Token: 0x04004009 RID: 16393
		public static Action OnInitFinishAction = new Action(BigMapLoadTalk.OnInitFinish);

		// Token: 0x0400400A RID: 16394
		public int id;

		// Token: 0x0400400B RID: 16395
		public int Talk;
	}
}
