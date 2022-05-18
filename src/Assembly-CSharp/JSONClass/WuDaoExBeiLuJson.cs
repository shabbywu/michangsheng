using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CFF RID: 3327
	public class WuDaoExBeiLuJson : IJSONClass
	{
		// Token: 0x06004F66 RID: 20326 RVA: 0x00214F44 File Offset: 0x00213144
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

		// Token: 0x06004F67 RID: 20327 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005075 RID: 20597
		public static Dictionary<int, WuDaoExBeiLuJson> DataDict = new Dictionary<int, WuDaoExBeiLuJson>();

		// Token: 0x04005076 RID: 20598
		public static List<WuDaoExBeiLuJson> DataList = new List<WuDaoExBeiLuJson>();

		// Token: 0x04005077 RID: 20599
		public static Action OnInitFinishAction = new Action(WuDaoExBeiLuJson.OnInitFinish);

		// Token: 0x04005078 RID: 20600
		public int id;

		// Token: 0x04005079 RID: 20601
		public int gongfa;

		// Token: 0x0400507A RID: 20602
		public int linwu;

		// Token: 0x0400507B RID: 20603
		public int tupo;

		// Token: 0x0400507C RID: 20604
		public int kanshu;

		// Token: 0x0400507D RID: 20605
		public int lingguang1;

		// Token: 0x0400507E RID: 20606
		public int lingguang2;

		// Token: 0x0400507F RID: 20607
		public int lingguang3;

		// Token: 0x04005080 RID: 20608
		public int lingguang4;

		// Token: 0x04005081 RID: 20609
		public int lingguang5;

		// Token: 0x04005082 RID: 20610
		public int lingguang6;
	}
}
