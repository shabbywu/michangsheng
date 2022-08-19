using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008BA RID: 2234
	public class NTaskSuiJI : IJSONClass
	{
		// Token: 0x060040FB RID: 16635 RVA: 0x001BCD74 File Offset: 0x001BAF74
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NTaskSuiJI.list)
			{
				try
				{
					NTaskSuiJI ntaskSuiJI = new NTaskSuiJI();
					ntaskSuiJI.id = jsonobject["id"].I;
					ntaskSuiJI.Value = jsonobject["Value"].I;
					ntaskSuiJI.jiaZhi = jsonobject["jiaZhi"].I;
					ntaskSuiJI.huobi = jsonobject["huobi"].I;
					ntaskSuiJI.Str = jsonobject["Str"].Str;
					ntaskSuiJI.StrValue = jsonobject["StrValue"].Str;
					ntaskSuiJI.name = jsonobject["name"].Str;
					ntaskSuiJI.type = jsonobject["type"].ToList();
					ntaskSuiJI.shuxing = jsonobject["shuxing"].ToList();
					if (NTaskSuiJI.DataDict.ContainsKey(ntaskSuiJI.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NTaskSuiJI.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", ntaskSuiJI.id));
					}
					else
					{
						NTaskSuiJI.DataDict.Add(ntaskSuiJI.id, ntaskSuiJI);
						NTaskSuiJI.DataList.Add(ntaskSuiJI);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NTaskSuiJI.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NTaskSuiJI.OnInitFinishAction != null)
			{
				NTaskSuiJI.OnInitFinishAction();
			}
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FBC RID: 16316
		public static Dictionary<int, NTaskSuiJI> DataDict = new Dictionary<int, NTaskSuiJI>();

		// Token: 0x04003FBD RID: 16317
		public static List<NTaskSuiJI> DataList = new List<NTaskSuiJI>();

		// Token: 0x04003FBE RID: 16318
		public static Action OnInitFinishAction = new Action(NTaskSuiJI.OnInitFinish);

		// Token: 0x04003FBF RID: 16319
		public int id;

		// Token: 0x04003FC0 RID: 16320
		public int Value;

		// Token: 0x04003FC1 RID: 16321
		public int jiaZhi;

		// Token: 0x04003FC2 RID: 16322
		public int huobi;

		// Token: 0x04003FC3 RID: 16323
		public string Str;

		// Token: 0x04003FC4 RID: 16324
		public string StrValue;

		// Token: 0x04003FC5 RID: 16325
		public string name;

		// Token: 0x04003FC6 RID: 16326
		public List<int> type = new List<int>();

		// Token: 0x04003FC7 RID: 16327
		public List<int> shuxing = new List<int>();
	}
}
