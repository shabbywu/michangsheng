using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008CC RID: 2252
	public class QieCuoJsonData : IJSONClass
	{
		// Token: 0x06004143 RID: 16707 RVA: 0x001BEE84 File Offset: 0x001BD084
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.QieCuoJsonData.list)
			{
				try
				{
					QieCuoJsonData qieCuoJsonData = new QieCuoJsonData();
					qieCuoJsonData.id = jsonobject["id"].I;
					qieCuoJsonData.AvatarID = jsonobject["AvatarID"].I;
					qieCuoJsonData.tisheng = jsonobject["tisheng"].I;
					qieCuoJsonData.name = jsonobject["name"].Str;
					qieCuoJsonData.lose = jsonobject["lose"].Str;
					qieCuoJsonData.win = jsonobject["win"].Str;
					qieCuoJsonData.jieshou = jsonobject["jieshou"].Str;
					if (QieCuoJsonData.DataDict.ContainsKey(qieCuoJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典QieCuoJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", qieCuoJsonData.id));
					}
					else
					{
						QieCuoJsonData.DataDict.Add(qieCuoJsonData.id, qieCuoJsonData);
						QieCuoJsonData.DataList.Add(qieCuoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典QieCuoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (QieCuoJsonData.OnInitFinishAction != null)
			{
				QieCuoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004069 RID: 16489
		public static Dictionary<int, QieCuoJsonData> DataDict = new Dictionary<int, QieCuoJsonData>();

		// Token: 0x0400406A RID: 16490
		public static List<QieCuoJsonData> DataList = new List<QieCuoJsonData>();

		// Token: 0x0400406B RID: 16491
		public static Action OnInitFinishAction = new Action(QieCuoJsonData.OnInitFinish);

		// Token: 0x0400406C RID: 16492
		public int id;

		// Token: 0x0400406D RID: 16493
		public int AvatarID;

		// Token: 0x0400406E RID: 16494
		public int tisheng;

		// Token: 0x0400406F RID: 16495
		public string name;

		// Token: 0x04004070 RID: 16496
		public string lose;

		// Token: 0x04004071 RID: 16497
		public string win;

		// Token: 0x04004072 RID: 16498
		public string jieshou;
	}
}
