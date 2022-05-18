using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CEF RID: 3311
	public class StaticValueSay : IJSONClass
	{
		// Token: 0x06004F24 RID: 20260 RVA: 0x0021314C File Offset: 0x0021134C
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

		// Token: 0x06004F25 RID: 20261 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FC2 RID: 20418
		public static Dictionary<int, StaticValueSay> DataDict = new Dictionary<int, StaticValueSay>();

		// Token: 0x04004FC3 RID: 20419
		public static List<StaticValueSay> DataList = new List<StaticValueSay>();

		// Token: 0x04004FC4 RID: 20420
		public static Action OnInitFinishAction = new Action(StaticValueSay.OnInitFinish);

		// Token: 0x04004FC5 RID: 20421
		public int id;

		// Token: 0x04004FC6 RID: 20422
		public int StaticID;

		// Token: 0x04004FC7 RID: 20423
		public int staticValue;

		// Token: 0x04004FC8 RID: 20424
		public string ChinaText;
	}
}
