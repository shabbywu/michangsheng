using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BDB RID: 3035
	public class ItemGoodSeid1JsonData : IJSONClass
	{
		// Token: 0x06004AD4 RID: 19156 RVA: 0x001FA828 File Offset: 0x001F8A28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemGoodSeid1JsonData.list)
			{
				try
				{
					ItemGoodSeid1JsonData itemGoodSeid1JsonData = new ItemGoodSeid1JsonData();
					itemGoodSeid1JsonData.id = jsonobject["id"].I;
					itemGoodSeid1JsonData.value1 = jsonobject["value1"].I;
					if (ItemGoodSeid1JsonData.DataDict.ContainsKey(itemGoodSeid1JsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemGoodSeid1JsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemGoodSeid1JsonData.id));
					}
					else
					{
						ItemGoodSeid1JsonData.DataDict.Add(itemGoodSeid1JsonData.id, itemGoodSeid1JsonData);
						ItemGoodSeid1JsonData.DataList.Add(itemGoodSeid1JsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemGoodSeid1JsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemGoodSeid1JsonData.OnInitFinishAction != null)
			{
				ItemGoodSeid1JsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046DA RID: 18138
		public static Dictionary<int, ItemGoodSeid1JsonData> DataDict = new Dictionary<int, ItemGoodSeid1JsonData>();

		// Token: 0x040046DB RID: 18139
		public static List<ItemGoodSeid1JsonData> DataList = new List<ItemGoodSeid1JsonData>();

		// Token: 0x040046DC RID: 18140
		public static Action OnInitFinishAction = new Action(ItemGoodSeid1JsonData.OnInitFinish);

		// Token: 0x040046DD RID: 18141
		public int id;

		// Token: 0x040046DE RID: 18142
		public int value1;
	}
}
