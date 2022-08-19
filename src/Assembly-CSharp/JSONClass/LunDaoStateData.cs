using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000889 RID: 2185
	public class LunDaoStateData : IJSONClass
	{
		// Token: 0x06004037 RID: 16439 RVA: 0x001B6534 File Offset: 0x001B4734
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LunDaoStateData.list)
			{
				try
				{
					LunDaoStateData lunDaoStateData = new LunDaoStateData();
					lunDaoStateData.id = jsonobject["id"].I;
					lunDaoStateData.Time = jsonobject["Time"].I;
					lunDaoStateData.WuDaoExp = jsonobject["WuDaoExp"].I;
					lunDaoStateData.WuDaoZhi = jsonobject["WuDaoZhi"].I;
					lunDaoStateData.LingGanXiaoHao = jsonobject["LingGanXiaoHao"].I;
					lunDaoStateData.ZhuangTaiInfo = jsonobject["ZhuangTaiInfo"].Str;
					lunDaoStateData.MiaoShu = jsonobject["MiaoShu"].Str;
					lunDaoStateData.MiaoShu1 = jsonobject["MiaoShu1"].Str;
					if (LunDaoStateData.DataDict.ContainsKey(lunDaoStateData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LunDaoStateData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lunDaoStateData.id));
					}
					else
					{
						LunDaoStateData.DataDict.Add(lunDaoStateData.id, lunDaoStateData);
						LunDaoStateData.DataList.Add(lunDaoStateData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LunDaoStateData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LunDaoStateData.OnInitFinishAction != null)
			{
				LunDaoStateData.OnInitFinishAction();
			}
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D53 RID: 15699
		public static Dictionary<int, LunDaoStateData> DataDict = new Dictionary<int, LunDaoStateData>();

		// Token: 0x04003D54 RID: 15700
		public static List<LunDaoStateData> DataList = new List<LunDaoStateData>();

		// Token: 0x04003D55 RID: 15701
		public static Action OnInitFinishAction = new Action(LunDaoStateData.OnInitFinish);

		// Token: 0x04003D56 RID: 15702
		public int id;

		// Token: 0x04003D57 RID: 15703
		public int Time;

		// Token: 0x04003D58 RID: 15704
		public int WuDaoExp;

		// Token: 0x04003D59 RID: 15705
		public int WuDaoZhi;

		// Token: 0x04003D5A RID: 15706
		public int LingGanXiaoHao;

		// Token: 0x04003D5B RID: 15707
		public string ZhuangTaiInfo;

		// Token: 0x04003D5C RID: 15708
		public string MiaoShu;

		// Token: 0x04003D5D RID: 15709
		public string MiaoShu1;
	}
}
