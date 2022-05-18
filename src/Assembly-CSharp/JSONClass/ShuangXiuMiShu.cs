using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C6E RID: 3182
	public class ShuangXiuMiShu : IJSONClass
	{
		// Token: 0x06004D21 RID: 19745 RVA: 0x0020914C File Offset: 0x0020734C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShuangXiuMiShu.list)
			{
				try
				{
					ShuangXiuMiShu shuangXiuMiShu = new ShuangXiuMiShu();
					shuangXiuMiShu.id = jsonobject["id"].I;
					shuangXiuMiShu.pinjietype = jsonobject["pinjietype"].I;
					shuangXiuMiShu.ningliantype = jsonobject["ningliantype"].I;
					shuangXiuMiShu.jingyuanbeilv = jsonobject["jingyuanbeilv"].I;
					shuangXiuMiShu.jingyuantype = jsonobject["jingyuantype"].I;
					shuangXiuMiShu.name = jsonobject["name"].Str;
					shuangXiuMiShu.desc = jsonobject["desc"].Str;
					if (ShuangXiuMiShu.DataDict.ContainsKey(shuangXiuMiShu.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShuangXiuMiShu.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shuangXiuMiShu.id));
					}
					else
					{
						ShuangXiuMiShu.DataDict.Add(shuangXiuMiShu.id, shuangXiuMiShu);
						ShuangXiuMiShu.DataList.Add(shuangXiuMiShu);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShuangXiuMiShu.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShuangXiuMiShu.OnInitFinishAction != null)
			{
				ShuangXiuMiShu.OnInitFinishAction();
			}
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C48 RID: 19528
		public static Dictionary<int, ShuangXiuMiShu> DataDict = new Dictionary<int, ShuangXiuMiShu>();

		// Token: 0x04004C49 RID: 19529
		public static List<ShuangXiuMiShu> DataList = new List<ShuangXiuMiShu>();

		// Token: 0x04004C4A RID: 19530
		public static Action OnInitFinishAction = new Action(ShuangXiuMiShu.OnInitFinish);

		// Token: 0x04004C4B RID: 19531
		public int id;

		// Token: 0x04004C4C RID: 19532
		public int pinjietype;

		// Token: 0x04004C4D RID: 19533
		public int ningliantype;

		// Token: 0x04004C4E RID: 19534
		public int jingyuanbeilv;

		// Token: 0x04004C4F RID: 19535
		public int jingyuantype;

		// Token: 0x04004C50 RID: 19536
		public string name;

		// Token: 0x04004C51 RID: 19537
		public string desc;
	}
}
