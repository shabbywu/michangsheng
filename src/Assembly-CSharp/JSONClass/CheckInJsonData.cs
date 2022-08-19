using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000805 RID: 2053
	public class CheckInJsonData : IJSONClass
	{
		// Token: 0x06003E26 RID: 15910 RVA: 0x001A90D8 File Offset: 0x001A72D8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CheckInJsonData.list)
			{
				try
				{
					CheckInJsonData checkInJsonData = new CheckInJsonData();
					checkInJsonData.id = jsonobject["id"].I;
					checkInJsonData.checkinType = jsonobject["checkinType"].I;
					checkInJsonData.checkinId = jsonobject["checkinId"].I;
					checkInJsonData.checkincount = jsonobject["checkincount"].I;
					checkInJsonData.day = jsonobject["day"].I;
					if (CheckInJsonData.DataDict.ContainsKey(checkInJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CheckInJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", checkInJsonData.id));
					}
					else
					{
						CheckInJsonData.DataDict.Add(checkInJsonData.id, checkInJsonData);
						CheckInJsonData.DataList.Add(checkInJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CheckInJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CheckInJsonData.OnInitFinishAction != null)
			{
				CheckInJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400392D RID: 14637
		public static Dictionary<int, CheckInJsonData> DataDict = new Dictionary<int, CheckInJsonData>();

		// Token: 0x0400392E RID: 14638
		public static List<CheckInJsonData> DataList = new List<CheckInJsonData>();

		// Token: 0x0400392F RID: 14639
		public static Action OnInitFinishAction = new Action(CheckInJsonData.OnInitFinish);

		// Token: 0x04003930 RID: 14640
		public int id;

		// Token: 0x04003931 RID: 14641
		public int checkinType;

		// Token: 0x04003932 RID: 14642
		public int checkinId;

		// Token: 0x04003933 RID: 14643
		public int checkincount;

		// Token: 0x04003934 RID: 14644
		public int day;
	}
}
