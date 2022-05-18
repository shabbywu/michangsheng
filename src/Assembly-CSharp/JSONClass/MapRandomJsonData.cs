using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C19 RID: 3097
	public class MapRandomJsonData : IJSONClass
	{
		// Token: 0x06004BCD RID: 19405 RVA: 0x001FFE78 File Offset: 0x001FE078
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.MapRandomJsonData.list)
			{
				try
				{
					MapRandomJsonData mapRandomJsonData = new MapRandomJsonData();
					mapRandomJsonData.id = jsonobject["id"].I;
					mapRandomJsonData.EventType = jsonobject["EventType"].I;
					mapRandomJsonData.EventList = jsonobject["EventList"].I;
					mapRandomJsonData.EventData = jsonobject["EventData"].I;
					mapRandomJsonData.MosterID = jsonobject["MosterID"].I;
					mapRandomJsonData.EventCastTime = jsonobject["EventCastTime"].I;
					mapRandomJsonData.percent = jsonobject["percent"].I;
					mapRandomJsonData.once = jsonobject["once"].I;
					mapRandomJsonData.EventName = jsonobject["EventName"].Str;
					mapRandomJsonData.Icon = jsonobject["Icon"].Str;
					mapRandomJsonData.StartTime = jsonobject["StartTime"].Str;
					mapRandomJsonData.EndTime = jsonobject["EndTime"].Str;
					mapRandomJsonData.fuhao = jsonobject["fuhao"].Str;
					mapRandomJsonData.EventLv = jsonobject["EventLv"].ToList();
					mapRandomJsonData.EventValue = jsonobject["EventValue"].ToList();
					if (MapRandomJsonData.DataDict.ContainsKey(mapRandomJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典MapRandomJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", mapRandomJsonData.id));
					}
					else
					{
						MapRandomJsonData.DataDict.Add(mapRandomJsonData.id, mapRandomJsonData);
						MapRandomJsonData.DataList.Add(mapRandomJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典MapRandomJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (MapRandomJsonData.OnInitFinishAction != null)
			{
				MapRandomJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048BE RID: 18622
		public static Dictionary<int, MapRandomJsonData> DataDict = new Dictionary<int, MapRandomJsonData>();

		// Token: 0x040048BF RID: 18623
		public static List<MapRandomJsonData> DataList = new List<MapRandomJsonData>();

		// Token: 0x040048C0 RID: 18624
		public static Action OnInitFinishAction = new Action(MapRandomJsonData.OnInitFinish);

		// Token: 0x040048C1 RID: 18625
		public int id;

		// Token: 0x040048C2 RID: 18626
		public int EventType;

		// Token: 0x040048C3 RID: 18627
		public int EventList;

		// Token: 0x040048C4 RID: 18628
		public int EventData;

		// Token: 0x040048C5 RID: 18629
		public int MosterID;

		// Token: 0x040048C6 RID: 18630
		public int EventCastTime;

		// Token: 0x040048C7 RID: 18631
		public int percent;

		// Token: 0x040048C8 RID: 18632
		public int once;

		// Token: 0x040048C9 RID: 18633
		public string EventName;

		// Token: 0x040048CA RID: 18634
		public string Icon;

		// Token: 0x040048CB RID: 18635
		public string StartTime;

		// Token: 0x040048CC RID: 18636
		public string EndTime;

		// Token: 0x040048CD RID: 18637
		public string fuhao;

		// Token: 0x040048CE RID: 18638
		public List<int> EventLv = new List<int>();

		// Token: 0x040048CF RID: 18639
		public List<int> EventValue = new List<int>();
	}
}
