using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE4 RID: 2788
	public class AllMapLuDainType : IJSONClass
	{
		// Token: 0x060046FA RID: 18170 RVA: 0x001E5E98 File Offset: 0x001E4098
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

		// Token: 0x060046FB RID: 18171 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F74 RID: 16244
		public static Dictionary<int, AllMapLuDainType> DataDict = new Dictionary<int, AllMapLuDainType>();

		// Token: 0x04003F75 RID: 16245
		public static List<AllMapLuDainType> DataList = new List<AllMapLuDainType>();

		// Token: 0x04003F76 RID: 16246
		public static Action OnInitFinishAction = new Action(AllMapLuDainType.OnInitFinish);

		// Token: 0x04003F77 RID: 16247
		public int id;

		// Token: 0x04003F78 RID: 16248
		public int MapType;

		// Token: 0x04003F79 RID: 16249
		public string LuDianName;
	}
}
