using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000867 RID: 2151
	public class ItemsSeidJsonData8 : IJSONClass
	{
		// Token: 0x06003FAE RID: 16302 RVA: 0x001B2AB8 File Offset: 0x001B0CB8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[8].list)
			{
				try
				{
					ItemsSeidJsonData8 itemsSeidJsonData = new ItemsSeidJsonData8();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData8.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData8.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData8.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData8.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData8.OnInitFinishAction != null)
			{
				ItemsSeidJsonData8.OnInitFinishAction();
			}
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C26 RID: 15398
		public static int SEIDID = 8;

		// Token: 0x04003C27 RID: 15399
		public static Dictionary<int, ItemsSeidJsonData8> DataDict = new Dictionary<int, ItemsSeidJsonData8>();

		// Token: 0x04003C28 RID: 15400
		public static List<ItemsSeidJsonData8> DataList = new List<ItemsSeidJsonData8>();

		// Token: 0x04003C29 RID: 15401
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData8.OnInitFinish);

		// Token: 0x04003C2A RID: 15402
		public int id;

		// Token: 0x04003C2B RID: 15403
		public int value1;
	}
}
