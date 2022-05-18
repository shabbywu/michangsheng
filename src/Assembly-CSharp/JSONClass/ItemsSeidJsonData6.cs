using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF4 RID: 3060
	public class ItemsSeidJsonData6 : IJSONClass
	{
		// Token: 0x06004B38 RID: 19256 RVA: 0x001FC5C4 File Offset: 0x001FA7C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[6].list)
			{
				try
				{
					ItemsSeidJsonData6 itemsSeidJsonData = new ItemsSeidJsonData6();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData6.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData6.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData6.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData6.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData6.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData6.OnInitFinishAction != null)
			{
				ItemsSeidJsonData6.OnInitFinishAction();
			}
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004778 RID: 18296
		public static int SEIDID = 6;

		// Token: 0x04004779 RID: 18297
		public static Dictionary<int, ItemsSeidJsonData6> DataDict = new Dictionary<int, ItemsSeidJsonData6>();

		// Token: 0x0400477A RID: 18298
		public static List<ItemsSeidJsonData6> DataList = new List<ItemsSeidJsonData6>();

		// Token: 0x0400477B RID: 18299
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData6.OnInitFinish);

		// Token: 0x0400477C RID: 18300
		public int id;

		// Token: 0x0400477D RID: 18301
		public int value1;
	}
}
