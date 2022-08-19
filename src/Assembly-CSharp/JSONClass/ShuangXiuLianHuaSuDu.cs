using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E0 RID: 2272
	public class ShuangXiuLianHuaSuDu : IJSONClass
	{
		// Token: 0x06004193 RID: 16787 RVA: 0x001C0DC8 File Offset: 0x001BEFC8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShuangXiuLianHuaSuDu.list)
			{
				try
				{
					ShuangXiuLianHuaSuDu shuangXiuLianHuaSuDu = new ShuangXiuLianHuaSuDu();
					shuangXiuLianHuaSuDu.id = jsonobject["id"].I;
					shuangXiuLianHuaSuDu.speed = jsonobject["speed"].I;
					if (ShuangXiuLianHuaSuDu.DataDict.ContainsKey(shuangXiuLianHuaSuDu.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShuangXiuLianHuaSuDu.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shuangXiuLianHuaSuDu.id));
					}
					else
					{
						ShuangXiuLianHuaSuDu.DataDict.Add(shuangXiuLianHuaSuDu.id, shuangXiuLianHuaSuDu);
						ShuangXiuLianHuaSuDu.DataList.Add(shuangXiuLianHuaSuDu);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShuangXiuLianHuaSuDu.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShuangXiuLianHuaSuDu.OnInitFinishAction != null)
			{
				ShuangXiuLianHuaSuDu.OnInitFinishAction();
			}
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040F9 RID: 16633
		public static Dictionary<int, ShuangXiuLianHuaSuDu> DataDict = new Dictionary<int, ShuangXiuLianHuaSuDu>();

		// Token: 0x040040FA RID: 16634
		public static List<ShuangXiuLianHuaSuDu> DataList = new List<ShuangXiuLianHuaSuDu>();

		// Token: 0x040040FB RID: 16635
		public static Action OnInitFinishAction = new Action(ShuangXiuLianHuaSuDu.OnInitFinish);

		// Token: 0x040040FC RID: 16636
		public int id;

		// Token: 0x040040FD RID: 16637
		public int speed;
	}
}
