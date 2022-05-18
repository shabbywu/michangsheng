using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF5 RID: 3061
	public class ItemsSeidJsonData7 : IJSONClass
	{
		// Token: 0x06004B3C RID: 19260 RVA: 0x001FC6EC File Offset: 0x001FA8EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[7].list)
			{
				try
				{
					ItemsSeidJsonData7 itemsSeidJsonData = new ItemsSeidJsonData7();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData7.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData7.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData7.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData7.OnInitFinishAction != null)
			{
				ItemsSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400477E RID: 18302
		public static int SEIDID = 7;

		// Token: 0x0400477F RID: 18303
		public static Dictionary<int, ItemsSeidJsonData7> DataDict = new Dictionary<int, ItemsSeidJsonData7>();

		// Token: 0x04004780 RID: 18304
		public static List<ItemsSeidJsonData7> DataList = new List<ItemsSeidJsonData7>();

		// Token: 0x04004781 RID: 18305
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData7.OnInitFinish);

		// Token: 0x04004782 RID: 18306
		public int id;

		// Token: 0x04004783 RID: 18307
		public int value1;
	}
}
