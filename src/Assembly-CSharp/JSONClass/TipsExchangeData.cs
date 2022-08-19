using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000979 RID: 2425
	public class TipsExchangeData : IJSONClass
	{
		// Token: 0x060043F8 RID: 17400 RVA: 0x001CF478 File Offset: 0x001CD678
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TipsExchangeData.list)
			{
				try
				{
					TipsExchangeData tipsExchangeData = new TipsExchangeData();
					tipsExchangeData.id = jsonobject["id"].I;
					tipsExchangeData.TiShi = jsonobject["TiShi"].Str;
					if (TipsExchangeData.DataDict.ContainsKey(tipsExchangeData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TipsExchangeData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tipsExchangeData.id));
					}
					else
					{
						TipsExchangeData.DataDict.Add(tipsExchangeData.id, tipsExchangeData);
						TipsExchangeData.DataList.Add(tipsExchangeData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TipsExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TipsExchangeData.OnInitFinishAction != null)
			{
				TipsExchangeData.OnInitFinishAction();
			}
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004555 RID: 17749
		public static Dictionary<int, TipsExchangeData> DataDict = new Dictionary<int, TipsExchangeData>();

		// Token: 0x04004556 RID: 17750
		public static List<TipsExchangeData> DataList = new List<TipsExchangeData>();

		// Token: 0x04004557 RID: 17751
		public static Action OnInitFinishAction = new Action(TipsExchangeData.OnInitFinish);

		// Token: 0x04004558 RID: 17752
		public int id;

		// Token: 0x04004559 RID: 17753
		public string TiShi;
	}
}
