using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C05 RID: 3077
	public class LianQiHeCheng : IJSONClass
	{
		// Token: 0x06004B7D RID: 19325 RVA: 0x001FDF10 File Offset: 0x001FC110
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiHeCheng.list)
			{
				try
				{
					LianQiHeCheng lianQiHeCheng = new LianQiHeCheng();
					lianQiHeCheng.id = jsonobject["id"].I;
					lianQiHeCheng.ShuXingType = jsonobject["ShuXingType"].I;
					lianQiHeCheng.zhonglei = jsonobject["zhonglei"].I;
					lianQiHeCheng.cast = jsonobject["cast"].I;
					lianQiHeCheng.seid = jsonobject["seid"].I;
					lianQiHeCheng.HP = jsonobject["HP"].I;
					lianQiHeCheng.intvalue1 = jsonobject["intvalue1"].I;
					lianQiHeCheng.intvalue2 = jsonobject["intvalue2"].I;
					lianQiHeCheng.intvalue3 = jsonobject["intvalue3"].I;
					lianQiHeCheng.Itemseid = jsonobject["Itemseid"].I;
					lianQiHeCheng.itemfanbei = jsonobject["itemfanbei"].I;
					lianQiHeCheng.ZhuShi1 = jsonobject["ZhuShi1"].Str;
					lianQiHeCheng.ZhuShi2 = jsonobject["ZhuShi2"].Str;
					lianQiHeCheng.ZhuShi3 = jsonobject["ZhuShi3"].Str;
					lianQiHeCheng.xiangxidesc = jsonobject["xiangxidesc"].Str;
					lianQiHeCheng.descfirst = jsonobject["descfirst"].Str;
					lianQiHeCheng.desc = jsonobject["desc"].Str;
					lianQiHeCheng.Affix = jsonobject["Affix"].ToList();
					lianQiHeCheng.fanbei = jsonobject["fanbei"].ToList();
					lianQiHeCheng.listvalue1 = jsonobject["listvalue1"].ToList();
					lianQiHeCheng.listvalue2 = jsonobject["listvalue2"].ToList();
					lianQiHeCheng.listvalue3 = jsonobject["listvalue3"].ToList();
					lianQiHeCheng.Itemintvalue1 = jsonobject["Itemintvalue1"].ToList();
					lianQiHeCheng.Itemintvalue2 = jsonobject["Itemintvalue2"].ToList();
					if (LianQiHeCheng.DataDict.ContainsKey(lianQiHeCheng.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiHeCheng.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiHeCheng.id));
					}
					else
					{
						LianQiHeCheng.DataDict.Add(lianQiHeCheng.id, lianQiHeCheng);
						LianQiHeCheng.DataList.Add(lianQiHeCheng);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiHeCheng.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiHeCheng.OnInitFinishAction != null)
			{
				LianQiHeCheng.OnInitFinishAction();
			}
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004808 RID: 18440
		public static Dictionary<int, LianQiHeCheng> DataDict = new Dictionary<int, LianQiHeCheng>();

		// Token: 0x04004809 RID: 18441
		public static List<LianQiHeCheng> DataList = new List<LianQiHeCheng>();

		// Token: 0x0400480A RID: 18442
		public static Action OnInitFinishAction = new Action(LianQiHeCheng.OnInitFinish);

		// Token: 0x0400480B RID: 18443
		public int id;

		// Token: 0x0400480C RID: 18444
		public int ShuXingType;

		// Token: 0x0400480D RID: 18445
		public int zhonglei;

		// Token: 0x0400480E RID: 18446
		public int cast;

		// Token: 0x0400480F RID: 18447
		public int seid;

		// Token: 0x04004810 RID: 18448
		public int HP;

		// Token: 0x04004811 RID: 18449
		public int intvalue1;

		// Token: 0x04004812 RID: 18450
		public int intvalue2;

		// Token: 0x04004813 RID: 18451
		public int intvalue3;

		// Token: 0x04004814 RID: 18452
		public int Itemseid;

		// Token: 0x04004815 RID: 18453
		public int itemfanbei;

		// Token: 0x04004816 RID: 18454
		public string ZhuShi1;

		// Token: 0x04004817 RID: 18455
		public string ZhuShi2;

		// Token: 0x04004818 RID: 18456
		public string ZhuShi3;

		// Token: 0x04004819 RID: 18457
		public string xiangxidesc;

		// Token: 0x0400481A RID: 18458
		public string descfirst;

		// Token: 0x0400481B RID: 18459
		public string desc;

		// Token: 0x0400481C RID: 18460
		public List<int> Affix = new List<int>();

		// Token: 0x0400481D RID: 18461
		public List<int> fanbei = new List<int>();

		// Token: 0x0400481E RID: 18462
		public List<int> listvalue1 = new List<int>();

		// Token: 0x0400481F RID: 18463
		public List<int> listvalue2 = new List<int>();

		// Token: 0x04004820 RID: 18464
		public List<int> listvalue3 = new List<int>();

		// Token: 0x04004821 RID: 18465
		public List<int> Itemintvalue1 = new List<int>();

		// Token: 0x04004822 RID: 18466
		public List<int> Itemintvalue2 = new List<int>();
	}
}
