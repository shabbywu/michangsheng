using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200097F RID: 2431
	public class WuDaoExBeiLuJson : IJSONClass
	{
		// Token: 0x06004410 RID: 17424 RVA: 0x001CFDB8 File Offset: 0x001CDFB8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoExBeiLuJson.list)
			{
				try
				{
					WuDaoExBeiLuJson wuDaoExBeiLuJson = new WuDaoExBeiLuJson();
					wuDaoExBeiLuJson.id = jsonobject["id"].I;
					wuDaoExBeiLuJson.gongfa = jsonobject["gongfa"].I;
					wuDaoExBeiLuJson.linwu = jsonobject["linwu"].I;
					wuDaoExBeiLuJson.tupo = jsonobject["tupo"].I;
					wuDaoExBeiLuJson.kanshu = jsonobject["kanshu"].I;
					wuDaoExBeiLuJson.lingguang1 = jsonobject["lingguang1"].I;
					wuDaoExBeiLuJson.lingguang2 = jsonobject["lingguang2"].I;
					wuDaoExBeiLuJson.lingguang3 = jsonobject["lingguang3"].I;
					wuDaoExBeiLuJson.lingguang4 = jsonobject["lingguang4"].I;
					wuDaoExBeiLuJson.lingguang5 = jsonobject["lingguang5"].I;
					wuDaoExBeiLuJson.lingguang6 = jsonobject["lingguang6"].I;
					if (WuDaoExBeiLuJson.DataDict.ContainsKey(wuDaoExBeiLuJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoExBeiLuJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoExBeiLuJson.id));
					}
					else
					{
						WuDaoExBeiLuJson.DataDict.Add(wuDaoExBeiLuJson.id, wuDaoExBeiLuJson);
						WuDaoExBeiLuJson.DataList.Add(wuDaoExBeiLuJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoExBeiLuJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoExBeiLuJson.OnInitFinishAction != null)
			{
				WuDaoExBeiLuJson.OnInitFinishAction();
			}
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004581 RID: 17793
		public static Dictionary<int, WuDaoExBeiLuJson> DataDict = new Dictionary<int, WuDaoExBeiLuJson>();

		// Token: 0x04004582 RID: 17794
		public static List<WuDaoExBeiLuJson> DataList = new List<WuDaoExBeiLuJson>();

		// Token: 0x04004583 RID: 17795
		public static Action OnInitFinishAction = new Action(WuDaoExBeiLuJson.OnInitFinish);

		// Token: 0x04004584 RID: 17796
		public int id;

		// Token: 0x04004585 RID: 17797
		public int gongfa;

		// Token: 0x04004586 RID: 17798
		public int linwu;

		// Token: 0x04004587 RID: 17799
		public int tupo;

		// Token: 0x04004588 RID: 17800
		public int kanshu;

		// Token: 0x04004589 RID: 17801
		public int lingguang1;

		// Token: 0x0400458A RID: 17802
		public int lingguang2;

		// Token: 0x0400458B RID: 17803
		public int lingguang3;

		// Token: 0x0400458C RID: 17804
		public int lingguang4;

		// Token: 0x0400458D RID: 17805
		public int lingguang5;

		// Token: 0x0400458E RID: 17806
		public int lingguang6;
	}
}
