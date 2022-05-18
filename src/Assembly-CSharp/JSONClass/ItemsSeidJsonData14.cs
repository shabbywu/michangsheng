using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE1 RID: 3041
	public class ItemsSeidJsonData14 : IJSONClass
	{
		// Token: 0x06004AEC RID: 19180 RVA: 0x001FAF28 File Offset: 0x001F9128
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[14].list)
			{
				try
				{
					ItemsSeidJsonData14 itemsSeidJsonData = new ItemsSeidJsonData14();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData14.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData14.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData14.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData14.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData14.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData14.OnInitFinishAction != null)
			{
				ItemsSeidJsonData14.OnInitFinishAction();
			}
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046FE RID: 18174
		public static int SEIDID = 14;

		// Token: 0x040046FF RID: 18175
		public static Dictionary<int, ItemsSeidJsonData14> DataDict = new Dictionary<int, ItemsSeidJsonData14>();

		// Token: 0x04004700 RID: 18176
		public static List<ItemsSeidJsonData14> DataList = new List<ItemsSeidJsonData14>();

		// Token: 0x04004701 RID: 18177
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData14.OnInitFinish);

		// Token: 0x04004702 RID: 18178
		public int id;

		// Token: 0x04004703 RID: 18179
		public int value1;
	}
}
