using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C67 RID: 3175
	public class ShengWangLevelData : IJSONClass
	{
		// Token: 0x06004D05 RID: 19717 RVA: 0x002088C4 File Offset: 0x00206AC4
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

		// Token: 0x06004D06 RID: 19718 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C20 RID: 19488
		public static Dictionary<int, ShengWangLevelData> DataDict = new Dictionary<int, ShengWangLevelData>();

		// Token: 0x04004C21 RID: 19489
		public static List<ShengWangLevelData> DataList = new List<ShengWangLevelData>();

		// Token: 0x04004C22 RID: 19490
		public static Action OnInitFinishAction = new Action(ShengWangLevelData.OnInitFinish);

		// Token: 0x04004C23 RID: 19491
		public int id;

		// Token: 0x04004C24 RID: 19492
		public string ShengWang;

		// Token: 0x04004C25 RID: 19493
		public List<int> ShengWangQuJian = new List<int>();
	}
}
