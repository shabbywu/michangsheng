using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE3 RID: 3043
	public class ItemsSeidJsonData17 : IJSONClass
	{
		// Token: 0x06004AF4 RID: 19188 RVA: 0x001FB18C File Offset: 0x001F938C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[17].list)
			{
				try
				{
					ItemsSeidJsonData17 itemsSeidJsonData = new ItemsSeidJsonData17();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (ItemsSeidJsonData17.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData17.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData17.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData17.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData17.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData17.OnInitFinishAction != null)
			{
				ItemsSeidJsonData17.OnInitFinishAction();
			}
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400470B RID: 18187
		public static int SEIDID = 17;

		// Token: 0x0400470C RID: 18188
		public static Dictionary<int, ItemsSeidJsonData17> DataDict = new Dictionary<int, ItemsSeidJsonData17>();

		// Token: 0x0400470D RID: 18189
		public static List<ItemsSeidJsonData17> DataList = new List<ItemsSeidJsonData17>();

		// Token: 0x0400470E RID: 18190
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData17.OnInitFinish);

		// Token: 0x0400470F RID: 18191
		public int id;

		// Token: 0x04004710 RID: 18192
		public List<int> value1 = new List<int>();
	}
}
