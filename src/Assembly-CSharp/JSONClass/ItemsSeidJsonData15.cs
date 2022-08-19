using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000853 RID: 2131
	public class ItemsSeidJsonData15 : IJSONClass
	{
		// Token: 0x06003F5E RID: 16222 RVA: 0x001B0EDC File Offset: 0x001AF0DC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[15].list)
			{
				try
				{
					ItemsSeidJsonData15 itemsSeidJsonData = new ItemsSeidJsonData15();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData15.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData15.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData15.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData15.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData15.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData15.OnInitFinishAction != null)
			{
				ItemsSeidJsonData15.OnInitFinishAction();
			}
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BA6 RID: 15270
		public static int SEIDID = 15;

		// Token: 0x04003BA7 RID: 15271
		public static Dictionary<int, ItemsSeidJsonData15> DataDict = new Dictionary<int, ItemsSeidJsonData15>();

		// Token: 0x04003BA8 RID: 15272
		public static List<ItemsSeidJsonData15> DataList = new List<ItemsSeidJsonData15>();

		// Token: 0x04003BA9 RID: 15273
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData15.OnInitFinish);

		// Token: 0x04003BAA RID: 15274
		public int id;

		// Token: 0x04003BAB RID: 15275
		public int value1;

		// Token: 0x04003BAC RID: 15276
		public int value2;
	}
}
