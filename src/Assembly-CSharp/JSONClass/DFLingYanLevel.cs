using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC2 RID: 3010
	public class DFLingYanLevel : IJSONClass
	{
		// Token: 0x06004A70 RID: 19056 RVA: 0x001F821C File Offset: 0x001F641C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DFLingYanLevel.list)
			{
				try
				{
					DFLingYanLevel dflingYanLevel = new DFLingYanLevel();
					dflingYanLevel.id = jsonobject["id"].I;
					dflingYanLevel.xiuliansudu = jsonobject["xiuliansudu"].I;
					dflingYanLevel.lingtiansudu = jsonobject["lingtiansudu"].I;
					dflingYanLevel.name = jsonobject["name"].Str;
					if (DFLingYanLevel.DataDict.ContainsKey(dflingYanLevel.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DFLingYanLevel.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", dflingYanLevel.id));
					}
					else
					{
						DFLingYanLevel.DataDict.Add(dflingYanLevel.id, dflingYanLevel);
						DFLingYanLevel.DataList.Add(dflingYanLevel);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DFLingYanLevel.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DFLingYanLevel.OnInitFinishAction != null)
			{
				DFLingYanLevel.OnInitFinishAction();
			}
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045F7 RID: 17911
		public static Dictionary<int, DFLingYanLevel> DataDict = new Dictionary<int, DFLingYanLevel>();

		// Token: 0x040045F8 RID: 17912
		public static List<DFLingYanLevel> DataList = new List<DFLingYanLevel>();

		// Token: 0x040045F9 RID: 17913
		public static Action OnInitFinishAction = new Action(DFLingYanLevel.OnInitFinish);

		// Token: 0x040045FA RID: 17914
		public int id;

		// Token: 0x040045FB RID: 17915
		public int xiuliansudu;

		// Token: 0x040045FC RID: 17916
		public int lingtiansudu;

		// Token: 0x040045FD RID: 17917
		public string name;
	}
}
