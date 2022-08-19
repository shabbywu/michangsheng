using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008DD RID: 2269
	public class ShiLiShenFenData : IJSONClass
	{
		// Token: 0x06004187 RID: 16775 RVA: 0x001C0988 File Offset: 0x001BEB88
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

		// Token: 0x06004188 RID: 16776 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040E7 RID: 16615
		public static Dictionary<int, ShiLiShenFenData> DataDict = new Dictionary<int, ShiLiShenFenData>();

		// Token: 0x040040E8 RID: 16616
		public static List<ShiLiShenFenData> DataList = new List<ShiLiShenFenData>();

		// Token: 0x040040E9 RID: 16617
		public static Action OnInitFinishAction = new Action(ShiLiShenFenData.OnInitFinish);

		// Token: 0x040040EA RID: 16618
		public int id;

		// Token: 0x040040EB RID: 16619
		public int ShenFen;

		// Token: 0x040040EC RID: 16620
		public int ShiLi;

		// Token: 0x040040ED RID: 16621
		public string ZongMen;

		// Token: 0x040040EE RID: 16622
		public string Name;
	}
}
