using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF8 RID: 3320
	public class TianJiDaBiGongFangKeZhi : IJSONClass
	{
		// Token: 0x06004F48 RID: 20296 RVA: 0x002141BC File Offset: 0x002123BC
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

		// Token: 0x06004F49 RID: 20297 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400502D RID: 20525
		public static Dictionary<int, TianJiDaBiGongFangKeZhi> DataDict = new Dictionary<int, TianJiDaBiGongFangKeZhi>();

		// Token: 0x0400502E RID: 20526
		public static List<TianJiDaBiGongFangKeZhi> DataList = new List<TianJiDaBiGongFangKeZhi>();

		// Token: 0x0400502F RID: 20527
		public static Action OnInitFinishAction = new Action(TianJiDaBiGongFangKeZhi.OnInitFinish);

		// Token: 0x04005030 RID: 20528
		public int id;

		// Token: 0x04005031 RID: 20529
		public int AttackType1;

		// Token: 0x04005032 RID: 20530
		public int AttackType2;

		// Token: 0x04005033 RID: 20531
		public int AttackType3;

		// Token: 0x04005034 RID: 20532
		public int AttackType4;

		// Token: 0x04005035 RID: 20533
		public int AttackType5;
	}
}
