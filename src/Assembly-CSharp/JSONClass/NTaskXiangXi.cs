using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C49 RID: 3145
	public class NTaskXiangXi : IJSONClass
	{
		// Token: 0x06004C8D RID: 19597 RVA: 0x00205AF8 File Offset: 0x00203CF8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NTaskXiangXi.list)
			{
				try
				{
					NTaskXiangXi ntaskXiangXi = new NTaskXiangXi();
					ntaskXiangXi.id = jsonobject["id"].I;
					ntaskXiangXi.JiaoFuType = jsonobject["JiaoFuType"].I;
					ntaskXiangXi.Type = jsonobject["Type"].I;
					ntaskXiangXi.percent = jsonobject["percent"].I;
					ntaskXiangXi.shiXian = jsonobject["shiXian"].I;
					ntaskXiangXi.ShiLIAdd = jsonobject["ShiLIAdd"].I;
					ntaskXiangXi.GeRenAdd = jsonobject["GeRenAdd"].I;
					ntaskXiangXi.ShiLIReduce = jsonobject["ShiLIReduce"].I;
					ntaskXiangXi.GeRenReduce = jsonobject["GeRenReduce"].I;
					ntaskXiangXi.shouYiLu = jsonobject["shouYiLu"].I;
					ntaskXiangXi.name = jsonobject["name"].Str;
					ntaskXiangXi.SayMiaoShu = jsonobject["SayMiaoShu"].Str;
					ntaskXiangXi.zongmiaoshu = jsonobject["zongmiaoshu"].Str;
					ntaskXiangXi.TaskZiXiang = jsonobject["TaskZiXiang"].Str;
					ntaskXiangXi.Level = jsonobject["Level"].ToList();
					ntaskXiangXi.menpaihaogan = jsonobject["menpaihaogan"].ToList();
					if (NTaskXiangXi.DataDict.ContainsKey(ntaskXiangXi.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NTaskXiangXi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", ntaskXiangXi.id));
					}
					else
					{
						NTaskXiangXi.DataDict.Add(ntaskXiangXi.id, ntaskXiangXi);
						NTaskXiangXi.DataList.Add(ntaskXiangXi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NTaskXiangXi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NTaskXiangXi.OnInitFinishAction != null)
			{
				NTaskXiangXi.OnInitFinishAction();
			}
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B1C RID: 19228
		public static Dictionary<int, NTaskXiangXi> DataDict = new Dictionary<int, NTaskXiangXi>();

		// Token: 0x04004B1D RID: 19229
		public static List<NTaskXiangXi> DataList = new List<NTaskXiangXi>();

		// Token: 0x04004B1E RID: 19230
		public static Action OnInitFinishAction = new Action(NTaskXiangXi.OnInitFinish);

		// Token: 0x04004B1F RID: 19231
		public int id;

		// Token: 0x04004B20 RID: 19232
		public int JiaoFuType;

		// Token: 0x04004B21 RID: 19233
		public int Type;

		// Token: 0x04004B22 RID: 19234
		public int percent;

		// Token: 0x04004B23 RID: 19235
		public int shiXian;

		// Token: 0x04004B24 RID: 19236
		public int ShiLIAdd;

		// Token: 0x04004B25 RID: 19237
		public int GeRenAdd;

		// Token: 0x04004B26 RID: 19238
		public int ShiLIReduce;

		// Token: 0x04004B27 RID: 19239
		public int GeRenReduce;

		// Token: 0x04004B28 RID: 19240
		public int shouYiLu;

		// Token: 0x04004B29 RID: 19241
		public string name;

		// Token: 0x04004B2A RID: 19242
		public string SayMiaoShu;

		// Token: 0x04004B2B RID: 19243
		public string zongmiaoshu;

		// Token: 0x04004B2C RID: 19244
		public string TaskZiXiang;

		// Token: 0x04004B2D RID: 19245
		public List<int> Level = new List<int>();

		// Token: 0x04004B2E RID: 19246
		public List<int> menpaihaogan = new List<int>();
	}
}
