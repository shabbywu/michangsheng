using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B9A RID: 2970
	public class CaiYaoDiaoLuo : IJSONClass
	{
		// Token: 0x060049D0 RID: 18896 RVA: 0x001F4808 File Offset: 0x001F2A08
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CaiYaoDiaoLuo.list)
			{
				try
				{
					CaiYaoDiaoLuo caiYaoDiaoLuo = new CaiYaoDiaoLuo();
					caiYaoDiaoLuo.id = jsonobject["id"].I;
					caiYaoDiaoLuo.type = jsonobject["type"].I;
					caiYaoDiaoLuo.MapIndex = jsonobject["MapIndex"].I;
					caiYaoDiaoLuo.ThreeSence = jsonobject["ThreeSence"].I;
					caiYaoDiaoLuo.value1 = jsonobject["value1"].I;
					caiYaoDiaoLuo.value2 = jsonobject["value2"].I;
					caiYaoDiaoLuo.value3 = jsonobject["value3"].I;
					caiYaoDiaoLuo.value4 = jsonobject["value4"].I;
					caiYaoDiaoLuo.value5 = jsonobject["value5"].I;
					caiYaoDiaoLuo.value6 = jsonobject["value6"].I;
					caiYaoDiaoLuo.value7 = jsonobject["value7"].I;
					caiYaoDiaoLuo.value8 = jsonobject["value8"].I;
					caiYaoDiaoLuo.name = jsonobject["name"].Str;
					caiYaoDiaoLuo.FuBen = jsonobject["FuBen"].Str;
					if (CaiYaoDiaoLuo.DataDict.ContainsKey(caiYaoDiaoLuo.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CaiYaoDiaoLuo.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", caiYaoDiaoLuo.id));
					}
					else
					{
						CaiYaoDiaoLuo.DataDict.Add(caiYaoDiaoLuo.id, caiYaoDiaoLuo);
						CaiYaoDiaoLuo.DataList.Add(caiYaoDiaoLuo);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CaiYaoDiaoLuo.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CaiYaoDiaoLuo.OnInitFinishAction != null)
			{
				CaiYaoDiaoLuo.OnInitFinishAction();
			}
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044A6 RID: 17574
		public static Dictionary<int, CaiYaoDiaoLuo> DataDict = new Dictionary<int, CaiYaoDiaoLuo>();

		// Token: 0x040044A7 RID: 17575
		public static List<CaiYaoDiaoLuo> DataList = new List<CaiYaoDiaoLuo>();

		// Token: 0x040044A8 RID: 17576
		public static Action OnInitFinishAction = new Action(CaiYaoDiaoLuo.OnInitFinish);

		// Token: 0x040044A9 RID: 17577
		public int id;

		// Token: 0x040044AA RID: 17578
		public int type;

		// Token: 0x040044AB RID: 17579
		public int MapIndex;

		// Token: 0x040044AC RID: 17580
		public int ThreeSence;

		// Token: 0x040044AD RID: 17581
		public int value1;

		// Token: 0x040044AE RID: 17582
		public int value2;

		// Token: 0x040044AF RID: 17583
		public int value3;

		// Token: 0x040044B0 RID: 17584
		public int value4;

		// Token: 0x040044B1 RID: 17585
		public int value5;

		// Token: 0x040044B2 RID: 17586
		public int value6;

		// Token: 0x040044B3 RID: 17587
		public int value7;

		// Token: 0x040044B4 RID: 17588
		public int value8;

		// Token: 0x040044B5 RID: 17589
		public string name;

		// Token: 0x040044B6 RID: 17590
		public string FuBen;
	}
}
