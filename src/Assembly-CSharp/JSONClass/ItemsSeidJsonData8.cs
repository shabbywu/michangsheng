using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF6 RID: 3062
	public class ItemsSeidJsonData8 : IJSONClass
	{
		// Token: 0x06004B40 RID: 19264 RVA: 0x001FC814 File Offset: 0x001FAA14
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

		// Token: 0x06004B41 RID: 19265 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004784 RID: 18308
		public static int SEIDID = 8;

		// Token: 0x04004785 RID: 18309
		public static Dictionary<int, ItemsSeidJsonData8> DataDict = new Dictionary<int, ItemsSeidJsonData8>();

		// Token: 0x04004786 RID: 18310
		public static List<ItemsSeidJsonData8> DataList = new List<ItemsSeidJsonData8>();

		// Token: 0x04004787 RID: 18311
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData8.OnInitFinish);

		// Token: 0x04004788 RID: 18312
		public int id;

		// Token: 0x04004789 RID: 18313
		public int value1;
	}
}
