using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BDF RID: 3039
	public class ItemsSeidJsonData12 : IJSONClass
	{
		// Token: 0x06004AE4 RID: 19172 RVA: 0x001FACC4 File Offset: 0x001F8EC4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[12].list)
			{
				try
				{
					ItemsSeidJsonData12 itemsSeidJsonData = new ItemsSeidJsonData12();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					itemsSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (ItemsSeidJsonData12.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData12.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData12.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData12.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData12.OnInitFinishAction != null)
			{
				ItemsSeidJsonData12.OnInitFinishAction();
			}
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046F1 RID: 18161
		public static int SEIDID = 12;

		// Token: 0x040046F2 RID: 18162
		public static Dictionary<int, ItemsSeidJsonData12> DataDict = new Dictionary<int, ItemsSeidJsonData12>();

		// Token: 0x040046F3 RID: 18163
		public static List<ItemsSeidJsonData12> DataList = new List<ItemsSeidJsonData12>();

		// Token: 0x040046F4 RID: 18164
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData12.OnInitFinish);

		// Token: 0x040046F5 RID: 18165
		public int id;

		// Token: 0x040046F6 RID: 18166
		public List<int> value1 = new List<int>();

		// Token: 0x040046F7 RID: 18167
		public List<int> value2 = new List<int>();
	}
}
