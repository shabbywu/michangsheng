using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE0 RID: 3040
	public class ItemsSeidJsonData13 : IJSONClass
	{
		// Token: 0x06004AE8 RID: 19176 RVA: 0x001FAE00 File Offset: 0x001F9000
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[13].list)
			{
				try
				{
					ItemsSeidJsonData13 itemsSeidJsonData = new ItemsSeidJsonData13();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData13.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData13.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData13.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData13.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData13.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData13.OnInitFinishAction != null)
			{
				ItemsSeidJsonData13.OnInitFinishAction();
			}
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046F8 RID: 18168
		public static int SEIDID = 13;

		// Token: 0x040046F9 RID: 18169
		public static Dictionary<int, ItemsSeidJsonData13> DataDict = new Dictionary<int, ItemsSeidJsonData13>();

		// Token: 0x040046FA RID: 18170
		public static List<ItemsSeidJsonData13> DataList = new List<ItemsSeidJsonData13>();

		// Token: 0x040046FB RID: 18171
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData13.OnInitFinish);

		// Token: 0x040046FC RID: 18172
		public int id;

		// Token: 0x040046FD RID: 18173
		public int value1;
	}
}
