using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000980 RID: 2432
	public class WuDaoJinJieJson : IJSONClass
	{
		// Token: 0x06004414 RID: 17428 RVA: 0x001CFFE4 File Offset: 0x001CE1E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoJinJieJson.list)
			{
				try
				{
					WuDaoJinJieJson wuDaoJinJieJson = new WuDaoJinJieJson();
					wuDaoJinJieJson.id = jsonobject["id"].I;
					wuDaoJinJieJson.LV = jsonobject["LV"].I;
					wuDaoJinJieJson.Max = jsonobject["Max"].I;
					wuDaoJinJieJson.JiaCheng = jsonobject["JiaCheng"].I;
					wuDaoJinJieJson.LianDan = jsonobject["LianDan"].I;
					wuDaoJinJieJson.LianQi = jsonobject["LianQi"].I;
					wuDaoJinJieJson.Text = jsonobject["Text"].Str;
					if (WuDaoJinJieJson.DataDict.ContainsKey(wuDaoJinJieJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoJinJieJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoJinJieJson.id));
					}
					else
					{
						WuDaoJinJieJson.DataDict.Add(wuDaoJinJieJson.id, wuDaoJinJieJson);
						WuDaoJinJieJson.DataList.Add(wuDaoJinJieJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoJinJieJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoJinJieJson.OnInitFinishAction != null)
			{
				WuDaoJinJieJson.OnInitFinishAction();
			}
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400458F RID: 17807
		public static Dictionary<int, WuDaoJinJieJson> DataDict = new Dictionary<int, WuDaoJinJieJson>();

		// Token: 0x04004590 RID: 17808
		public static List<WuDaoJinJieJson> DataList = new List<WuDaoJinJieJson>();

		// Token: 0x04004591 RID: 17809
		public static Action OnInitFinishAction = new Action(WuDaoJinJieJson.OnInitFinish);

		// Token: 0x04004592 RID: 17810
		public int id;

		// Token: 0x04004593 RID: 17811
		public int LV;

		// Token: 0x04004594 RID: 17812
		public int Max;

		// Token: 0x04004595 RID: 17813
		public int JiaCheng;

		// Token: 0x04004596 RID: 17814
		public int LianDan;

		// Token: 0x04004597 RID: 17815
		public int LianQi;

		// Token: 0x04004598 RID: 17816
		public string Text;
	}
}
