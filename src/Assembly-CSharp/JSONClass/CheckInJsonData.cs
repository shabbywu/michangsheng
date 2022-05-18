using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B9C RID: 2972
	public class CheckInJsonData : IJSONClass
	{
		// Token: 0x060049D8 RID: 18904 RVA: 0x001F4B88 File Offset: 0x001F2D88
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

		// Token: 0x060049D9 RID: 18905 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044BD RID: 17597
		public static Dictionary<int, CheckInJsonData> DataDict = new Dictionary<int, CheckInJsonData>();

		// Token: 0x040044BE RID: 17598
		public static List<CheckInJsonData> DataList = new List<CheckInJsonData>();

		// Token: 0x040044BF RID: 17599
		public static Action OnInitFinishAction = new Action(CheckInJsonData.OnInitFinish);

		// Token: 0x040044C0 RID: 17600
		public int id;

		// Token: 0x040044C1 RID: 17601
		public int checkinType;

		// Token: 0x040044C2 RID: 17602
		public int checkinId;

		// Token: 0x040044C3 RID: 17603
		public int checkincount;

		// Token: 0x040044C4 RID: 17604
		public int day;
	}
}
