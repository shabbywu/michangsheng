using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200096B RID: 2411
	public class StaticValueSay : IJSONClass
	{
		// Token: 0x060043BE RID: 17342 RVA: 0x001CD680 File Offset: 0x001CB880
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticValueSay.list)
			{
				try
				{
					StaticValueSay staticValueSay = new StaticValueSay();
					staticValueSay.id = jsonobject["id"].I;
					staticValueSay.StaticID = jsonobject["StaticID"].I;
					staticValueSay.staticValue = jsonobject["staticValue"].I;
					staticValueSay.ChinaText = jsonobject["ChinaText"].Str;
					if (StaticValueSay.DataDict.ContainsKey(staticValueSay.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticValueSay.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticValueSay.id));
					}
					else
					{
						StaticValueSay.DataDict.Add(staticValueSay.id, staticValueSay);
						StaticValueSay.DataList.Add(staticValueSay);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticValueSay.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticValueSay.OnInitFinishAction != null)
			{
				StaticValueSay.OnInitFinishAction();
			}
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044B2 RID: 17586
		public static Dictionary<int, StaticValueSay> DataDict = new Dictionary<int, StaticValueSay>();

		// Token: 0x040044B3 RID: 17587
		public static List<StaticValueSay> DataList = new List<StaticValueSay>();

		// Token: 0x040044B4 RID: 17588
		public static Action OnInitFinishAction = new Action(StaticValueSay.OnInitFinish);

		// Token: 0x040044B5 RID: 17589
		public int id;

		// Token: 0x040044B6 RID: 17590
		public int StaticID;

		// Token: 0x040044B7 RID: 17591
		public int staticValue;

		// Token: 0x040044B8 RID: 17592
		public string ChinaText;
	}
}
