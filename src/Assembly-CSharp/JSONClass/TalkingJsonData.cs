using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF2 RID: 3314
	public class TalkingJsonData : IJSONClass
	{
		// Token: 0x06004F30 RID: 20272 RVA: 0x00213630 File Offset: 0x00211830
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TalkingJsonData.list)
			{
				try
				{
					TalkingJsonData talkingJsonData = new TalkingJsonData();
					talkingJsonData.id = jsonobject["id"].I;
					talkingJsonData.headID = jsonobject["headID"].I;
					talkingJsonData.menu1 = jsonobject["menu1"].I;
					talkingJsonData.menu2 = jsonobject["menu2"].I;
					talkingJsonData.menu3 = jsonobject["menu3"].I;
					talkingJsonData.menu4 = jsonobject["menu4"].I;
					talkingJsonData.menu5 = jsonobject["menu5"].I;
					talkingJsonData.sayname = jsonobject["sayname"].Str;
					talkingJsonData.title = jsonobject["title"].Str;
					talkingJsonData.body = jsonobject["body"].Str;
					talkingJsonData.funcFailMsg = jsonobject["funcFailMsg"].Str;
					talkingJsonData.func1 = jsonobject["func1"].Str;
					talkingJsonData.func2 = jsonobject["func2"].Str;
					talkingJsonData.func3 = jsonobject["func3"].Str;
					talkingJsonData.func4 = jsonobject["func4"].Str;
					talkingJsonData.funcargs4 = jsonobject["funcargs4"].Str;
					talkingJsonData.func5 = jsonobject["func5"].Str;
					talkingJsonData.funcargs5 = jsonobject["funcargs5"].Str;
					talkingJsonData.funcargs1 = jsonobject["funcargs1"].ToList();
					talkingJsonData.funcargs2 = jsonobject["funcargs2"].ToList();
					talkingJsonData.funcargs3 = jsonobject["funcargs3"].ToList();
					if (TalkingJsonData.DataDict.ContainsKey(talkingJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TalkingJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", talkingJsonData.id));
					}
					else
					{
						TalkingJsonData.DataDict.Add(talkingJsonData.id, talkingJsonData);
						TalkingJsonData.DataList.Add(talkingJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TalkingJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TalkingJsonData.OnInitFinishAction != null)
			{
				TalkingJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FDD RID: 20445
		public static Dictionary<int, TalkingJsonData> DataDict = new Dictionary<int, TalkingJsonData>();

		// Token: 0x04004FDE RID: 20446
		public static List<TalkingJsonData> DataList = new List<TalkingJsonData>();

		// Token: 0x04004FDF RID: 20447
		public static Action OnInitFinishAction = new Action(TalkingJsonData.OnInitFinish);

		// Token: 0x04004FE0 RID: 20448
		public int id;

		// Token: 0x04004FE1 RID: 20449
		public int headID;

		// Token: 0x04004FE2 RID: 20450
		public int menu1;

		// Token: 0x04004FE3 RID: 20451
		public int menu2;

		// Token: 0x04004FE4 RID: 20452
		public int menu3;

		// Token: 0x04004FE5 RID: 20453
		public int menu4;

		// Token: 0x04004FE6 RID: 20454
		public int menu5;

		// Token: 0x04004FE7 RID: 20455
		public string sayname;

		// Token: 0x04004FE8 RID: 20456
		public string title;

		// Token: 0x04004FE9 RID: 20457
		public string body;

		// Token: 0x04004FEA RID: 20458
		public string funcFailMsg;

		// Token: 0x04004FEB RID: 20459
		public string func1;

		// Token: 0x04004FEC RID: 20460
		public string func2;

		// Token: 0x04004FED RID: 20461
		public string func3;

		// Token: 0x04004FEE RID: 20462
		public string func4;

		// Token: 0x04004FEF RID: 20463
		public string funcargs4;

		// Token: 0x04004FF0 RID: 20464
		public string func5;

		// Token: 0x04004FF1 RID: 20465
		public string funcargs5;

		// Token: 0x04004FF2 RID: 20466
		public List<int> funcargs1 = new List<int>();

		// Token: 0x04004FF3 RID: 20467
		public List<int> funcargs2 = new List<int>();

		// Token: 0x04004FF4 RID: 20468
		public List<int> funcargs3 = new List<int>();
	}
}
