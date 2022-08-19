using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200087A RID: 2170
	public class LianQiLingWenBiao : IJSONClass
	{
		// Token: 0x06003FFB RID: 16379 RVA: 0x001B4C80 File Offset: 0x001B2E80
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiLingWenBiao.list)
			{
				try
				{
					LianQiLingWenBiao lianQiLingWenBiao = new LianQiLingWenBiao();
					lianQiLingWenBiao.id = jsonobject["id"].I;
					lianQiLingWenBiao.type = jsonobject["type"].I;
					lianQiLingWenBiao.value1 = jsonobject["value1"].I;
					lianQiLingWenBiao.value2 = jsonobject["value2"].I;
					lianQiLingWenBiao.value3 = jsonobject["value3"].I;
					lianQiLingWenBiao.value4 = jsonobject["value4"].I;
					lianQiLingWenBiao.seid = jsonobject["seid"].I;
					lianQiLingWenBiao.Itemseid = jsonobject["Itemseid"].I;
					lianQiLingWenBiao.name = jsonobject["name"].Str;
					lianQiLingWenBiao.desc = jsonobject["desc"].Str;
					lianQiLingWenBiao.xiangxidesc = jsonobject["xiangxidesc"].Str;
					lianQiLingWenBiao.Affix = jsonobject["Affix"].ToList();
					lianQiLingWenBiao.listvalue1 = jsonobject["listvalue1"].ToList();
					lianQiLingWenBiao.listvalue2 = jsonobject["listvalue2"].ToList();
					lianQiLingWenBiao.listvalue3 = jsonobject["listvalue3"].ToList();
					lianQiLingWenBiao.Itemintvalue1 = jsonobject["Itemintvalue1"].ToList();
					lianQiLingWenBiao.Itemintvalue2 = jsonobject["Itemintvalue2"].ToList();
					if (LianQiLingWenBiao.DataDict.ContainsKey(lianQiLingWenBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiLingWenBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiLingWenBiao.id));
					}
					else
					{
						LianQiLingWenBiao.DataDict.Add(lianQiLingWenBiao.id, lianQiLingWenBiao);
						LianQiLingWenBiao.DataList.Add(lianQiLingWenBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiLingWenBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiLingWenBiao.OnInitFinishAction != null)
			{
				LianQiLingWenBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003CD7 RID: 15575
		public static Dictionary<int, LianQiLingWenBiao> DataDict = new Dictionary<int, LianQiLingWenBiao>();

		// Token: 0x04003CD8 RID: 15576
		public static List<LianQiLingWenBiao> DataList = new List<LianQiLingWenBiao>();

		// Token: 0x04003CD9 RID: 15577
		public static Action OnInitFinishAction = new Action(LianQiLingWenBiao.OnInitFinish);

		// Token: 0x04003CDA RID: 15578
		public int id;

		// Token: 0x04003CDB RID: 15579
		public int type;

		// Token: 0x04003CDC RID: 15580
		public int value1;

		// Token: 0x04003CDD RID: 15581
		public int value2;

		// Token: 0x04003CDE RID: 15582
		public int value3;

		// Token: 0x04003CDF RID: 15583
		public int value4;

		// Token: 0x04003CE0 RID: 15584
		public int seid;

		// Token: 0x04003CE1 RID: 15585
		public int Itemseid;

		// Token: 0x04003CE2 RID: 15586
		public string name;

		// Token: 0x04003CE3 RID: 15587
		public string desc;

		// Token: 0x04003CE4 RID: 15588
		public string xiangxidesc;

		// Token: 0x04003CE5 RID: 15589
		public List<int> Affix = new List<int>();

		// Token: 0x04003CE6 RID: 15590
		public List<int> listvalue1 = new List<int>();

		// Token: 0x04003CE7 RID: 15591
		public List<int> listvalue2 = new List<int>();

		// Token: 0x04003CE8 RID: 15592
		public List<int> listvalue3 = new List<int>();

		// Token: 0x04003CE9 RID: 15593
		public List<int> Itemintvalue1 = new List<int>();

		// Token: 0x04003CEA RID: 15594
		public List<int> Itemintvalue2 = new List<int>();
	}
}
