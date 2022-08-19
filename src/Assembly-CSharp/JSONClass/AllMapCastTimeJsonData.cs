using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200074B RID: 1867
	public class AllMapCastTimeJsonData : IJSONClass
	{
		// Token: 0x06003B40 RID: 15168 RVA: 0x00197B40 File Offset: 0x00195D40
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapCastTimeJsonData.list)
			{
				try
				{
					AllMapCastTimeJsonData allMapCastTimeJsonData = new AllMapCastTimeJsonData();
					allMapCastTimeJsonData.id = jsonobject["id"].I;
					allMapCastTimeJsonData.dunSu = jsonobject["dunSu"].I;
					allMapCastTimeJsonData.XiaoHao = jsonobject["XiaoHao"].I;
					if (AllMapCastTimeJsonData.DataDict.ContainsKey(allMapCastTimeJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapCastTimeJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapCastTimeJsonData.id));
					}
					else
					{
						AllMapCastTimeJsonData.DataDict.Add(allMapCastTimeJsonData.id, allMapCastTimeJsonData);
						AllMapCastTimeJsonData.DataList.Add(allMapCastTimeJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapCastTimeJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapCastTimeJsonData.OnInitFinishAction != null)
			{
				AllMapCastTimeJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040033D5 RID: 13269
		public static Dictionary<int, AllMapCastTimeJsonData> DataDict = new Dictionary<int, AllMapCastTimeJsonData>();

		// Token: 0x040033D6 RID: 13270
		public static List<AllMapCastTimeJsonData> DataList = new List<AllMapCastTimeJsonData>();

		// Token: 0x040033D7 RID: 13271
		public static Action OnInitFinishAction = new Action(AllMapCastTimeJsonData.OnInitFinish);

		// Token: 0x040033D8 RID: 13272
		public int id;

		// Token: 0x040033D9 RID: 13273
		public int dunSu;

		// Token: 0x040033DA RID: 13274
		public int XiaoHao;
	}
}
