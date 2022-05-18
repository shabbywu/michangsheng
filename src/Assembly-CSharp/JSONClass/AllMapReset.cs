using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE6 RID: 2790
	public class AllMapReset : IJSONClass
	{
		// Token: 0x06004702 RID: 18178 RVA: 0x001E6204 File Offset: 0x001E4404
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

		// Token: 0x06004703 RID: 18179 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F8A RID: 16266
		public static Dictionary<int, AllMapReset> DataDict = new Dictionary<int, AllMapReset>();

		// Token: 0x04003F8B RID: 16267
		public static List<AllMapReset> DataList = new List<AllMapReset>();

		// Token: 0x04003F8C RID: 16268
		public static Action OnInitFinishAction = new Action(AllMapReset.OnInitFinish);

		// Token: 0x04003F8D RID: 16269
		public int id;

		// Token: 0x04003F8E RID: 16270
		public int Type;

		// Token: 0x04003F8F RID: 16271
		public int resetTiem;

		// Token: 0x04003F90 RID: 16272
		public int CanSame;

		// Token: 0x04003F91 RID: 16273
		public int percent;

		// Token: 0x04003F92 RID: 16274
		public int max;

		// Token: 0x04003F93 RID: 16275
		public string name;

		// Token: 0x04003F94 RID: 16276
		public string Icon;

		// Token: 0x04003F95 RID: 16277
		public string Act;

		// Token: 0x04003F96 RID: 16278
		public List<int> qujian = new List<int>();
	}
}
