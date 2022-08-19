using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008DC RID: 2268
	public class ShiLiHaoGanDuName : IJSONClass
	{
		// Token: 0x06004183 RID: 16771 RVA: 0x001C083C File Offset: 0x001BEA3C
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

		// Token: 0x06004184 RID: 16772 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040E2 RID: 16610
		public static Dictionary<int, ShiLiHaoGanDuName> DataDict = new Dictionary<int, ShiLiHaoGanDuName>();

		// Token: 0x040040E3 RID: 16611
		public static List<ShiLiHaoGanDuName> DataList = new List<ShiLiHaoGanDuName>();

		// Token: 0x040040E4 RID: 16612
		public static Action OnInitFinishAction = new Action(ShiLiHaoGanDuName.OnInitFinish);

		// Token: 0x040040E5 RID: 16613
		public int id;

		// Token: 0x040040E6 RID: 16614
		public string ChinaText;
	}
}
