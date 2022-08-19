using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200087C RID: 2172
	public class LianQiWuWeiBiao : IJSONClass
	{
		// Token: 0x06004003 RID: 16387 RVA: 0x001B50E8 File Offset: 0x001B32E8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiWuWeiBiao.list)
			{
				try
				{
					LianQiWuWeiBiao lianQiWuWeiBiao = new LianQiWuWeiBiao();
					lianQiWuWeiBiao.id = jsonobject["id"].I;
					lianQiWuWeiBiao.value1 = jsonobject["value1"].I;
					lianQiWuWeiBiao.value2 = jsonobject["value2"].I;
					lianQiWuWeiBiao.value3 = jsonobject["value3"].I;
					lianQiWuWeiBiao.value4 = jsonobject["value4"].I;
					lianQiWuWeiBiao.value5 = jsonobject["value5"].I;
					lianQiWuWeiBiao.desc = jsonobject["desc"].Str;
					if (LianQiWuWeiBiao.DataDict.ContainsKey(lianQiWuWeiBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiWuWeiBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiWuWeiBiao.id));
					}
					else
					{
						LianQiWuWeiBiao.DataDict.Add(lianQiWuWeiBiao.id, lianQiWuWeiBiao);
						LianQiWuWeiBiao.DataList.Add(lianQiWuWeiBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiWuWeiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiWuWeiBiao.OnInitFinishAction != null)
			{
				LianQiWuWeiBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003CF1 RID: 15601
		public static Dictionary<int, LianQiWuWeiBiao> DataDict = new Dictionary<int, LianQiWuWeiBiao>();

		// Token: 0x04003CF2 RID: 15602
		public static List<LianQiWuWeiBiao> DataList = new List<LianQiWuWeiBiao>();

		// Token: 0x04003CF3 RID: 15603
		public static Action OnInitFinishAction = new Action(LianQiWuWeiBiao.OnInitFinish);

		// Token: 0x04003CF4 RID: 15604
		public int id;

		// Token: 0x04003CF5 RID: 15605
		public int value1;

		// Token: 0x04003CF6 RID: 15606
		public int value2;

		// Token: 0x04003CF7 RID: 15607
		public int value3;

		// Token: 0x04003CF8 RID: 15608
		public int value4;

		// Token: 0x04003CF9 RID: 15609
		public int value5;

		// Token: 0x04003CFA RID: 15610
		public string desc;
	}
}
