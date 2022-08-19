using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E1 RID: 2273
	public class ShuangXiuMiShu : IJSONClass
	{
		// Token: 0x06004197 RID: 16791 RVA: 0x001C0F14 File Offset: 0x001BF114
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

		// Token: 0x06004198 RID: 16792 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040FE RID: 16638
		public static Dictionary<int, ShuangXiuMiShu> DataDict = new Dictionary<int, ShuangXiuMiShu>();

		// Token: 0x040040FF RID: 16639
		public static List<ShuangXiuMiShu> DataList = new List<ShuangXiuMiShu>();

		// Token: 0x04004100 RID: 16640
		public static Action OnInitFinishAction = new Action(ShuangXiuMiShu.OnInitFinish);

		// Token: 0x04004101 RID: 16641
		public int id;

		// Token: 0x04004102 RID: 16642
		public int pinjietype;

		// Token: 0x04004103 RID: 16643
		public int ningliantype;

		// Token: 0x04004104 RID: 16644
		public int jingyuanbeilv;

		// Token: 0x04004105 RID: 16645
		public int jingyuantype;

		// Token: 0x04004106 RID: 16646
		public string name;

		// Token: 0x04004107 RID: 16647
		public string desc;
	}
}
