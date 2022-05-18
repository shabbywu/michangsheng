using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF7 RID: 3063
	public class ItemsSeidJsonData9 : IJSONClass
	{
		// Token: 0x06004B44 RID: 19268 RVA: 0x001FC93C File Offset: 0x001FAB3C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[9].list)
			{
				try
				{
					ItemsSeidJsonData9 itemsSeidJsonData = new ItemsSeidJsonData9();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData9.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData9.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData9.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData9.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData9.OnInitFinishAction != null)
			{
				ItemsSeidJsonData9.OnInitFinishAction();
			}
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400478A RID: 18314
		public static int SEIDID = 9;

		// Token: 0x0400478B RID: 18315
		public static Dictionary<int, ItemsSeidJsonData9> DataDict = new Dictionary<int, ItemsSeidJsonData9>();

		// Token: 0x0400478C RID: 18316
		public static List<ItemsSeidJsonData9> DataList = new List<ItemsSeidJsonData9>();

		// Token: 0x0400478D RID: 18317
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData9.OnInitFinish);

		// Token: 0x0400478E RID: 18318
		public int id;

		// Token: 0x0400478F RID: 18319
		public int value1;
	}
}
