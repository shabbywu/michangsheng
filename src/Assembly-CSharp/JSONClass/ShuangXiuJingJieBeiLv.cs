using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C6B RID: 3179
	public class ShuangXiuJingJieBeiLv : IJSONClass
	{
		// Token: 0x06004D15 RID: 19733 RVA: 0x00208DE0 File Offset: 0x00206FE0
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

		// Token: 0x06004D16 RID: 19734 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C39 RID: 19513
		public static Dictionary<int, ShuangXiuJingJieBeiLv> DataDict = new Dictionary<int, ShuangXiuJingJieBeiLv>();

		// Token: 0x04004C3A RID: 19514
		public static List<ShuangXiuJingJieBeiLv> DataList = new List<ShuangXiuJingJieBeiLv>();

		// Token: 0x04004C3B RID: 19515
		public static Action OnInitFinishAction = new Action(ShuangXiuJingJieBeiLv.OnInitFinish);

		// Token: 0x04004C3C RID: 19516
		public int id;

		// Token: 0x04004C3D RID: 19517
		public int BeiLv;
	}
}
