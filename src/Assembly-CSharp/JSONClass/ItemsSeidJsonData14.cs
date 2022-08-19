using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000852 RID: 2130
	public class ItemsSeidJsonData14 : IJSONClass
	{
		// Token: 0x06003F5A RID: 16218 RVA: 0x001B0D84 File Offset: 0x001AEF84
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[14].list)
			{
				try
				{
					ItemsSeidJsonData14 itemsSeidJsonData = new ItemsSeidJsonData14();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData14.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData14.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData14.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData14.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData14.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData14.OnInitFinishAction != null)
			{
				ItemsSeidJsonData14.OnInitFinishAction();
			}
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BA0 RID: 15264
		public static int SEIDID = 14;

		// Token: 0x04003BA1 RID: 15265
		public static Dictionary<int, ItemsSeidJsonData14> DataDict = new Dictionary<int, ItemsSeidJsonData14>();

		// Token: 0x04003BA2 RID: 15266
		public static List<ItemsSeidJsonData14> DataList = new List<ItemsSeidJsonData14>();

		// Token: 0x04003BA3 RID: 15267
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData14.OnInitFinish);

		// Token: 0x04003BA4 RID: 15268
		public int id;

		// Token: 0x04003BA5 RID: 15269
		public int value1;
	}
}
