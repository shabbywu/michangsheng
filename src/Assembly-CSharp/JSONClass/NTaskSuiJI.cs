using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C48 RID: 3144
	public class NTaskSuiJI : IJSONClass
	{
		// Token: 0x06004C89 RID: 19593 RVA: 0x00205920 File Offset: 0x00203B20
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

		// Token: 0x06004C8A RID: 19594 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B10 RID: 19216
		public static Dictionary<int, NTaskSuiJI> DataDict = new Dictionary<int, NTaskSuiJI>();

		// Token: 0x04004B11 RID: 19217
		public static List<NTaskSuiJI> DataList = new List<NTaskSuiJI>();

		// Token: 0x04004B12 RID: 19218
		public static Action OnInitFinishAction = new Action(NTaskSuiJI.OnInitFinish);

		// Token: 0x04004B13 RID: 19219
		public int id;

		// Token: 0x04004B14 RID: 19220
		public int Value;

		// Token: 0x04004B15 RID: 19221
		public int jiaZhi;

		// Token: 0x04004B16 RID: 19222
		public int huobi;

		// Token: 0x04004B17 RID: 19223
		public string Str;

		// Token: 0x04004B18 RID: 19224
		public string StrValue;

		// Token: 0x04004B19 RID: 19225
		public string name;

		// Token: 0x04004B1A RID: 19226
		public List<int> type = new List<int>();

		// Token: 0x04004B1B RID: 19227
		public List<int> shuxing = new List<int>();
	}
}
