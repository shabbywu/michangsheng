using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BEB RID: 3051
	public class ItemsSeidJsonData28 : IJSONClass
	{
		// Token: 0x06004B14 RID: 19220 RVA: 0x001FBB20 File Offset: 0x001F9D20
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[28].list)
			{
				try
				{
					ItemsSeidJsonData28 itemsSeidJsonData = new ItemsSeidJsonData28();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (ItemsSeidJsonData28.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData28.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData28.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData28.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData28.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData28.OnInitFinishAction != null)
			{
				ItemsSeidJsonData28.OnInitFinishAction();
			}
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400473F RID: 18239
		public static int SEIDID = 28;

		// Token: 0x04004740 RID: 18240
		public static Dictionary<int, ItemsSeidJsonData28> DataDict = new Dictionary<int, ItemsSeidJsonData28>();

		// Token: 0x04004741 RID: 18241
		public static List<ItemsSeidJsonData28> DataList = new List<ItemsSeidJsonData28>();

		// Token: 0x04004742 RID: 18242
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData28.OnInitFinish);

		// Token: 0x04004743 RID: 18243
		public int id;

		// Token: 0x04004744 RID: 18244
		public List<int> value1 = new List<int>();
	}
}
