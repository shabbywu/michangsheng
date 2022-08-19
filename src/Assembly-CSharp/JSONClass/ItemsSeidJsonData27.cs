using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200085B RID: 2139
	public class ItemsSeidJsonData27 : IJSONClass
	{
		// Token: 0x06003F7E RID: 16254 RVA: 0x001B1A08 File Offset: 0x001AFC08
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[27].list)
			{
				try
				{
					ItemsSeidJsonData27 itemsSeidJsonData = new ItemsSeidJsonData27();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					itemsSeidJsonData.value3 = jsonobject["value3"].Str;
					if (ItemsSeidJsonData27.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData27.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData27.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData27.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData27.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData27.OnInitFinishAction != null)
			{
				ItemsSeidJsonData27.OnInitFinishAction();
			}
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BD9 RID: 15321
		public static int SEIDID = 27;

		// Token: 0x04003BDA RID: 15322
		public static Dictionary<int, ItemsSeidJsonData27> DataDict = new Dictionary<int, ItemsSeidJsonData27>();

		// Token: 0x04003BDB RID: 15323
		public static List<ItemsSeidJsonData27> DataList = new List<ItemsSeidJsonData27>();

		// Token: 0x04003BDC RID: 15324
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData27.OnInitFinish);

		// Token: 0x04003BDD RID: 15325
		public int id;

		// Token: 0x04003BDE RID: 15326
		public int value1;

		// Token: 0x04003BDF RID: 15327
		public int value2;

		// Token: 0x04003BE0 RID: 15328
		public string value3;
	}
}
