using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200082B RID: 2091
	public class DFBuKeZhongZhi : IJSONClass
	{
		// Token: 0x06003EBE RID: 16062 RVA: 0x001ACF54 File Offset: 0x001AB154
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DFBuKeZhongZhi.list)
			{
				try
				{
					DFBuKeZhongZhi dfbuKeZhongZhi = new DFBuKeZhongZhi();
					dfbuKeZhongZhi.id = jsonobject["id"].I;
					if (DFBuKeZhongZhi.DataDict.ContainsKey(dfbuKeZhongZhi.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DFBuKeZhongZhi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", dfbuKeZhongZhi.id));
					}
					else
					{
						DFBuKeZhongZhi.DataDict.Add(dfbuKeZhongZhi.id, dfbuKeZhongZhi);
						DFBuKeZhongZhi.DataList.Add(dfbuKeZhongZhi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DFBuKeZhongZhi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DFBuKeZhongZhi.OnInitFinishAction != null)
			{
				DFBuKeZhongZhi.OnInitFinishAction();
			}
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A6B RID: 14955
		public static Dictionary<int, DFBuKeZhongZhi> DataDict = new Dictionary<int, DFBuKeZhongZhi>();

		// Token: 0x04003A6C RID: 14956
		public static List<DFBuKeZhongZhi> DataList = new List<DFBuKeZhongZhi>();

		// Token: 0x04003A6D RID: 14957
		public static Action OnInitFinishAction = new Action(DFBuKeZhongZhi.OnInitFinish);

		// Token: 0x04003A6E RID: 14958
		public int id;
	}
}
