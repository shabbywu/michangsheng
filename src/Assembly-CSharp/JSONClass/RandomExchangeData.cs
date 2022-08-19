using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008CD RID: 2253
	public class RandomExchangeData : IJSONClass
	{
		// Token: 0x06004147 RID: 16711 RVA: 0x001BF058 File Offset: 0x001BD258
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.RandomExchangeData.list)
			{
				try
				{
					RandomExchangeData randomExchangeData = new RandomExchangeData();
					randomExchangeData.id = jsonobject["id"].I;
					randomExchangeData.ItemID = jsonobject["ItemID"].I;
					randomExchangeData.percent = jsonobject["percent"].I;
					randomExchangeData.YiWuFlag = jsonobject["YiWuFlag"].ToList();
					randomExchangeData.NumFlag = jsonobject["NumFlag"].ToList();
					randomExchangeData.YiWuItem = jsonobject["YiWuItem"].ToList();
					randomExchangeData.NumItem = jsonobject["NumItem"].ToList();
					if (RandomExchangeData.DataDict.ContainsKey(randomExchangeData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典RandomExchangeData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", randomExchangeData.id));
					}
					else
					{
						RandomExchangeData.DataDict.Add(randomExchangeData.id, randomExchangeData);
						RandomExchangeData.DataList.Add(randomExchangeData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典RandomExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (RandomExchangeData.OnInitFinishAction != null)
			{
				RandomExchangeData.OnInitFinishAction();
			}
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004073 RID: 16499
		public static Dictionary<int, RandomExchangeData> DataDict = new Dictionary<int, RandomExchangeData>();

		// Token: 0x04004074 RID: 16500
		public static List<RandomExchangeData> DataList = new List<RandomExchangeData>();

		// Token: 0x04004075 RID: 16501
		public static Action OnInitFinishAction = new Action(RandomExchangeData.OnInitFinish);

		// Token: 0x04004076 RID: 16502
		public int id;

		// Token: 0x04004077 RID: 16503
		public int ItemID;

		// Token: 0x04004078 RID: 16504
		public int percent;

		// Token: 0x04004079 RID: 16505
		public List<int> YiWuFlag = new List<int>();

		// Token: 0x0400407A RID: 16506
		public List<int> NumFlag = new List<int>();

		// Token: 0x0400407B RID: 16507
		public List<int> YiWuItem = new List<int>();

		// Token: 0x0400407C RID: 16508
		public List<int> NumItem = new List<int>();
	}
}
