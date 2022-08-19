using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200074E RID: 1870
	public class AllMapReset : IJSONClass
	{
		// Token: 0x06003B4C RID: 15180 RVA: 0x00198080 File Offset: 0x00196280
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapReset.list)
			{
				try
				{
					AllMapReset allMapReset = new AllMapReset();
					allMapReset.id = jsonobject["id"].I;
					allMapReset.Type = jsonobject["Type"].I;
					allMapReset.resetTiem = jsonobject["resetTiem"].I;
					allMapReset.CanSame = jsonobject["CanSame"].I;
					allMapReset.percent = jsonobject["percent"].I;
					allMapReset.max = jsonobject["max"].I;
					allMapReset.name = jsonobject["name"].Str;
					allMapReset.Icon = jsonobject["Icon"].Str;
					allMapReset.Act = jsonobject["Act"].Str;
					allMapReset.qujian = jsonobject["qujian"].ToList();
					if (AllMapReset.DataDict.ContainsKey(allMapReset.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapReset.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapReset.id));
					}
					else
					{
						AllMapReset.DataDict.Add(allMapReset.id, allMapReset);
						AllMapReset.DataList.Add(allMapReset);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapReset.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapReset.OnInitFinishAction != null)
			{
				AllMapReset.OnInitFinishAction();
			}
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040033F1 RID: 13297
		public static Dictionary<int, AllMapReset> DataDict = new Dictionary<int, AllMapReset>();

		// Token: 0x040033F2 RID: 13298
		public static List<AllMapReset> DataList = new List<AllMapReset>();

		// Token: 0x040033F3 RID: 13299
		public static Action OnInitFinishAction = new Action(AllMapReset.OnInitFinish);

		// Token: 0x040033F4 RID: 13300
		public int id;

		// Token: 0x040033F5 RID: 13301
		public int Type;

		// Token: 0x040033F6 RID: 13302
		public int resetTiem;

		// Token: 0x040033F7 RID: 13303
		public int CanSame;

		// Token: 0x040033F8 RID: 13304
		public int percent;

		// Token: 0x040033F9 RID: 13305
		public int max;

		// Token: 0x040033FA RID: 13306
		public string name;

		// Token: 0x040033FB RID: 13307
		public string Icon;

		// Token: 0x040033FC RID: 13308
		public string Act;

		// Token: 0x040033FD RID: 13309
		public List<int> qujian = new List<int>();
	}
}
