using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSONClass
{
	// Token: 0x02000CF9 RID: 3321
	public class TianJiDaBiReward : IJSONClass
	{
		// Token: 0x06004F4C RID: 20300 RVA: 0x00214350 File Offset: 0x00212550
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TianJiDaBiReward.list)
			{
				try
				{
					TianJiDaBiReward tianJiDaBiReward = new TianJiDaBiReward();
					tianJiDaBiReward.id = jsonobject["id"].I;
					tianJiDaBiReward.Reward1 = jsonobject["Reward1"].ToList();
					tianJiDaBiReward.Reward2 = jsonobject["Reward2"].ToList();
					tianJiDaBiReward.Reward3 = jsonobject["Reward3"].ToList();
					tianJiDaBiReward.Reward4 = jsonobject["Reward4"].ToList();
					tianJiDaBiReward.Reward5 = jsonobject["Reward5"].ToList();
					tianJiDaBiReward.Reward6 = jsonobject["Reward6"].ToList();
					tianJiDaBiReward.Reward7 = jsonobject["Reward7"].ToList();
					tianJiDaBiReward.Reward8 = jsonobject["Reward8"].ToList();
					tianJiDaBiReward.Reward9 = jsonobject["Reward9"].ToList();
					tianJiDaBiReward.Reward10 = jsonobject["Reward10"].ToList();
					if (TianJiDaBiReward.DataDict.ContainsKey(tianJiDaBiReward.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TianJiDaBiReward.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tianJiDaBiReward.id));
					}
					else
					{
						TianJiDaBiReward.DataDict.Add(tianJiDaBiReward.id, tianJiDaBiReward);
						TianJiDaBiReward.DataList.Add(tianJiDaBiReward);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TianJiDaBiReward.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TianJiDaBiReward.OnInitFinishAction != null)
			{
				TianJiDaBiReward.OnInitFinishAction();
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x00214554 File Offset: 0x00212754
		private static void InitReward()
		{
			if (!TianJiDaBiReward.rewardInited)
			{
				TianJiDaBiReward.rewardInited = true;
				foreach (TianJiDaBiReward tianJiDaBiReward in TianJiDaBiReward.DataList)
				{
					tianJiDaBiReward.RewardDict = new Dictionary<int, List<int>>();
					tianJiDaBiReward.RewardDict[1] = tianJiDaBiReward.Reward1;
					tianJiDaBiReward.RewardDict[2] = tianJiDaBiReward.Reward2;
					tianJiDaBiReward.RewardDict[3] = tianJiDaBiReward.Reward3;
					tianJiDaBiReward.RewardDict[4] = tianJiDaBiReward.Reward4;
					tianJiDaBiReward.RewardDict[5] = tianJiDaBiReward.Reward5;
					tianJiDaBiReward.RewardDict[6] = tianJiDaBiReward.Reward6;
					tianJiDaBiReward.RewardDict[7] = tianJiDaBiReward.Reward7;
					tianJiDaBiReward.RewardDict[8] = tianJiDaBiReward.Reward8;
					tianJiDaBiReward.RewardDict[9] = tianJiDaBiReward.Reward9;
					tianJiDaBiReward.RewardDict[10] = tianJiDaBiReward.Reward10;
				}
			}
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x00214678 File Offset: 0x00212878
		public static List<int> GetReward(int liuPai, int rank)
		{
			TianJiDaBiReward.InitReward();
			TianJiDaBiReward tianJiDaBiReward;
			if (TianJiDaBiReward.DataDict.ContainsKey(liuPai))
			{
				tianJiDaBiReward = TianJiDaBiReward.DataDict[liuPai];
			}
			else
			{
				tianJiDaBiReward = TianJiDaBiReward.DataDict[999];
			}
			if (tianJiDaBiReward.RewardDict.ContainsKey(rank))
			{
				return tianJiDaBiReward.RewardDict[rank];
			}
			Debug.LogError(string.Format("获取天机大比奖励时使用了未计划的排名{0}，返回了空列表", rank));
			return new List<int>();
		}

		// Token: 0x04005036 RID: 20534
		public static Dictionary<int, TianJiDaBiReward> DataDict = new Dictionary<int, TianJiDaBiReward>();

		// Token: 0x04005037 RID: 20535
		public static List<TianJiDaBiReward> DataList = new List<TianJiDaBiReward>();

		// Token: 0x04005038 RID: 20536
		public static Action OnInitFinishAction = new Action(TianJiDaBiReward.OnInitFinish);

		// Token: 0x04005039 RID: 20537
		public int id;

		// Token: 0x0400503A RID: 20538
		public List<int> Reward1 = new List<int>();

		// Token: 0x0400503B RID: 20539
		public List<int> Reward2 = new List<int>();

		// Token: 0x0400503C RID: 20540
		public List<int> Reward3 = new List<int>();

		// Token: 0x0400503D RID: 20541
		public List<int> Reward4 = new List<int>();

		// Token: 0x0400503E RID: 20542
		public List<int> Reward5 = new List<int>();

		// Token: 0x0400503F RID: 20543
		public List<int> Reward6 = new List<int>();

		// Token: 0x04005040 RID: 20544
		public List<int> Reward7 = new List<int>();

		// Token: 0x04005041 RID: 20545
		public List<int> Reward8 = new List<int>();

		// Token: 0x04005042 RID: 20546
		public List<int> Reward9 = new List<int>();

		// Token: 0x04005043 RID: 20547
		public List<int> Reward10 = new List<int>();

		// Token: 0x04005044 RID: 20548
		public Dictionary<int, List<int>> RewardDict;

		// Token: 0x04005045 RID: 20549
		private static bool rewardInited;
	}
}
