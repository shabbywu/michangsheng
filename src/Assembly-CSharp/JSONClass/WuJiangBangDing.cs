using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D0C RID: 3340
	public class WuJiangBangDing : IJSONClass
	{
		// Token: 0x06004F9A RID: 20378 RVA: 0x00216098 File Offset: 0x00214298
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuJiangBangDing.list)
			{
				try
				{
					WuJiangBangDing wuJiangBangDing = new WuJiangBangDing();
					wuJiangBangDing.id = jsonobject["id"].I;
					wuJiangBangDing.Image = jsonobject["Image"].I;
					wuJiangBangDing.PaiMaiHang = jsonobject["PaiMaiHang"].I;
					wuJiangBangDing.TimeStart = jsonobject["TimeStart"].Str;
					wuJiangBangDing.TimeEnd = jsonobject["TimeEnd"].Str;
					wuJiangBangDing.Name = jsonobject["Name"].Str;
					wuJiangBangDing.Title = jsonobject["Title"].Str;
					wuJiangBangDing.avatar = jsonobject["avatar"].ToList();
					if (WuJiangBangDing.DataDict.ContainsKey(wuJiangBangDing.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuJiangBangDing.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuJiangBangDing.id));
					}
					else
					{
						WuJiangBangDing.DataDict.Add(wuJiangBangDing.id, wuJiangBangDing);
						WuJiangBangDing.DataList.Add(wuJiangBangDing);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuJiangBangDing.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuJiangBangDing.OnInitFinishAction != null)
			{
				WuJiangBangDing.OnInitFinishAction();
			}
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050D6 RID: 20694
		public static Dictionary<int, WuJiangBangDing> DataDict = new Dictionary<int, WuJiangBangDing>();

		// Token: 0x040050D7 RID: 20695
		public static List<WuJiangBangDing> DataList = new List<WuJiangBangDing>();

		// Token: 0x040050D8 RID: 20696
		public static Action OnInitFinishAction = new Action(WuJiangBangDing.OnInitFinish);

		// Token: 0x040050D9 RID: 20697
		public int id;

		// Token: 0x040050DA RID: 20698
		public int Image;

		// Token: 0x040050DB RID: 20699
		public int PaiMaiHang;

		// Token: 0x040050DC RID: 20700
		public string TimeStart;

		// Token: 0x040050DD RID: 20701
		public string TimeEnd;

		// Token: 0x040050DE RID: 20702
		public string Name;

		// Token: 0x040050DF RID: 20703
		public string Title;

		// Token: 0x040050E0 RID: 20704
		public List<int> avatar = new List<int>();
	}
}
