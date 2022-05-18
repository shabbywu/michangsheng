using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE0 RID: 2784
	public class AllMapCaiJiAddItemBiao : IJSONClass
	{
		// Token: 0x060046EA RID: 18154 RVA: 0x001E58A0 File Offset: 0x001E3AA0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapCaiJiAddItemBiao.list)
			{
				try
				{
					AllMapCaiJiAddItemBiao allMapCaiJiAddItemBiao = new AllMapCaiJiAddItemBiao();
					allMapCaiJiAddItemBiao.ID = jsonobject["ID"].I;
					allMapCaiJiAddItemBiao.percent = jsonobject["percent"].I;
					allMapCaiJiAddItemBiao.time = jsonobject["time"].I;
					if (AllMapCaiJiAddItemBiao.DataDict.ContainsKey(allMapCaiJiAddItemBiao.ID))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapCaiJiAddItemBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapCaiJiAddItemBiao.ID));
					}
					else
					{
						AllMapCaiJiAddItemBiao.DataDict.Add(allMapCaiJiAddItemBiao.ID, allMapCaiJiAddItemBiao);
						AllMapCaiJiAddItemBiao.DataList.Add(allMapCaiJiAddItemBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiAddItemBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapCaiJiAddItemBiao.OnInitFinishAction != null)
			{
				AllMapCaiJiAddItemBiao.OnInitFinishAction();
			}
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F52 RID: 16210
		public static Dictionary<int, AllMapCaiJiAddItemBiao> DataDict = new Dictionary<int, AllMapCaiJiAddItemBiao>();

		// Token: 0x04003F53 RID: 16211
		public static List<AllMapCaiJiAddItemBiao> DataList = new List<AllMapCaiJiAddItemBiao>();

		// Token: 0x04003F54 RID: 16212
		public static Action OnInitFinishAction = new Action(AllMapCaiJiAddItemBiao.OnInitFinish);

		// Token: 0x04003F55 RID: 16213
		public int ID;

		// Token: 0x04003F56 RID: 16214
		public int percent;

		// Token: 0x04003F57 RID: 16215
		public int time;
	}
}
