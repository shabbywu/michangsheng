using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C08 RID: 3080
	public class LianQiLingWenBiao : IJSONClass
	{
		// Token: 0x06004B89 RID: 19337 RVA: 0x001FE51C File Offset: 0x001FC71C
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

		// Token: 0x06004B8A RID: 19338 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004830 RID: 18480
		public static Dictionary<int, LianQiLingWenBiao> DataDict = new Dictionary<int, LianQiLingWenBiao>();

		// Token: 0x04004831 RID: 18481
		public static List<LianQiLingWenBiao> DataList = new List<LianQiLingWenBiao>();

		// Token: 0x04004832 RID: 18482
		public static Action OnInitFinishAction = new Action(LianQiLingWenBiao.OnInitFinish);

		// Token: 0x04004833 RID: 18483
		public int id;

		// Token: 0x04004834 RID: 18484
		public int type;

		// Token: 0x04004835 RID: 18485
		public int value1;

		// Token: 0x04004836 RID: 18486
		public int value2;

		// Token: 0x04004837 RID: 18487
		public int value3;

		// Token: 0x04004838 RID: 18488
		public int value4;

		// Token: 0x04004839 RID: 18489
		public int seid;

		// Token: 0x0400483A RID: 18490
		public int Itemseid;

		// Token: 0x0400483B RID: 18491
		public string name;

		// Token: 0x0400483C RID: 18492
		public string desc;

		// Token: 0x0400483D RID: 18493
		public string xiangxidesc;

		// Token: 0x0400483E RID: 18494
		public List<int> Affix = new List<int>();

		// Token: 0x0400483F RID: 18495
		public List<int> listvalue1 = new List<int>();

		// Token: 0x04004840 RID: 18496
		public List<int> listvalue2 = new List<int>();

		// Token: 0x04004841 RID: 18497
		public List<int> listvalue3 = new List<int>();

		// Token: 0x04004842 RID: 18498
		public List<int> Itemintvalue1 = new List<int>();

		// Token: 0x04004843 RID: 18499
		public List<int> Itemintvalue2 = new List<int>();
	}
}
