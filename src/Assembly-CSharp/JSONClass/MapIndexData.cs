using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200088A RID: 2186
	public class MapIndexData : IJSONClass
	{
		// Token: 0x0600403B RID: 16443 RVA: 0x001B671C File Offset: 0x001B491C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.MapIndexData.list)
			{
				try
				{
					MapIndexData mapIndexData = new MapIndexData();
					mapIndexData.id = jsonobject["id"].I;
					mapIndexData.mapIndex = jsonobject["mapIndex"].I;
					mapIndexData.StrValue = jsonobject["StrValue"].Str;
					mapIndexData.name = jsonobject["name"].Str;
					if (MapIndexData.DataDict.ContainsKey(mapIndexData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典MapIndexData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", mapIndexData.id));
					}
					else
					{
						MapIndexData.DataDict.Add(mapIndexData.id, mapIndexData);
						MapIndexData.DataList.Add(mapIndexData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典MapIndexData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (MapIndexData.OnInitFinishAction != null)
			{
				MapIndexData.OnInitFinishAction();
			}
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D5E RID: 15710
		public static Dictionary<int, MapIndexData> DataDict = new Dictionary<int, MapIndexData>();

		// Token: 0x04003D5F RID: 15711
		public static List<MapIndexData> DataList = new List<MapIndexData>();

		// Token: 0x04003D60 RID: 15712
		public static Action OnInitFinishAction = new Action(MapIndexData.OnInitFinish);

		// Token: 0x04003D61 RID: 15713
		public int id;

		// Token: 0x04003D62 RID: 15714
		public int mapIndex;

		// Token: 0x04003D63 RID: 15715
		public string StrValue;

		// Token: 0x04003D64 RID: 15716
		public string name;
	}
}
