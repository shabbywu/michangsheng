using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200088E RID: 2190
	public class MessageJsonData : IJSONClass
	{
		// Token: 0x0600404B RID: 16459 RVA: 0x001B6EF0 File Offset: 0x001B50F0
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

		// Token: 0x0600404C RID: 16460 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D8A RID: 15754
		public static Dictionary<int, MessageJsonData> DataDict = new Dictionary<int, MessageJsonData>();

		// Token: 0x04003D8B RID: 15755
		public static List<MessageJsonData> DataList = new List<MessageJsonData>();

		// Token: 0x04003D8C RID: 15756
		public static Action OnInitFinishAction = new Action(MessageJsonData.OnInitFinish);

		// Token: 0x04003D8D RID: 15757
		public int id;

		// Token: 0x04003D8E RID: 15758
		public int headID;

		// Token: 0x04003D8F RID: 15759
		public string messageInfo;

		// Token: 0x04003D90 RID: 15760
		public string title;

		// Token: 0x04003D91 RID: 15761
		public string body;

		// Token: 0x04003D92 RID: 15762
		public string func1;

		// Token: 0x04003D93 RID: 15763
		public string funcargs1;

		// Token: 0x04003D94 RID: 15764
		public string func2;

		// Token: 0x04003D95 RID: 15765
		public string funcargs2;

		// Token: 0x04003D96 RID: 15766
		public string func3;

		// Token: 0x04003D97 RID: 15767
		public string funcargs3;

		// Token: 0x04003D98 RID: 15768
		public string func4;

		// Token: 0x04003D99 RID: 15769
		public string funcargs4;

		// Token: 0x04003D9A RID: 15770
		public string func5;

		// Token: 0x04003D9B RID: 15771
		public string funcargs5;
	}
}
