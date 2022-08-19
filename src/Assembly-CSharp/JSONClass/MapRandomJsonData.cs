using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200088B RID: 2187
	public class MapRandomJsonData : IJSONClass
	{
		// Token: 0x0600403F RID: 16447 RVA: 0x001B6894 File Offset: 0x001B4A94
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

		// Token: 0x06004040 RID: 16448 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D65 RID: 15717
		public static Dictionary<int, MapRandomJsonData> DataDict = new Dictionary<int, MapRandomJsonData>();

		// Token: 0x04003D66 RID: 15718
		public static List<MapRandomJsonData> DataList = new List<MapRandomJsonData>();

		// Token: 0x04003D67 RID: 15719
		public static Action OnInitFinishAction = new Action(MapRandomJsonData.OnInitFinish);

		// Token: 0x04003D68 RID: 15720
		public int id;

		// Token: 0x04003D69 RID: 15721
		public int EventType;

		// Token: 0x04003D6A RID: 15722
		public int EventList;

		// Token: 0x04003D6B RID: 15723
		public int EventData;

		// Token: 0x04003D6C RID: 15724
		public int MosterID;

		// Token: 0x04003D6D RID: 15725
		public int EventCastTime;

		// Token: 0x04003D6E RID: 15726
		public int percent;

		// Token: 0x04003D6F RID: 15727
		public int once;

		// Token: 0x04003D70 RID: 15728
		public string EventName;

		// Token: 0x04003D71 RID: 15729
		public string Icon;

		// Token: 0x04003D72 RID: 15730
		public string StartTime;

		// Token: 0x04003D73 RID: 15731
		public string EndTime;

		// Token: 0x04003D74 RID: 15732
		public string fuhao;

		// Token: 0x04003D75 RID: 15733
		public List<int> EventLv = new List<int>();

		// Token: 0x04003D76 RID: 15734
		public List<int> EventValue = new List<int>();
	}
}
