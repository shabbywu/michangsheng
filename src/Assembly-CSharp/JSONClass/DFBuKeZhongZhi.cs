using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC1 RID: 3009
	public class DFBuKeZhongZhi : IJSONClass
	{
		// Token: 0x06004A6C RID: 19052 RVA: 0x001F810C File Offset: 0x001F630C
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

		// Token: 0x06004A6D RID: 19053 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045F3 RID: 17907
		public static Dictionary<int, DFBuKeZhongZhi> DataDict = new Dictionary<int, DFBuKeZhongZhi>();

		// Token: 0x040045F4 RID: 17908
		public static List<DFBuKeZhongZhi> DataList = new List<DFBuKeZhongZhi>();

		// Token: 0x040045F5 RID: 17909
		public static Action OnInitFinishAction = new Action(DFBuKeZhongZhi.OnInitFinish);

		// Token: 0x040045F6 RID: 17910
		public int id;
	}
}
