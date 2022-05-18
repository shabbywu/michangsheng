using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE8 RID: 3048
	public class ItemsSeidJsonData25 : IJSONClass
	{
		// Token: 0x06004B08 RID: 19208 RVA: 0x001FB768 File Offset: 0x001F9968
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[25].list)
			{
				try
				{
					ItemsSeidJsonData25 itemsSeidJsonData = new ItemsSeidJsonData25();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData25.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData25.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData25.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData25.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData25.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData25.OnInitFinishAction != null)
			{
				ItemsSeidJsonData25.OnInitFinishAction();
			}
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400472A RID: 18218
		public static int SEIDID = 25;

		// Token: 0x0400472B RID: 18219
		public static Dictionary<int, ItemsSeidJsonData25> DataDict = new Dictionary<int, ItemsSeidJsonData25>();

		// Token: 0x0400472C RID: 18220
		public static List<ItemsSeidJsonData25> DataList = new List<ItemsSeidJsonData25>();

		// Token: 0x0400472D RID: 18221
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData25.OnInitFinish);

		// Token: 0x0400472E RID: 18222
		public int id;

		// Token: 0x0400472F RID: 18223
		public int value1;

		// Token: 0x04004730 RID: 18224
		public int value2;
	}
}
