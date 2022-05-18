using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C18 RID: 3096
	public class MapIndexData : IJSONClass
	{
		// Token: 0x06004BC9 RID: 19401 RVA: 0x001FFD28 File Offset: 0x001FDF28
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

		// Token: 0x06004BCA RID: 19402 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048B7 RID: 18615
		public static Dictionary<int, MapIndexData> DataDict = new Dictionary<int, MapIndexData>();

		// Token: 0x040048B8 RID: 18616
		public static List<MapIndexData> DataList = new List<MapIndexData>();

		// Token: 0x040048B9 RID: 18617
		public static Action OnInitFinishAction = new Action(MapIndexData.OnInitFinish);

		// Token: 0x040048BA RID: 18618
		public int id;

		// Token: 0x040048BB RID: 18619
		public int mapIndex;

		// Token: 0x040048BC RID: 18620
		public string StrValue;

		// Token: 0x040048BD RID: 18621
		public string name;
	}
}
