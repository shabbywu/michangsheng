using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSONClass
{
	// Token: 0x02000975 RID: 2421
	public class TianJiDaBiReward : IJSONClass
	{
		// Token: 0x060043E6 RID: 17382 RVA: 0x001CEA78 File Offset: 0x001CCC78
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

		// Token: 0x060043E7 RID: 17383 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x001CEC7C File Offset: 0x001CCE7C
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

		// Token: 0x060043E9 RID: 17385 RVA: 0x001CEDA0 File Offset: 0x001CCFA0
		public static List<int> GetReward(int liuPai, int rank)
		{
			TianJiDaBiReward.InitReward();
			TianJiDaBiReward tianJiDaBiReward = null;
			if (TianJiDaBiReward.DataDict.ContainsKey(liuPai))
			{
				tianJiDaBiReward = TianJiDaBiReward.DataDict[liuPai];
			}
			else if (TianJiDaBiReward.DataDict.ContainsKey(999))
			{
				tianJiDaBiReward = TianJiDaBiReward.DataDict[999];
			}
			else
			{
				Debug.LogError("天机大比奖励无法获取到保底流派999");
				new List<int>();
			}
			if (tianJiDaBiReward.RewardDict.ContainsKey(rank))
			{
				return tianJiDaBiReward.RewardDict[rank];
			}
			Debug.LogError(string.Format("获取天机大比奖励时使用了未计划的排名{0}，返回了空列表", rank));
			return new List<int>();
		}

		// Token: 0x04004526 RID: 17702
		public static Dictionary<int, TianJiDaBiReward> DataDict = new Dictionary<int, TianJiDaBiReward>();

		// Token: 0x04004527 RID: 17703
		public static List<TianJiDaBiReward> DataList = new List<TianJiDaBiReward>();

		// Token: 0x04004528 RID: 17704
		public static Action OnInitFinishAction = new Action(TianJiDaBiReward.OnInitFinish);

		// Token: 0x04004529 RID: 17705
		public int id;

		// Token: 0x0400452A RID: 17706
		public List<int> Reward1 = new List<int>();

		// Token: 0x0400452B RID: 17707
		public List<int> Reward2 = new List<int>();

		// Token: 0x0400452C RID: 17708
		public List<int> Reward3 = new List<int>();

		// Token: 0x0400452D RID: 17709
		public List<int> Reward4 = new List<int>();

		// Token: 0x0400452E RID: 17710
		public List<int> Reward5 = new List<int>();

		// Token: 0x0400452F RID: 17711
		public List<int> Reward6 = new List<int>();

		// Token: 0x04004530 RID: 17712
		public List<int> Reward7 = new List<int>();

		// Token: 0x04004531 RID: 17713
		public List<int> Reward8 = new List<int>();

		// Token: 0x04004532 RID: 17714
		public List<int> Reward9 = new List<int>();

		// Token: 0x04004533 RID: 17715
		public List<int> Reward10 = new List<int>();

		// Token: 0x04004534 RID: 17716
		public Dictionary<int, List<int>> RewardDict;

		// Token: 0x04004535 RID: 17717
		private static bool rewardInited;
	}
}
