using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C02 RID: 3074
	public class LianDanSuccessItemLeiXin : IJSONClass
	{
		// Token: 0x06004B71 RID: 19313 RVA: 0x001FDAA8 File Offset: 0x001FBCA8
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

		// Token: 0x06004B72 RID: 19314 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047F0 RID: 18416
		public static Dictionary<int, LianDanSuccessItemLeiXin> DataDict = new Dictionary<int, LianDanSuccessItemLeiXin>();

		// Token: 0x040047F1 RID: 18417
		public static List<LianDanSuccessItemLeiXin> DataList = new List<LianDanSuccessItemLeiXin>();

		// Token: 0x040047F2 RID: 18418
		public static Action OnInitFinishAction = new Action(LianDanSuccessItemLeiXin.OnInitFinish);

		// Token: 0x040047F3 RID: 18419
		public int id;

		// Token: 0x040047F4 RID: 18420
		public int zhonglei;

		// Token: 0x040047F5 RID: 18421
		public string desc;
	}
}
