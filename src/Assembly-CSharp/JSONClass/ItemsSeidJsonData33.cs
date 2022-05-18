using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF0 RID: 3056
	public class ItemsSeidJsonData33 : IJSONClass
	{
		// Token: 0x06004B28 RID: 19240 RVA: 0x001FC110 File Offset: 0x001FA310
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[33].list)
			{
				try
				{
					ItemsSeidJsonData33 itemsSeidJsonData = new ItemsSeidJsonData33();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData33.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData33.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData33.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData33.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData33.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData33.OnInitFinishAction != null)
			{
				ItemsSeidJsonData33.OnInitFinishAction();
			}
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400475F RID: 18271
		public static int SEIDID = 33;

		// Token: 0x04004760 RID: 18272
		public static Dictionary<int, ItemsSeidJsonData33> DataDict = new Dictionary<int, ItemsSeidJsonData33>();

		// Token: 0x04004761 RID: 18273
		public static List<ItemsSeidJsonData33> DataList = new List<ItemsSeidJsonData33>();

		// Token: 0x04004762 RID: 18274
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData33.OnInitFinish);

		// Token: 0x04004763 RID: 18275
		public int id;

		// Token: 0x04004764 RID: 18276
		public int value1;

		// Token: 0x04004765 RID: 18277
		public int value2;
	}
}
