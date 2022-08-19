using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008BB RID: 2235
	public class NTaskXiangXi : IJSONClass
	{
		// Token: 0x060040FF RID: 16639 RVA: 0x001BCF94 File Offset: 0x001BB194
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

		// Token: 0x06004100 RID: 16640 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FC8 RID: 16328
		public static Dictionary<int, NTaskXiangXi> DataDict = new Dictionary<int, NTaskXiangXi>();

		// Token: 0x04003FC9 RID: 16329
		public static List<NTaskXiangXi> DataList = new List<NTaskXiangXi>();

		// Token: 0x04003FCA RID: 16330
		public static Action OnInitFinishAction = new Action(NTaskXiangXi.OnInitFinish);

		// Token: 0x04003FCB RID: 16331
		public int id;

		// Token: 0x04003FCC RID: 16332
		public int JiaoFuType;

		// Token: 0x04003FCD RID: 16333
		public int Type;

		// Token: 0x04003FCE RID: 16334
		public int percent;

		// Token: 0x04003FCF RID: 16335
		public int shiXian;

		// Token: 0x04003FD0 RID: 16336
		public int ShiLIAdd;

		// Token: 0x04003FD1 RID: 16337
		public int GeRenAdd;

		// Token: 0x04003FD2 RID: 16338
		public int ShiLIReduce;

		// Token: 0x04003FD3 RID: 16339
		public int GeRenReduce;

		// Token: 0x04003FD4 RID: 16340
		public int shouYiLu;

		// Token: 0x04003FD5 RID: 16341
		public string name;

		// Token: 0x04003FD6 RID: 16342
		public string SayMiaoShu;

		// Token: 0x04003FD7 RID: 16343
		public string zongmiaoshu;

		// Token: 0x04003FD8 RID: 16344
		public string TaskZiXiang;

		// Token: 0x04003FD9 RID: 16345
		public List<int> Level = new List<int>();

		// Token: 0x04003FDA RID: 16346
		public List<int> menpaihaogan = new List<int>();
	}
}
