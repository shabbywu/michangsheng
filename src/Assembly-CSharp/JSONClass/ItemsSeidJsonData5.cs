using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF3 RID: 3059
	public class ItemsSeidJsonData5 : IJSONClass
	{
		// Token: 0x06004B34 RID: 19252 RVA: 0x001FC49C File Offset: 0x001FA69C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[5].list)
			{
				try
				{
					ItemsSeidJsonData5 itemsSeidJsonData = new ItemsSeidJsonData5();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData5.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData5.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData5.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData5.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData5.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData5.OnInitFinishAction != null)
			{
				ItemsSeidJsonData5.OnInitFinishAction();
			}
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004772 RID: 18290
		public static int SEIDID = 5;

		// Token: 0x04004773 RID: 18291
		public static Dictionary<int, ItemsSeidJsonData5> DataDict = new Dictionary<int, ItemsSeidJsonData5>();

		// Token: 0x04004774 RID: 18292
		public static List<ItemsSeidJsonData5> DataList = new List<ItemsSeidJsonData5>();

		// Token: 0x04004775 RID: 18293
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData5.OnInitFinish);

		// Token: 0x04004776 RID: 18294
		public int id;

		// Token: 0x04004777 RID: 18295
		public int value1;
	}
}
