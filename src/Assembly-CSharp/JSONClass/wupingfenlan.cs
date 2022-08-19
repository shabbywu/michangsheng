using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200098D RID: 2445
	public class wupingfenlan : IJSONClass
	{
		// Token: 0x06004448 RID: 17480 RVA: 0x001D138C File Offset: 0x001CF58C
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

		// Token: 0x06004449 RID: 17481 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045ED RID: 17901
		public static Dictionary<int, wupingfenlan> DataDict = new Dictionary<int, wupingfenlan>();

		// Token: 0x040045EE RID: 17902
		public static List<wupingfenlan> DataList = new List<wupingfenlan>();

		// Token: 0x040045EF RID: 17903
		public static Action OnInitFinishAction = new Action(wupingfenlan.OnInitFinish);

		// Token: 0x040045F0 RID: 17904
		public int id;

		// Token: 0x040045F1 RID: 17905
		public List<int> ItemFlag = new List<int>();
	}
}
