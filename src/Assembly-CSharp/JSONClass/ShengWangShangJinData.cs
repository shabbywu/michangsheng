using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C68 RID: 3176
	public class ShengWangShangJinData : IJSONClass
	{
		// Token: 0x06004D09 RID: 19721 RVA: 0x00208A00 File Offset: 0x00206C00
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShengWangShangJinData.list)
			{
				try
				{
					ShengWangShangJinData shengWangShangJinData = new ShengWangShangJinData();
					shengWangShangJinData.id = jsonobject["id"].I;
					shengWangShangJinData.ShengWang = jsonobject["ShengWang"].I;
					shengWangShangJinData.ShiJiShangJin = jsonobject["ShiJiShangJin"].I;
					if (ShengWangShangJinData.DataDict.ContainsKey(shengWangShangJinData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShengWangShangJinData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shengWangShangJinData.id));
					}
					else
					{
						ShengWangShangJinData.DataDict.Add(shengWangShangJinData.id, shengWangShangJinData);
						ShengWangShangJinData.DataList.Add(shengWangShangJinData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShengWangShangJinData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShengWangShangJinData.OnInitFinishAction != null)
			{
				ShengWangShangJinData.OnInitFinishAction();
			}
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C26 RID: 19494
		public static Dictionary<int, ShengWangShangJinData> DataDict = new Dictionary<int, ShengWangShangJinData>();

		// Token: 0x04004C27 RID: 19495
		public static List<ShengWangShangJinData> DataList = new List<ShengWangShangJinData>();

		// Token: 0x04004C28 RID: 19496
		public static Action OnInitFinishAction = new Action(ShengWangShangJinData.OnInitFinish);

		// Token: 0x04004C29 RID: 19497
		public int id;

		// Token: 0x04004C2A RID: 19498
		public int ShengWang;

		// Token: 0x04004C2B RID: 19499
		public int ShiJiShangJin;
	}
}
