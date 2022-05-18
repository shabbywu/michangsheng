using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C5A RID: 3162
	public class QieCuoJsonData : IJSONClass
	{
		// Token: 0x06004CD1 RID: 19665 RVA: 0x00207668 File Offset: 0x00205868
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

		// Token: 0x06004CD2 RID: 19666 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BBD RID: 19389
		public static Dictionary<int, QieCuoJsonData> DataDict = new Dictionary<int, QieCuoJsonData>();

		// Token: 0x04004BBE RID: 19390
		public static List<QieCuoJsonData> DataList = new List<QieCuoJsonData>();

		// Token: 0x04004BBF RID: 19391
		public static Action OnInitFinishAction = new Action(QieCuoJsonData.OnInitFinish);

		// Token: 0x04004BC0 RID: 19392
		public int id;

		// Token: 0x04004BC1 RID: 19393
		public int AvatarID;

		// Token: 0x04004BC2 RID: 19394
		public int tisheng;

		// Token: 0x04004BC3 RID: 19395
		public string name;

		// Token: 0x04004BC4 RID: 19396
		public string lose;

		// Token: 0x04004BC5 RID: 19397
		public string win;

		// Token: 0x04004BC6 RID: 19398
		public string jieshou;
	}
}
