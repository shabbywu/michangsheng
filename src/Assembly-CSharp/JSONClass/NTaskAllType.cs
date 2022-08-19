using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B9 RID: 2233
	public class NTaskAllType : IJSONClass
	{
		// Token: 0x060040F7 RID: 16631 RVA: 0x001BCB14 File Offset: 0x001BAD14
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NTaskAllType.list)
			{
				try
				{
					NTaskAllType ntaskAllType = new NTaskAllType();
					ntaskAllType.Id = jsonobject["Id"].I;
					ntaskAllType.CD = jsonobject["CD"].I;
					ntaskAllType.shili = jsonobject["shili"].I;
					ntaskAllType.GeRen = jsonobject["GeRen"].I;
					ntaskAllType.menpaihuobi = jsonobject["menpaihuobi"].I;
					ntaskAllType.Type = jsonobject["Type"].I;
					ntaskAllType.name = jsonobject["name"].Str;
					ntaskAllType.ZongMiaoShu = jsonobject["ZongMiaoShu"].Str;
					ntaskAllType.jiaofurenwu = jsonobject["jiaofurenwu"].Str;
					ntaskAllType.jiaofudidian = jsonobject["jiaofudidian"].Str;
					ntaskAllType.XiangXiID = jsonobject["XiangXiID"].ToList();
					ntaskAllType.seid = jsonobject["seid"].ToList();
					if (NTaskAllType.DataDict.ContainsKey(ntaskAllType.Id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NTaskAllType.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", ntaskAllType.Id));
					}
					else
					{
						NTaskAllType.DataDict.Add(ntaskAllType.Id, ntaskAllType);
						NTaskAllType.DataList.Add(ntaskAllType);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NTaskAllType.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NTaskAllType.OnInitFinishAction != null)
			{
				NTaskAllType.OnInitFinishAction();
			}
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FAD RID: 16301
		public static Dictionary<int, NTaskAllType> DataDict = new Dictionary<int, NTaskAllType>();

		// Token: 0x04003FAE RID: 16302
		public static List<NTaskAllType> DataList = new List<NTaskAllType>();

		// Token: 0x04003FAF RID: 16303
		public static Action OnInitFinishAction = new Action(NTaskAllType.OnInitFinish);

		// Token: 0x04003FB0 RID: 16304
		public int Id;

		// Token: 0x04003FB1 RID: 16305
		public int CD;

		// Token: 0x04003FB2 RID: 16306
		public int shili;

		// Token: 0x04003FB3 RID: 16307
		public int GeRen;

		// Token: 0x04003FB4 RID: 16308
		public int menpaihuobi;

		// Token: 0x04003FB5 RID: 16309
		public int Type;

		// Token: 0x04003FB6 RID: 16310
		public string name;

		// Token: 0x04003FB7 RID: 16311
		public string ZongMiaoShu;

		// Token: 0x04003FB8 RID: 16312
		public string jiaofurenwu;

		// Token: 0x04003FB9 RID: 16313
		public string jiaofudidian;

		// Token: 0x04003FBA RID: 16314
		public List<int> XiangXiID = new List<int>();

		// Token: 0x04003FBB RID: 16315
		public List<int> seid = new List<int>();
	}
}
