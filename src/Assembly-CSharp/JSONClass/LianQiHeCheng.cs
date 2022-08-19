using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000877 RID: 2167
	public class LianQiHeCheng : IJSONClass
	{
		// Token: 0x06003FEF RID: 16367 RVA: 0x001B45E8 File Offset: 0x001B27E8
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

		// Token: 0x06003FF0 RID: 16368 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003CAF RID: 15535
		public static Dictionary<int, LianQiHeCheng> DataDict = new Dictionary<int, LianQiHeCheng>();

		// Token: 0x04003CB0 RID: 15536
		public static List<LianQiHeCheng> DataList = new List<LianQiHeCheng>();

		// Token: 0x04003CB1 RID: 15537
		public static Action OnInitFinishAction = new Action(LianQiHeCheng.OnInitFinish);

		// Token: 0x04003CB2 RID: 15538
		public int id;

		// Token: 0x04003CB3 RID: 15539
		public int ShuXingType;

		// Token: 0x04003CB4 RID: 15540
		public int zhonglei;

		// Token: 0x04003CB5 RID: 15541
		public int cast;

		// Token: 0x04003CB6 RID: 15542
		public int seid;

		// Token: 0x04003CB7 RID: 15543
		public int HP;

		// Token: 0x04003CB8 RID: 15544
		public int intvalue1;

		// Token: 0x04003CB9 RID: 15545
		public int intvalue2;

		// Token: 0x04003CBA RID: 15546
		public int intvalue3;

		// Token: 0x04003CBB RID: 15547
		public int Itemseid;

		// Token: 0x04003CBC RID: 15548
		public int itemfanbei;

		// Token: 0x04003CBD RID: 15549
		public string ZhuShi1;

		// Token: 0x04003CBE RID: 15550
		public string ZhuShi2;

		// Token: 0x04003CBF RID: 15551
		public string ZhuShi3;

		// Token: 0x04003CC0 RID: 15552
		public string xiangxidesc;

		// Token: 0x04003CC1 RID: 15553
		public string descfirst;

		// Token: 0x04003CC2 RID: 15554
		public string desc;

		// Token: 0x04003CC3 RID: 15555
		public List<int> Affix = new List<int>();

		// Token: 0x04003CC4 RID: 15556
		public List<int> fanbei = new List<int>();

		// Token: 0x04003CC5 RID: 15557
		public List<int> listvalue1 = new List<int>();

		// Token: 0x04003CC6 RID: 15558
		public List<int> listvalue2 = new List<int>();

		// Token: 0x04003CC7 RID: 15559
		public List<int> listvalue3 = new List<int>();

		// Token: 0x04003CC8 RID: 15560
		public List<int> Itemintvalue1 = new List<int>();

		// Token: 0x04003CC9 RID: 15561
		public List<int> Itemintvalue2 = new List<int>();
	}
}
