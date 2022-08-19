using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200087D RID: 2173
	public class LinGenZiZhiJsonData : IJSONClass
	{
		// Token: 0x06004007 RID: 16391 RVA: 0x001B52BC File Offset: 0x001B34BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LinGenZiZhiJsonData.list)
			{
				try
				{
					LinGenZiZhiJsonData linGenZiZhiJsonData = new LinGenZiZhiJsonData();
					linGenZiZhiJsonData.id = jsonobject["id"].I;
					linGenZiZhiJsonData.qujian = jsonobject["qujian"].I;
					linGenZiZhiJsonData.Title = jsonobject["Title"].Str;
					if (LinGenZiZhiJsonData.DataDict.ContainsKey(linGenZiZhiJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LinGenZiZhiJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", linGenZiZhiJsonData.id));
					}
					else
					{
						LinGenZiZhiJsonData.DataDict.Add(linGenZiZhiJsonData.id, linGenZiZhiJsonData);
						LinGenZiZhiJsonData.DataList.Add(linGenZiZhiJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LinGenZiZhiJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LinGenZiZhiJsonData.OnInitFinishAction != null)
			{
				LinGenZiZhiJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003CFB RID: 15611
		public static Dictionary<int, LinGenZiZhiJsonData> DataDict = new Dictionary<int, LinGenZiZhiJsonData>();

		// Token: 0x04003CFC RID: 15612
		public static List<LinGenZiZhiJsonData> DataList = new List<LinGenZiZhiJsonData>();

		// Token: 0x04003CFD RID: 15613
		public static Action OnInitFinishAction = new Action(LinGenZiZhiJsonData.OnInitFinish);

		// Token: 0x04003CFE RID: 15614
		public int id;

		// Token: 0x04003CFF RID: 15615
		public int qujian;

		// Token: 0x04003D00 RID: 15616
		public string Title;
	}
}
