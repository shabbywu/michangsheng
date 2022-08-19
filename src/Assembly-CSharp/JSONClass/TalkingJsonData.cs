using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200096E RID: 2414
	public class TalkingJsonData : IJSONClass
	{
		// Token: 0x060043CA RID: 17354 RVA: 0x001CDBD8 File Offset: 0x001CBDD8
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

		// Token: 0x060043CB RID: 17355 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044CD RID: 17613
		public static Dictionary<int, TalkingJsonData> DataDict = new Dictionary<int, TalkingJsonData>();

		// Token: 0x040044CE RID: 17614
		public static List<TalkingJsonData> DataList = new List<TalkingJsonData>();

		// Token: 0x040044CF RID: 17615
		public static Action OnInitFinishAction = new Action(TalkingJsonData.OnInitFinish);

		// Token: 0x040044D0 RID: 17616
		public int id;

		// Token: 0x040044D1 RID: 17617
		public int headID;

		// Token: 0x040044D2 RID: 17618
		public int menu1;

		// Token: 0x040044D3 RID: 17619
		public int menu2;

		// Token: 0x040044D4 RID: 17620
		public int menu3;

		// Token: 0x040044D5 RID: 17621
		public int menu4;

		// Token: 0x040044D6 RID: 17622
		public int menu5;

		// Token: 0x040044D7 RID: 17623
		public string sayname;

		// Token: 0x040044D8 RID: 17624
		public string title;

		// Token: 0x040044D9 RID: 17625
		public string body;

		// Token: 0x040044DA RID: 17626
		public string funcFailMsg;

		// Token: 0x040044DB RID: 17627
		public string func1;

		// Token: 0x040044DC RID: 17628
		public string func2;

		// Token: 0x040044DD RID: 17629
		public string func3;

		// Token: 0x040044DE RID: 17630
		public string func4;

		// Token: 0x040044DF RID: 17631
		public string funcargs4;

		// Token: 0x040044E0 RID: 17632
		public string func5;

		// Token: 0x040044E1 RID: 17633
		public string funcargs5;

		// Token: 0x040044E2 RID: 17634
		public List<int> funcargs1 = new List<int>();

		// Token: 0x040044E3 RID: 17635
		public List<int> funcargs2 = new List<int>();

		// Token: 0x040044E4 RID: 17636
		public List<int> funcargs3 = new List<int>();
	}
}
