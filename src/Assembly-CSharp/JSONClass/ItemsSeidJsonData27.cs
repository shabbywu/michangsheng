using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BEA RID: 3050
	public class ItemsSeidJsonData27 : IJSONClass
	{
		// Token: 0x06004B10 RID: 19216 RVA: 0x001FB9CC File Offset: 0x001F9BCC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[27].list)
			{
				try
				{
					ItemsSeidJsonData27 itemsSeidJsonData = new ItemsSeidJsonData27();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					itemsSeidJsonData.value3 = jsonobject["value3"].Str;
					if (ItemsSeidJsonData27.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData27.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData27.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData27.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData27.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData27.OnInitFinishAction != null)
			{
				ItemsSeidJsonData27.OnInitFinishAction();
			}
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004737 RID: 18231
		public static int SEIDID = 27;

		// Token: 0x04004738 RID: 18232
		public static Dictionary<int, ItemsSeidJsonData27> DataDict = new Dictionary<int, ItemsSeidJsonData27>();

		// Token: 0x04004739 RID: 18233
		public static List<ItemsSeidJsonData27> DataList = new List<ItemsSeidJsonData27>();

		// Token: 0x0400473A RID: 18234
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData27.OnInitFinish);

		// Token: 0x0400473B RID: 18235
		public int id;

		// Token: 0x0400473C RID: 18236
		public int value1;

		// Token: 0x0400473D RID: 18237
		public int value2;

		// Token: 0x0400473E RID: 18238
		public string value3;
	}
}
