using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C0B RID: 3083
	public class LinGenZiZhiJsonData : IJSONClass
	{
		// Token: 0x06004B95 RID: 19349 RVA: 0x001FEAE4 File Offset: 0x001FCCE4
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

		// Token: 0x06004B96 RID: 19350 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004854 RID: 18516
		public static Dictionary<int, LinGenZiZhiJsonData> DataDict = new Dictionary<int, LinGenZiZhiJsonData>();

		// Token: 0x04004855 RID: 18517
		public static List<LinGenZiZhiJsonData> DataList = new List<LinGenZiZhiJsonData>();

		// Token: 0x04004856 RID: 18518
		public static Action OnInitFinishAction = new Action(LinGenZiZhiJsonData.OnInitFinish);

		// Token: 0x04004857 RID: 18519
		public int id;

		// Token: 0x04004858 RID: 18520
		public int qujian;

		// Token: 0x04004859 RID: 18521
		public string Title;
	}
}
