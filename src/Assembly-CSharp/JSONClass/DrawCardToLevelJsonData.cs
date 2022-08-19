using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000832 RID: 2098
	public class DrawCardToLevelJsonData : IJSONClass
	{
		// Token: 0x06003EDA RID: 16090 RVA: 0x001AD990 File Offset: 0x001ABB90
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DrawCardToLevelJsonData.list)
			{
				try
				{
					DrawCardToLevelJsonData drawCardToLevelJsonData = new DrawCardToLevelJsonData();
					drawCardToLevelJsonData.id = jsonobject["id"].I;
					drawCardToLevelJsonData.StartCard = jsonobject["StartCard"].I;
					drawCardToLevelJsonData.MaxDraw = jsonobject["MaxDraw"].I;
					drawCardToLevelJsonData.rundDraw = jsonobject["rundDraw"].I;
					drawCardToLevelJsonData.Name = jsonobject["Name"].Str;
					if (DrawCardToLevelJsonData.DataDict.ContainsKey(drawCardToLevelJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DrawCardToLevelJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", drawCardToLevelJsonData.id));
					}
					else
					{
						DrawCardToLevelJsonData.DataDict.Add(drawCardToLevelJsonData.id, drawCardToLevelJsonData);
						DrawCardToLevelJsonData.DataList.Add(drawCardToLevelJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DrawCardToLevelJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DrawCardToLevelJsonData.OnInitFinishAction != null)
			{
				DrawCardToLevelJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A99 RID: 15001
		public static Dictionary<int, DrawCardToLevelJsonData> DataDict = new Dictionary<int, DrawCardToLevelJsonData>();

		// Token: 0x04003A9A RID: 15002
		public static List<DrawCardToLevelJsonData> DataList = new List<DrawCardToLevelJsonData>();

		// Token: 0x04003A9B RID: 15003
		public static Action OnInitFinishAction = new Action(DrawCardToLevelJsonData.OnInitFinish);

		// Token: 0x04003A9C RID: 15004
		public int id;

		// Token: 0x04003A9D RID: 15005
		public int StartCard;

		// Token: 0x04003A9E RID: 15006
		public int MaxDraw;

		// Token: 0x04003A9F RID: 15007
		public int rundDraw;

		// Token: 0x04003AA0 RID: 15008
		public string Name;
	}
}
