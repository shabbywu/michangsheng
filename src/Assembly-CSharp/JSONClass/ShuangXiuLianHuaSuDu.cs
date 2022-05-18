using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C6D RID: 3181
	public class ShuangXiuLianHuaSuDu : IJSONClass
	{
		// Token: 0x06004D1D RID: 19741 RVA: 0x00209028 File Offset: 0x00207228
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

		// Token: 0x06004D1E RID: 19742 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C43 RID: 19523
		public static Dictionary<int, ShuangXiuLianHuaSuDu> DataDict = new Dictionary<int, ShuangXiuLianHuaSuDu>();

		// Token: 0x04004C44 RID: 19524
		public static List<ShuangXiuLianHuaSuDu> DataList = new List<ShuangXiuLianHuaSuDu>();

		// Token: 0x04004C45 RID: 19525
		public static Action OnInitFinishAction = new Action(ShuangXiuLianHuaSuDu.OnInitFinish);

		// Token: 0x04004C46 RID: 19526
		public int id;

		// Token: 0x04004C47 RID: 19527
		public int speed;
	}
}
