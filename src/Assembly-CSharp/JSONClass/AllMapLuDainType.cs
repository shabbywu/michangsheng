using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200074C RID: 1868
	public class AllMapLuDainType : IJSONClass
	{
		// Token: 0x06003B44 RID: 15172 RVA: 0x00197CA4 File Offset: 0x00195EA4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapLuDainType.list)
			{
				try
				{
					AllMapLuDainType allMapLuDainType = new AllMapLuDainType();
					allMapLuDainType.id = jsonobject["id"].I;
					allMapLuDainType.MapType = jsonobject["MapType"].I;
					allMapLuDainType.LuDianName = jsonobject["LuDianName"].Str;
					if (AllMapLuDainType.DataDict.ContainsKey(allMapLuDainType.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapLuDainType.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapLuDainType.id));
					}
					else
					{
						AllMapLuDainType.DataDict.Add(allMapLuDainType.id, allMapLuDainType);
						AllMapLuDainType.DataList.Add(allMapLuDainType);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapLuDainType.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapLuDainType.OnInitFinishAction != null)
			{
				AllMapLuDainType.OnInitFinishAction();
			}
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040033DB RID: 13275
		public static Dictionary<int, AllMapLuDainType> DataDict = new Dictionary<int, AllMapLuDainType>();

		// Token: 0x040033DC RID: 13276
		public static List<AllMapLuDainType> DataList = new List<AllMapLuDainType>();

		// Token: 0x040033DD RID: 13277
		public static Action OnInitFinishAction = new Action(AllMapLuDainType.OnInitFinish);

		// Token: 0x040033DE RID: 13278
		public int id;

		// Token: 0x040033DF RID: 13279
		public int MapType;

		// Token: 0x040033E0 RID: 13280
		public string LuDianName;
	}
}
