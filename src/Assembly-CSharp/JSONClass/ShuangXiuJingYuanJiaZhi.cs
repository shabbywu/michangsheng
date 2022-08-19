using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008DF RID: 2271
	public class ShuangXiuJingYuanJiaZhi : IJSONClass
	{
		// Token: 0x0600418F RID: 16783 RVA: 0x001C0C7C File Offset: 0x001BEE7C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShuangXiuJingYuanJiaZhi.list)
			{
				try
				{
					ShuangXiuJingYuanJiaZhi shuangXiuJingYuanJiaZhi = new ShuangXiuJingYuanJiaZhi();
					shuangXiuJingYuanJiaZhi.id = jsonobject["id"].I;
					shuangXiuJingYuanJiaZhi.jiazhi = jsonobject["jiazhi"].I;
					if (ShuangXiuJingYuanJiaZhi.DataDict.ContainsKey(shuangXiuJingYuanJiaZhi.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShuangXiuJingYuanJiaZhi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shuangXiuJingYuanJiaZhi.id));
					}
					else
					{
						ShuangXiuJingYuanJiaZhi.DataDict.Add(shuangXiuJingYuanJiaZhi.id, shuangXiuJingYuanJiaZhi);
						ShuangXiuJingYuanJiaZhi.DataList.Add(shuangXiuJingYuanJiaZhi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShuangXiuJingYuanJiaZhi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShuangXiuJingYuanJiaZhi.OnInitFinishAction != null)
			{
				ShuangXiuJingYuanJiaZhi.OnInitFinishAction();
			}
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040F4 RID: 16628
		public static Dictionary<int, ShuangXiuJingYuanJiaZhi> DataDict = new Dictionary<int, ShuangXiuJingYuanJiaZhi>();

		// Token: 0x040040F5 RID: 16629
		public static List<ShuangXiuJingYuanJiaZhi> DataList = new List<ShuangXiuJingYuanJiaZhi>();

		// Token: 0x040040F6 RID: 16630
		public static Action OnInitFinishAction = new Action(ShuangXiuJingYuanJiaZhi.OnInitFinish);

		// Token: 0x040040F7 RID: 16631
		public int id;

		// Token: 0x040040F8 RID: 16632
		public int jiazhi;
	}
}
