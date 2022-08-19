using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200082C RID: 2092
	public class DFLingYanLevel : IJSONClass
	{
		// Token: 0x06003EC2 RID: 16066 RVA: 0x001AD08C File Offset: 0x001AB28C
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

		// Token: 0x06003EC3 RID: 16067 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A6F RID: 14959
		public static Dictionary<int, DFLingYanLevel> DataDict = new Dictionary<int, DFLingYanLevel>();

		// Token: 0x04003A70 RID: 14960
		public static List<DFLingYanLevel> DataList = new List<DFLingYanLevel>();

		// Token: 0x04003A71 RID: 14961
		public static Action OnInitFinishAction = new Action(DFLingYanLevel.OnInitFinish);

		// Token: 0x04003A72 RID: 14962
		public int id;

		// Token: 0x04003A73 RID: 14963
		public int xiuliansudu;

		// Token: 0x04003A74 RID: 14964
		public int lingtiansudu;

		// Token: 0x04003A75 RID: 14965
		public string name;
	}
}
