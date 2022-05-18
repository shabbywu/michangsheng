using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BED RID: 3053
	public class ItemsSeidJsonData3 : IJSONClass
	{
		// Token: 0x06004B1C RID: 19228 RVA: 0x001FBD84 File Offset: 0x001F9F84
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[3].list)
			{
				try
				{
					ItemsSeidJsonData3 itemsSeidJsonData = new ItemsSeidJsonData3();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData3.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData3.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData3.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData3.OnInitFinishAction != null)
			{
				ItemsSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400474C RID: 18252
		public static int SEIDID = 3;

		// Token: 0x0400474D RID: 18253
		public static Dictionary<int, ItemsSeidJsonData3> DataDict = new Dictionary<int, ItemsSeidJsonData3>();

		// Token: 0x0400474E RID: 18254
		public static List<ItemsSeidJsonData3> DataList = new List<ItemsSeidJsonData3>();

		// Token: 0x0400474F RID: 18255
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData3.OnInitFinish);

		// Token: 0x04004750 RID: 18256
		public int id;

		// Token: 0x04004751 RID: 18257
		public int value1;
	}
}
