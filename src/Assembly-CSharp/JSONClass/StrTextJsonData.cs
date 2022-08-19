using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200096C RID: 2412
	public class StrTextJsonData : IJSONClass
	{
		// Token: 0x060043C2 RID: 17346 RVA: 0x001CD7F8 File Offset: 0x001CB9F8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StrTextJsonData.list)
			{
				try
				{
					StrTextJsonData strTextJsonData = new StrTextJsonData();
					strTextJsonData.StrID = jsonobject["StrID"].Str;
					strTextJsonData.ChinaText = jsonobject["ChinaText"].Str;
					if (StrTextJsonData.DataDict.ContainsKey(strTextJsonData.StrID))
					{
						PreloadManager.LogException("!!!错误!!!向字典StrTextJsonData.DataDict添加数据时出现重复的键，Key:" + strTextJsonData.StrID + "，已跳过，请检查配表");
					}
					else
					{
						StrTextJsonData.DataDict.Add(strTextJsonData.StrID, strTextJsonData);
						StrTextJsonData.DataList.Add(strTextJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StrTextJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StrTextJsonData.OnInitFinishAction != null)
			{
				StrTextJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044B9 RID: 17593
		public static Dictionary<string, StrTextJsonData> DataDict = new Dictionary<string, StrTextJsonData>();

		// Token: 0x040044BA RID: 17594
		public static List<StrTextJsonData> DataList = new List<StrTextJsonData>();

		// Token: 0x040044BB RID: 17595
		public static Action OnInitFinishAction = new Action(StrTextJsonData.OnInitFinish);

		// Token: 0x040044BC RID: 17596
		public string StrID;

		// Token: 0x040044BD RID: 17597
		public string ChinaText;
	}
}
