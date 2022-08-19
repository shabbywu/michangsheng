using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008DB RID: 2267
	public class ShengWangShangJinData : IJSONClass
	{
		// Token: 0x0600417F RID: 16767 RVA: 0x001C06D8 File Offset: 0x001BE8D8
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

		// Token: 0x06004180 RID: 16768 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040DC RID: 16604
		public static Dictionary<int, ShengWangShangJinData> DataDict = new Dictionary<int, ShengWangShangJinData>();

		// Token: 0x040040DD RID: 16605
		public static List<ShengWangShangJinData> DataList = new List<ShengWangShangJinData>();

		// Token: 0x040040DE RID: 16606
		public static Action OnInitFinishAction = new Action(ShengWangShangJinData.OnInitFinish);

		// Token: 0x040040DF RID: 16607
		public int id;

		// Token: 0x040040E0 RID: 16608
		public int ShengWang;

		// Token: 0x040040E1 RID: 16609
		public int ShiJiShangJin;
	}
}
