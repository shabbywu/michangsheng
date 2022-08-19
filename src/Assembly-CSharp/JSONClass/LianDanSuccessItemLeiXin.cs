using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000874 RID: 2164
	public class LianDanSuccessItemLeiXin : IJSONClass
	{
		// Token: 0x06003FE3 RID: 16355 RVA: 0x001B4108 File Offset: 0x001B2308
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianDanSuccessItemLeiXin.list)
			{
				try
				{
					LianDanSuccessItemLeiXin lianDanSuccessItemLeiXin = new LianDanSuccessItemLeiXin();
					lianDanSuccessItemLeiXin.id = jsonobject["id"].I;
					lianDanSuccessItemLeiXin.zhonglei = jsonobject["zhonglei"].I;
					lianDanSuccessItemLeiXin.desc = jsonobject["desc"].Str;
					if (LianDanSuccessItemLeiXin.DataDict.ContainsKey(lianDanSuccessItemLeiXin.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianDanSuccessItemLeiXin.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianDanSuccessItemLeiXin.id));
					}
					else
					{
						LianDanSuccessItemLeiXin.DataDict.Add(lianDanSuccessItemLeiXin.id, lianDanSuccessItemLeiXin);
						LianDanSuccessItemLeiXin.DataList.Add(lianDanSuccessItemLeiXin);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianDanSuccessItemLeiXin.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianDanSuccessItemLeiXin.OnInitFinishAction != null)
			{
				LianDanSuccessItemLeiXin.OnInitFinishAction();
			}
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C97 RID: 15511
		public static Dictionary<int, LianDanSuccessItemLeiXin> DataDict = new Dictionary<int, LianDanSuccessItemLeiXin>();

		// Token: 0x04003C98 RID: 15512
		public static List<LianDanSuccessItemLeiXin> DataList = new List<LianDanSuccessItemLeiXin>();

		// Token: 0x04003C99 RID: 15513
		public static Action OnInitFinishAction = new Action(LianDanSuccessItemLeiXin.OnInitFinish);

		// Token: 0x04003C9A RID: 15514
		public int id;

		// Token: 0x04003C9B RID: 15515
		public int zhonglei;

		// Token: 0x04003C9C RID: 15516
		public string desc;
	}
}
