using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD7 RID: 3031
	public class helpTextJsonData : IJSONClass
	{
		// Token: 0x06004AC4 RID: 19140 RVA: 0x001F9FF8 File Offset: 0x001F81F8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.helpTextJsonData.list)
			{
				try
				{
					helpTextJsonData helpTextJsonData = new helpTextJsonData();
					helpTextJsonData.id = jsonobject["id"].I;
					helpTextJsonData.link = jsonobject["link"].I;
					helpTextJsonData.Titile = jsonobject["Titile"].Str;
					helpTextJsonData.desc = jsonobject["desc"].Str;
					if (helpTextJsonData.DataDict.ContainsKey(helpTextJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典helpTextJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", helpTextJsonData.id));
					}
					else
					{
						helpTextJsonData.DataDict.Add(helpTextJsonData.id, helpTextJsonData);
						helpTextJsonData.DataList.Add(helpTextJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典helpTextJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (helpTextJsonData.OnInitFinishAction != null)
			{
				helpTextJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400469D RID: 18077
		public static Dictionary<int, helpTextJsonData> DataDict = new Dictionary<int, helpTextJsonData>();

		// Token: 0x0400469E RID: 18078
		public static List<helpTextJsonData> DataList = new List<helpTextJsonData>();

		// Token: 0x0400469F RID: 18079
		public static Action OnInitFinishAction = new Action(helpTextJsonData.OnInitFinish);

		// Token: 0x040046A0 RID: 18080
		public int id;

		// Token: 0x040046A1 RID: 18081
		public int link;

		// Token: 0x040046A2 RID: 18082
		public string Titile;

		// Token: 0x040046A3 RID: 18083
		public string desc;
	}
}
