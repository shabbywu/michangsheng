using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC6 RID: 3014
	public class DrawCardToLevelJsonData : IJSONClass
	{
		// Token: 0x06004A80 RID: 19072 RVA: 0x001F87D0 File Offset: 0x001F69D0
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

		// Token: 0x06004A81 RID: 19073 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004616 RID: 17942
		public static Dictionary<int, DrawCardToLevelJsonData> DataDict = new Dictionary<int, DrawCardToLevelJsonData>();

		// Token: 0x04004617 RID: 17943
		public static List<DrawCardToLevelJsonData> DataList = new List<DrawCardToLevelJsonData>();

		// Token: 0x04004618 RID: 17944
		public static Action OnInitFinishAction = new Action(DrawCardToLevelJsonData.OnInitFinish);

		// Token: 0x04004619 RID: 17945
		public int id;

		// Token: 0x0400461A RID: 17946
		public int StartCard;

		// Token: 0x0400461B RID: 17947
		public int MaxDraw;

		// Token: 0x0400461C RID: 17948
		public int rundDraw;

		// Token: 0x0400461D RID: 17949
		public string Name;
	}
}
