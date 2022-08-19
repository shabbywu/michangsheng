using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000844 RID: 2116
	public class GuDingExchangeData : IJSONClass
	{
		// Token: 0x06003F22 RID: 16162 RVA: 0x001AF5C0 File Offset: 0x001AD7C0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.GuDingExchangeData.list)
			{
				try
				{
					GuDingExchangeData guDingExchangeData = new GuDingExchangeData();
					guDingExchangeData.id = jsonobject["id"].I;
					guDingExchangeData.ItemID = jsonobject["ItemID"].I;
					guDingExchangeData.fuhao = jsonobject["fuhao"].Str;
					guDingExchangeData.YiWuFlag = jsonobject["YiWuFlag"].ToList();
					guDingExchangeData.NumFlag = jsonobject["NumFlag"].ToList();
					guDingExchangeData.YiWuItem = jsonobject["YiWuItem"].ToList();
					guDingExchangeData.NumItem = jsonobject["NumItem"].ToList();
					guDingExchangeData.EventValue = jsonobject["EventValue"].ToList();
					if (GuDingExchangeData.DataDict.ContainsKey(guDingExchangeData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典GuDingExchangeData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", guDingExchangeData.id));
					}
					else
					{
						GuDingExchangeData.DataDict.Add(guDingExchangeData.id, guDingExchangeData);
						GuDingExchangeData.DataList.Add(guDingExchangeData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典GuDingExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (GuDingExchangeData.OnInitFinishAction != null)
			{
				GuDingExchangeData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B21 RID: 15137
		public static Dictionary<int, GuDingExchangeData> DataDict = new Dictionary<int, GuDingExchangeData>();

		// Token: 0x04003B22 RID: 15138
		public static List<GuDingExchangeData> DataList = new List<GuDingExchangeData>();

		// Token: 0x04003B23 RID: 15139
		public static Action OnInitFinishAction = new Action(GuDingExchangeData.OnInitFinish);

		// Token: 0x04003B24 RID: 15140
		public int id;

		// Token: 0x04003B25 RID: 15141
		public int ItemID;

		// Token: 0x04003B26 RID: 15142
		public string fuhao;

		// Token: 0x04003B27 RID: 15143
		public List<int> YiWuFlag = new List<int>();

		// Token: 0x04003B28 RID: 15144
		public List<int> NumFlag = new List<int>();

		// Token: 0x04003B29 RID: 15145
		public List<int> YiWuItem = new List<int>();

		// Token: 0x04003B2A RID: 15146
		public List<int> NumItem = new List<int>();

		// Token: 0x04003B2B RID: 15147
		public List<int> EventValue = new List<int>();
	}
}
