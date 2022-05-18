using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C6A RID: 3178
	public class ShiLiShenFenData : IJSONClass
	{
		// Token: 0x06004D11 RID: 19729 RVA: 0x00208C60 File Offset: 0x00206E60
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShiLiShenFenData.list)
			{
				try
				{
					ShiLiShenFenData shiLiShenFenData = new ShiLiShenFenData();
					shiLiShenFenData.id = jsonobject["id"].I;
					shiLiShenFenData.ShenFen = jsonobject["ShenFen"].I;
					shiLiShenFenData.ShiLi = jsonobject["ShiLi"].I;
					shiLiShenFenData.ZongMen = jsonobject["ZongMen"].Str;
					shiLiShenFenData.Name = jsonobject["Name"].Str;
					if (ShiLiShenFenData.DataDict.ContainsKey(shiLiShenFenData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShiLiShenFenData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shiLiShenFenData.id));
					}
					else
					{
						ShiLiShenFenData.DataDict.Add(shiLiShenFenData.id, shiLiShenFenData);
						ShiLiShenFenData.DataList.Add(shiLiShenFenData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShiLiShenFenData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShiLiShenFenData.OnInitFinishAction != null)
			{
				ShiLiShenFenData.OnInitFinishAction();
			}
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C31 RID: 19505
		public static Dictionary<int, ShiLiShenFenData> DataDict = new Dictionary<int, ShiLiShenFenData>();

		// Token: 0x04004C32 RID: 19506
		public static List<ShiLiShenFenData> DataList = new List<ShiLiShenFenData>();

		// Token: 0x04004C33 RID: 19507
		public static Action OnInitFinishAction = new Action(ShiLiShenFenData.OnInitFinish);

		// Token: 0x04004C34 RID: 19508
		public int id;

		// Token: 0x04004C35 RID: 19509
		public int ShenFen;

		// Token: 0x04004C36 RID: 19510
		public int ShiLi;

		// Token: 0x04004C37 RID: 19511
		public string ZongMen;

		// Token: 0x04004C38 RID: 19512
		public string Name;
	}
}
