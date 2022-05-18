using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C17 RID: 3095
	public class LunDaoStateData : IJSONClass
	{
		// Token: 0x06004BC5 RID: 19397 RVA: 0x001FFB68 File Offset: 0x001FDD68
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

		// Token: 0x06004BC6 RID: 19398 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048AC RID: 18604
		public static Dictionary<int, LunDaoStateData> DataDict = new Dictionary<int, LunDaoStateData>();

		// Token: 0x040048AD RID: 18605
		public static List<LunDaoStateData> DataList = new List<LunDaoStateData>();

		// Token: 0x040048AE RID: 18606
		public static Action OnInitFinishAction = new Action(LunDaoStateData.OnInitFinish);

		// Token: 0x040048AF RID: 18607
		public int id;

		// Token: 0x040048B0 RID: 18608
		public int Time;

		// Token: 0x040048B1 RID: 18609
		public int WuDaoExp;

		// Token: 0x040048B2 RID: 18610
		public int WuDaoZhi;

		// Token: 0x040048B3 RID: 18611
		public int LingGanXiaoHao;

		// Token: 0x040048B4 RID: 18612
		public string ZhuangTaiInfo;

		// Token: 0x040048B5 RID: 18613
		public string MiaoShu;

		// Token: 0x040048B6 RID: 18614
		public string MiaoShu1;
	}
}
