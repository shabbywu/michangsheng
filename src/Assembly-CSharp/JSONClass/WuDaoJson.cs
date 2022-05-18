using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D01 RID: 3329
	public class WuDaoJson : IJSONClass
	{
		// Token: 0x06004F6E RID: 20334 RVA: 0x002152F4 File Offset: 0x002134F4
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

		// Token: 0x06004F6F RID: 20335 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400508D RID: 20621
		public static Dictionary<int, WuDaoJson> DataDict = new Dictionary<int, WuDaoJson>();

		// Token: 0x0400508E RID: 20622
		public static List<WuDaoJson> DataList = new List<WuDaoJson>();

		// Token: 0x0400508F RID: 20623
		public static Action OnInitFinishAction = new Action(WuDaoJson.OnInitFinish);

		// Token: 0x04005090 RID: 20624
		public int id;

		// Token: 0x04005091 RID: 20625
		public int Cast;

		// Token: 0x04005092 RID: 20626
		public int Lv;

		// Token: 0x04005093 RID: 20627
		public string icon;

		// Token: 0x04005094 RID: 20628
		public string name;

		// Token: 0x04005095 RID: 20629
		public string desc;

		// Token: 0x04005096 RID: 20630
		public string xiaoguo;

		// Token: 0x04005097 RID: 20631
		public List<int> Type = new List<int>();

		// Token: 0x04005098 RID: 20632
		public List<int> seid = new List<int>();
	}
}
