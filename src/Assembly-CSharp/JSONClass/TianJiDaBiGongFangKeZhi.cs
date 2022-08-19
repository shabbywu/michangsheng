using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000974 RID: 2420
	public class TianJiDaBiGongFangKeZhi : IJSONClass
	{
		// Token: 0x060043E2 RID: 17378 RVA: 0x001CE8BC File Offset: 0x001CCABC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TianJiDaBiGongFangKeZhi.list)
			{
				try
				{
					TianJiDaBiGongFangKeZhi tianJiDaBiGongFangKeZhi = new TianJiDaBiGongFangKeZhi();
					tianJiDaBiGongFangKeZhi.id = jsonobject["id"].I;
					tianJiDaBiGongFangKeZhi.AttackType1 = jsonobject["AttackType1"].I;
					tianJiDaBiGongFangKeZhi.AttackType2 = jsonobject["AttackType2"].I;
					tianJiDaBiGongFangKeZhi.AttackType3 = jsonobject["AttackType3"].I;
					tianJiDaBiGongFangKeZhi.AttackType4 = jsonobject["AttackType4"].I;
					tianJiDaBiGongFangKeZhi.AttackType5 = jsonobject["AttackType5"].I;
					if (TianJiDaBiGongFangKeZhi.DataDict.ContainsKey(tianJiDaBiGongFangKeZhi.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TianJiDaBiGongFangKeZhi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tianJiDaBiGongFangKeZhi.id));
					}
					else
					{
						TianJiDaBiGongFangKeZhi.DataDict.Add(tianJiDaBiGongFangKeZhi.id, tianJiDaBiGongFangKeZhi);
						TianJiDaBiGongFangKeZhi.DataList.Add(tianJiDaBiGongFangKeZhi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TianJiDaBiGongFangKeZhi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TianJiDaBiGongFangKeZhi.OnInitFinishAction != null)
			{
				TianJiDaBiGongFangKeZhi.OnInitFinishAction();
			}
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400451D RID: 17693
		public static Dictionary<int, TianJiDaBiGongFangKeZhi> DataDict = new Dictionary<int, TianJiDaBiGongFangKeZhi>();

		// Token: 0x0400451E RID: 17694
		public static List<TianJiDaBiGongFangKeZhi> DataList = new List<TianJiDaBiGongFangKeZhi>();

		// Token: 0x0400451F RID: 17695
		public static Action OnInitFinishAction = new Action(TianJiDaBiGongFangKeZhi.OnInitFinish);

		// Token: 0x04004520 RID: 17696
		public int id;

		// Token: 0x04004521 RID: 17697
		public int AttackType1;

		// Token: 0x04004522 RID: 17698
		public int AttackType2;

		// Token: 0x04004523 RID: 17699
		public int AttackType3;

		// Token: 0x04004524 RID: 17700
		public int AttackType4;

		// Token: 0x04004525 RID: 17701
		public int AttackType5;
	}
}
