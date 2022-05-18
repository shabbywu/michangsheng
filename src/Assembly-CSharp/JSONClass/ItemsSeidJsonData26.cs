using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE9 RID: 3049
	public class ItemsSeidJsonData26 : IJSONClass
	{
		// Token: 0x06004B0C RID: 19212 RVA: 0x001FB8A4 File Offset: 0x001F9AA4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[26].list)
			{
				try
				{
					ItemsSeidJsonData26 itemsSeidJsonData = new ItemsSeidJsonData26();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData26.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData26.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData26.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData26.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData26.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData26.OnInitFinishAction != null)
			{
				ItemsSeidJsonData26.OnInitFinishAction();
			}
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004731 RID: 18225
		public static int SEIDID = 26;

		// Token: 0x04004732 RID: 18226
		public static Dictionary<int, ItemsSeidJsonData26> DataDict = new Dictionary<int, ItemsSeidJsonData26>();

		// Token: 0x04004733 RID: 18227
		public static List<ItemsSeidJsonData26> DataList = new List<ItemsSeidJsonData26>();

		// Token: 0x04004734 RID: 18228
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData26.OnInitFinish);

		// Token: 0x04004735 RID: 18229
		public int id;

		// Token: 0x04004736 RID: 18230
		public int value1;
	}
}
