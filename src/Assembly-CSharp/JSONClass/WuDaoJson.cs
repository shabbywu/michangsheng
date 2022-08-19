using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000981 RID: 2433
	public class WuDaoJson : IJSONClass
	{
		// Token: 0x06004418 RID: 17432 RVA: 0x001D01B8 File Offset: 0x001CE3B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoJson.list)
			{
				try
				{
					WuDaoJson wuDaoJson = new WuDaoJson();
					wuDaoJson.id = jsonobject["id"].I;
					wuDaoJson.Cast = jsonobject["Cast"].I;
					wuDaoJson.Lv = jsonobject["Lv"].I;
					wuDaoJson.icon = jsonobject["icon"].Str;
					wuDaoJson.name = jsonobject["name"].Str;
					wuDaoJson.desc = jsonobject["desc"].Str;
					wuDaoJson.xiaoguo = jsonobject["xiaoguo"].Str;
					wuDaoJson.Type = jsonobject["Type"].ToList();
					wuDaoJson.seid = jsonobject["seid"].ToList();
					if (WuDaoJson.DataDict.ContainsKey(wuDaoJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoJson.id));
					}
					else
					{
						WuDaoJson.DataDict.Add(wuDaoJson.id, wuDaoJson);
						WuDaoJson.DataList.Add(wuDaoJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoJson.OnInitFinishAction != null)
			{
				WuDaoJson.OnInitFinishAction();
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004599 RID: 17817
		public static Dictionary<int, WuDaoJson> DataDict = new Dictionary<int, WuDaoJson>();

		// Token: 0x0400459A RID: 17818
		public static List<WuDaoJson> DataList = new List<WuDaoJson>();

		// Token: 0x0400459B RID: 17819
		public static Action OnInitFinishAction = new Action(WuDaoJson.OnInitFinish);

		// Token: 0x0400459C RID: 17820
		public int id;

		// Token: 0x0400459D RID: 17821
		public int Cast;

		// Token: 0x0400459E RID: 17822
		public int Lv;

		// Token: 0x0400459F RID: 17823
		public string icon;

		// Token: 0x040045A0 RID: 17824
		public string name;

		// Token: 0x040045A1 RID: 17825
		public string desc;

		// Token: 0x040045A2 RID: 17826
		public string xiaoguo;

		// Token: 0x040045A3 RID: 17827
		public List<int> Type = new List<int>();

		// Token: 0x040045A4 RID: 17828
		public List<int> seid = new List<int>();
	}
}
