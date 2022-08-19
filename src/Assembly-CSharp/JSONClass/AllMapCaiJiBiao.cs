using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000749 RID: 1865
	public class AllMapCaiJiBiao : IJSONClass
	{
		// Token: 0x06003B38 RID: 15160 RVA: 0x00197750 File Offset: 0x00195950
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapCaiJiBiao.list)
			{
				try
				{
					AllMapCaiJiBiao allMapCaiJiBiao = new AllMapCaiJiBiao();
					allMapCaiJiBiao.ID = jsonobject["ID"].I;
					allMapCaiJiBiao.Item = jsonobject["Item"].I;
					allMapCaiJiBiao.percent = jsonobject["percent"].I;
					allMapCaiJiBiao.Monstar = jsonobject["Monstar"].I;
					allMapCaiJiBiao.MaiFuTime = jsonobject["MaiFuTime"].I;
					allMapCaiJiBiao.MaiFuMonstar = jsonobject["MaiFuMonstar"].I;
					allMapCaiJiBiao.Num = jsonobject["Num"].ToList();
					allMapCaiJiBiao.Level = jsonobject["Level"].ToList();
					if (AllMapCaiJiBiao.DataDict.ContainsKey(allMapCaiJiBiao.ID))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapCaiJiBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapCaiJiBiao.ID));
					}
					else
					{
						AllMapCaiJiBiao.DataDict.Add(allMapCaiJiBiao.ID, allMapCaiJiBiao);
						AllMapCaiJiBiao.DataList.Add(allMapCaiJiBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapCaiJiBiao.OnInitFinishAction != null)
			{
				AllMapCaiJiBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040033BF RID: 13247
		public static Dictionary<int, AllMapCaiJiBiao> DataDict = new Dictionary<int, AllMapCaiJiBiao>();

		// Token: 0x040033C0 RID: 13248
		public static List<AllMapCaiJiBiao> DataList = new List<AllMapCaiJiBiao>();

		// Token: 0x040033C1 RID: 13249
		public static Action OnInitFinishAction = new Action(AllMapCaiJiBiao.OnInitFinish);

		// Token: 0x040033C2 RID: 13250
		public int ID;

		// Token: 0x040033C3 RID: 13251
		public int Item;

		// Token: 0x040033C4 RID: 13252
		public int percent;

		// Token: 0x040033C5 RID: 13253
		public int Monstar;

		// Token: 0x040033C6 RID: 13254
		public int MaiFuTime;

		// Token: 0x040033C7 RID: 13255
		public int MaiFuMonstar;

		// Token: 0x040033C8 RID: 13256
		public List<int> Num = new List<int>();

		// Token: 0x040033C9 RID: 13257
		public List<int> Level = new List<int>();
	}
}
