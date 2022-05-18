using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BDD RID: 3037
	public class ItemsSeidJsonData10 : IJSONClass
	{
		// Token: 0x06004ADC RID: 19164 RVA: 0x001FAA74 File Offset: 0x001F8C74
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[10].list)
			{
				try
				{
					ItemsSeidJsonData10 itemsSeidJsonData = new ItemsSeidJsonData10();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData10.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData10.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData10.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData10.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData10.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData10.OnInitFinishAction != null)
			{
				ItemsSeidJsonData10.OnInitFinishAction();
			}
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046E5 RID: 18149
		public static int SEIDID = 10;

		// Token: 0x040046E6 RID: 18150
		public static Dictionary<int, ItemsSeidJsonData10> DataDict = new Dictionary<int, ItemsSeidJsonData10>();

		// Token: 0x040046E7 RID: 18151
		public static List<ItemsSeidJsonData10> DataList = new List<ItemsSeidJsonData10>();

		// Token: 0x040046E8 RID: 18152
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData10.OnInitFinish);

		// Token: 0x040046E9 RID: 18153
		public int id;

		// Token: 0x040046EA RID: 18154
		public int value1;
	}
}
