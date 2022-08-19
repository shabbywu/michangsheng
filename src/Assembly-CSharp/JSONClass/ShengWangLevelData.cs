using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008DA RID: 2266
	public class ShengWangLevelData : IJSONClass
	{
		// Token: 0x0600417B RID: 16763 RVA: 0x001C0560 File Offset: 0x001BE760
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShengWangLevelData.list)
			{
				try
				{
					ShengWangLevelData shengWangLevelData = new ShengWangLevelData();
					shengWangLevelData.id = jsonobject["id"].I;
					shengWangLevelData.ShengWang = jsonobject["ShengWang"].Str;
					shengWangLevelData.ShengWangQuJian = jsonobject["ShengWangQuJian"].ToList();
					if (ShengWangLevelData.DataDict.ContainsKey(shengWangLevelData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShengWangLevelData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shengWangLevelData.id));
					}
					else
					{
						ShengWangLevelData.DataDict.Add(shengWangLevelData.id, shengWangLevelData);
						ShengWangLevelData.DataList.Add(shengWangLevelData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShengWangLevelData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShengWangLevelData.OnInitFinishAction != null)
			{
				ShengWangLevelData.OnInitFinishAction();
			}
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040D6 RID: 16598
		public static Dictionary<int, ShengWangLevelData> DataDict = new Dictionary<int, ShengWangLevelData>();

		// Token: 0x040040D7 RID: 16599
		public static List<ShengWangLevelData> DataList = new List<ShengWangLevelData>();

		// Token: 0x040040D8 RID: 16600
		public static Action OnInitFinishAction = new Action(ShengWangLevelData.OnInitFinish);

		// Token: 0x040040D9 RID: 16601
		public int id;

		// Token: 0x040040DA RID: 16602
		public string ShengWang;

		// Token: 0x040040DB RID: 16603
		public List<int> ShengWangQuJian = new List<int>();
	}
}
