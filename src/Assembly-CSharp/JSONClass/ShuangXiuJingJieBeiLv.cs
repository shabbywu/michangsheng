using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008DE RID: 2270
	public class ShuangXiuJingJieBeiLv : IJSONClass
	{
		// Token: 0x0600418B RID: 16779 RVA: 0x001C0B30 File Offset: 0x001BED30
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShuangXiuJingJieBeiLv.list)
			{
				try
				{
					ShuangXiuJingJieBeiLv shuangXiuJingJieBeiLv = new ShuangXiuJingJieBeiLv();
					shuangXiuJingJieBeiLv.id = jsonobject["id"].I;
					shuangXiuJingJieBeiLv.BeiLv = jsonobject["BeiLv"].I;
					if (ShuangXiuJingJieBeiLv.DataDict.ContainsKey(shuangXiuJingJieBeiLv.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShuangXiuJingJieBeiLv.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shuangXiuJingJieBeiLv.id));
					}
					else
					{
						ShuangXiuJingJieBeiLv.DataDict.Add(shuangXiuJingJieBeiLv.id, shuangXiuJingJieBeiLv);
						ShuangXiuJingJieBeiLv.DataList.Add(shuangXiuJingJieBeiLv);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShuangXiuJingJieBeiLv.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShuangXiuJingJieBeiLv.OnInitFinishAction != null)
			{
				ShuangXiuJingJieBeiLv.OnInitFinishAction();
			}
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040EF RID: 16623
		public static Dictionary<int, ShuangXiuJingJieBeiLv> DataDict = new Dictionary<int, ShuangXiuJingJieBeiLv>();

		// Token: 0x040040F0 RID: 16624
		public static List<ShuangXiuJingJieBeiLv> DataList = new List<ShuangXiuJingJieBeiLv>();

		// Token: 0x040040F1 RID: 16625
		public static Action OnInitFinishAction = new Action(ShuangXiuJingJieBeiLv.OnInitFinish);

		// Token: 0x040040F2 RID: 16626
		public int id;

		// Token: 0x040040F3 RID: 16627
		public int BeiLv;
	}
}
