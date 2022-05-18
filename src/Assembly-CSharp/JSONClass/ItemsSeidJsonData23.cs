using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE7 RID: 3047
	public class ItemsSeidJsonData23 : IJSONClass
	{
		// Token: 0x06004B04 RID: 19204 RVA: 0x001FB640 File Offset: 0x001F9840
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[23].list)
			{
				try
				{
					ItemsSeidJsonData23 itemsSeidJsonData = new ItemsSeidJsonData23();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData23.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData23.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData23.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData23.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData23.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData23.OnInitFinishAction != null)
			{
				ItemsSeidJsonData23.OnInitFinishAction();
			}
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004724 RID: 18212
		public static int SEIDID = 23;

		// Token: 0x04004725 RID: 18213
		public static Dictionary<int, ItemsSeidJsonData23> DataDict = new Dictionary<int, ItemsSeidJsonData23>();

		// Token: 0x04004726 RID: 18214
		public static List<ItemsSeidJsonData23> DataList = new List<ItemsSeidJsonData23>();

		// Token: 0x04004727 RID: 18215
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData23.OnInitFinish);

		// Token: 0x04004728 RID: 18216
		public int id;

		// Token: 0x04004729 RID: 18217
		public int value1;
	}
}
