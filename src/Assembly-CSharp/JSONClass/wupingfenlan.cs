using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D0D RID: 3341
	public class wupingfenlan : IJSONClass
	{
		// Token: 0x06004F9E RID: 20382 RVA: 0x00216258 File Offset: 0x00214458
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.wupingfenlan.list)
			{
				try
				{
					wupingfenlan wupingfenlan = new wupingfenlan();
					wupingfenlan.id = jsonobject["id"].I;
					wupingfenlan.ItemFlag = jsonobject["ItemFlag"].ToList();
					if (wupingfenlan.DataDict.ContainsKey(wupingfenlan.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典wupingfenlan.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wupingfenlan.id));
					}
					else
					{
						wupingfenlan.DataDict.Add(wupingfenlan.id, wupingfenlan);
						wupingfenlan.DataList.Add(wupingfenlan);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典wupingfenlan.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (wupingfenlan.OnInitFinishAction != null)
			{
				wupingfenlan.OnInitFinishAction();
			}
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050E1 RID: 20705
		public static Dictionary<int, wupingfenlan> DataDict = new Dictionary<int, wupingfenlan>();

		// Token: 0x040050E2 RID: 20706
		public static List<wupingfenlan> DataList = new List<wupingfenlan>();

		// Token: 0x040050E3 RID: 20707
		public static Action OnInitFinishAction = new Action(wupingfenlan.OnInitFinish);

		// Token: 0x040050E4 RID: 20708
		public int id;

		// Token: 0x040050E5 RID: 20709
		public List<int> ItemFlag = new List<int>();
	}
}
