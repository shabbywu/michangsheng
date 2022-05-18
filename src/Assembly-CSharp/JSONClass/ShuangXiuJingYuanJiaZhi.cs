using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C6C RID: 3180
	public class ShuangXiuJingYuanJiaZhi : IJSONClass
	{
		// Token: 0x06004D19 RID: 19737 RVA: 0x00208F04 File Offset: 0x00207104
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

		// Token: 0x06004D1A RID: 19738 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C3E RID: 19518
		public static Dictionary<int, ShuangXiuJingYuanJiaZhi> DataDict = new Dictionary<int, ShuangXiuJingYuanJiaZhi>();

		// Token: 0x04004C3F RID: 19519
		public static List<ShuangXiuJingYuanJiaZhi> DataList = new List<ShuangXiuJingYuanJiaZhi>();

		// Token: 0x04004C40 RID: 19520
		public static Action OnInitFinishAction = new Action(ShuangXiuJingYuanJiaZhi.OnInitFinish);

		// Token: 0x04004C41 RID: 19521
		public int id;

		// Token: 0x04004C42 RID: 19522
		public int jiazhi;
	}
}
