using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE5 RID: 3045
	public class ItemsSeidJsonData20 : IJSONClass
	{
		// Token: 0x06004AFC RID: 19196 RVA: 0x001FB3DC File Offset: 0x001F95DC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[20].list)
			{
				try
				{
					ItemsSeidJsonData20 itemsSeidJsonData = new ItemsSeidJsonData20();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					itemsSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (ItemsSeidJsonData20.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData20.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData20.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData20.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData20.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData20.OnInitFinishAction != null)
			{
				ItemsSeidJsonData20.OnInitFinishAction();
			}
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004717 RID: 18199
		public static int SEIDID = 20;

		// Token: 0x04004718 RID: 18200
		public static Dictionary<int, ItemsSeidJsonData20> DataDict = new Dictionary<int, ItemsSeidJsonData20>();

		// Token: 0x04004719 RID: 18201
		public static List<ItemsSeidJsonData20> DataList = new List<ItemsSeidJsonData20>();

		// Token: 0x0400471A RID: 18202
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData20.OnInitFinish);

		// Token: 0x0400471B RID: 18203
		public int id;

		// Token: 0x0400471C RID: 18204
		public List<int> value1 = new List<int>();

		// Token: 0x0400471D RID: 18205
		public List<int> value2 = new List<int>();
	}
}
