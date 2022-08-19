using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200082E RID: 2094
	public class DisableExchangeData : IJSONClass
	{
		// Token: 0x06003ECA RID: 16074 RVA: 0x001AD3EC File Offset: 0x001AB5EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DisableExchangeData.list)
			{
				try
				{
					DisableExchangeData disableExchangeData = new DisableExchangeData();
					disableExchangeData.id = jsonobject["id"].I;
					if (DisableExchangeData.DataDict.ContainsKey(disableExchangeData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DisableExchangeData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", disableExchangeData.id));
					}
					else
					{
						DisableExchangeData.DataDict.Add(disableExchangeData.id, disableExchangeData);
						DisableExchangeData.DataList.Add(disableExchangeData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DisableExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DisableExchangeData.OnInitFinishAction != null)
			{
				DisableExchangeData.OnInitFinishAction();
			}
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A81 RID: 14977
		public static Dictionary<int, DisableExchangeData> DataDict = new Dictionary<int, DisableExchangeData>();

		// Token: 0x04003A82 RID: 14978
		public static List<DisableExchangeData> DataList = new List<DisableExchangeData>();

		// Token: 0x04003A83 RID: 14979
		public static Action OnInitFinishAction = new Action(DisableExchangeData.OnInitFinish);

		// Token: 0x04003A84 RID: 14980
		public int id;
	}
}
