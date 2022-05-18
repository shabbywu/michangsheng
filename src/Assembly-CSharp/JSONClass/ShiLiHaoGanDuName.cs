using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C69 RID: 3177
	public class ShiLiHaoGanDuName : IJSONClass
	{
		// Token: 0x06004D0D RID: 19725 RVA: 0x00208B3C File Offset: 0x00206D3C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShiLiHaoGanDuName.list)
			{
				try
				{
					ShiLiHaoGanDuName shiLiHaoGanDuName = new ShiLiHaoGanDuName();
					shiLiHaoGanDuName.id = jsonobject["id"].I;
					shiLiHaoGanDuName.ChinaText = jsonobject["ChinaText"].Str;
					if (ShiLiHaoGanDuName.DataDict.ContainsKey(shiLiHaoGanDuName.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShiLiHaoGanDuName.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shiLiHaoGanDuName.id));
					}
					else
					{
						ShiLiHaoGanDuName.DataDict.Add(shiLiHaoGanDuName.id, shiLiHaoGanDuName);
						ShiLiHaoGanDuName.DataList.Add(shiLiHaoGanDuName);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShiLiHaoGanDuName.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShiLiHaoGanDuName.OnInitFinishAction != null)
			{
				ShiLiHaoGanDuName.OnInitFinishAction();
			}
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C2C RID: 19500
		public static Dictionary<int, ShiLiHaoGanDuName> DataDict = new Dictionary<int, ShiLiHaoGanDuName>();

		// Token: 0x04004C2D RID: 19501
		public static List<ShiLiHaoGanDuName> DataList = new List<ShiLiHaoGanDuName>();

		// Token: 0x04004C2E RID: 19502
		public static Action OnInitFinishAction = new Action(ShiLiHaoGanDuName.OnInitFinish);

		// Token: 0x04004C2F RID: 19503
		public int id;

		// Token: 0x04004C30 RID: 19504
		public string ChinaText;
	}
}
