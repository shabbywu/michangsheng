using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BDE RID: 3038
	public class ItemsSeidJsonData11 : IJSONClass
	{
		// Token: 0x06004AE0 RID: 19168 RVA: 0x001FAB9C File Offset: 0x001F8D9C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[11].list)
			{
				try
				{
					ItemsSeidJsonData11 itemsSeidJsonData = new ItemsSeidJsonData11();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData11.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData11.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData11.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData11.OnInitFinishAction != null)
			{
				ItemsSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046EB RID: 18155
		public static int SEIDID = 11;

		// Token: 0x040046EC RID: 18156
		public static Dictionary<int, ItemsSeidJsonData11> DataDict = new Dictionary<int, ItemsSeidJsonData11>();

		// Token: 0x040046ED RID: 18157
		public static List<ItemsSeidJsonData11> DataList = new List<ItemsSeidJsonData11>();

		// Token: 0x040046EE RID: 18158
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData11.OnInitFinish);

		// Token: 0x040046EF RID: 18159
		public int id;

		// Token: 0x040046F0 RID: 18160
		public int value1;
	}
}
