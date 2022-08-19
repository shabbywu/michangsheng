using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200098C RID: 2444
	public class WuJiangBangDing : IJSONClass
	{
		// Token: 0x06004444 RID: 17476 RVA: 0x001D1190 File Offset: 0x001CF390
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

		// Token: 0x06004445 RID: 17477 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045E2 RID: 17890
		public static Dictionary<int, WuJiangBangDing> DataDict = new Dictionary<int, WuJiangBangDing>();

		// Token: 0x040045E3 RID: 17891
		public static List<WuJiangBangDing> DataList = new List<WuJiangBangDing>();

		// Token: 0x040045E4 RID: 17892
		public static Action OnInitFinishAction = new Action(WuJiangBangDing.OnInitFinish);

		// Token: 0x040045E5 RID: 17893
		public int id;

		// Token: 0x040045E6 RID: 17894
		public int Image;

		// Token: 0x040045E7 RID: 17895
		public int PaiMaiHang;

		// Token: 0x040045E8 RID: 17896
		public string TimeStart;

		// Token: 0x040045E9 RID: 17897
		public string TimeEnd;

		// Token: 0x040045EA RID: 17898
		public string Name;

		// Token: 0x040045EB RID: 17899
		public string Title;

		// Token: 0x040045EC RID: 17900
		public List<int> avatar = new List<int>();
	}
}
