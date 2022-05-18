using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C1C RID: 3100
	public class MessageJsonData : IJSONClass
	{
		// Token: 0x06004BD9 RID: 19417 RVA: 0x00200414 File Offset: 0x001FE614
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.MessageJsonData.list)
			{
				try
				{
					MessageJsonData messageJsonData = new MessageJsonData();
					messageJsonData.id = jsonobject["id"].I;
					messageJsonData.headID = jsonobject["headID"].I;
					messageJsonData.messageInfo = jsonobject["messageInfo"].Str;
					messageJsonData.title = jsonobject["title"].Str;
					messageJsonData.body = jsonobject["body"].Str;
					messageJsonData.func1 = jsonobject["func1"].Str;
					messageJsonData.funcargs1 = jsonobject["funcargs1"].Str;
					messageJsonData.func2 = jsonobject["func2"].Str;
					messageJsonData.funcargs2 = jsonobject["funcargs2"].Str;
					messageJsonData.func3 = jsonobject["func3"].Str;
					messageJsonData.funcargs3 = jsonobject["funcargs3"].Str;
					messageJsonData.func4 = jsonobject["func4"].Str;
					messageJsonData.funcargs4 = jsonobject["funcargs4"].Str;
					messageJsonData.func5 = jsonobject["func5"].Str;
					messageJsonData.funcargs5 = jsonobject["funcargs5"].Str;
					if (MessageJsonData.DataDict.ContainsKey(messageJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典MessageJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", messageJsonData.id));
					}
					else
					{
						MessageJsonData.DataDict.Add(messageJsonData.id, messageJsonData);
						MessageJsonData.DataList.Add(messageJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典MessageJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (MessageJsonData.OnInitFinishAction != null)
			{
				MessageJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048E3 RID: 18659
		public static Dictionary<int, MessageJsonData> DataDict = new Dictionary<int, MessageJsonData>();

		// Token: 0x040048E4 RID: 18660
		public static List<MessageJsonData> DataList = new List<MessageJsonData>();

		// Token: 0x040048E5 RID: 18661
		public static Action OnInitFinishAction = new Action(MessageJsonData.OnInitFinish);

		// Token: 0x040048E6 RID: 18662
		public int id;

		// Token: 0x040048E7 RID: 18663
		public int headID;

		// Token: 0x040048E8 RID: 18664
		public string messageInfo;

		// Token: 0x040048E9 RID: 18665
		public string title;

		// Token: 0x040048EA RID: 18666
		public string body;

		// Token: 0x040048EB RID: 18667
		public string func1;

		// Token: 0x040048EC RID: 18668
		public string funcargs1;

		// Token: 0x040048ED RID: 18669
		public string func2;

		// Token: 0x040048EE RID: 18670
		public string funcargs2;

		// Token: 0x040048EF RID: 18671
		public string func3;

		// Token: 0x040048F0 RID: 18672
		public string funcargs3;

		// Token: 0x040048F1 RID: 18673
		public string func4;

		// Token: 0x040048F2 RID: 18674
		public string funcargs4;

		// Token: 0x040048F3 RID: 18675
		public string func5;

		// Token: 0x040048F4 RID: 18676
		public string funcargs5;
	}
}
