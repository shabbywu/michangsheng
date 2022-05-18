using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC0 RID: 3008
	public class DanduMiaoShu : IJSONClass
	{
		// Token: 0x06004A68 RID: 19048 RVA: 0x001F7FBC File Offset: 0x001F61BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DanduMiaoShu.list)
			{
				try
				{
					DanduMiaoShu danduMiaoShu = new DanduMiaoShu();
					danduMiaoShu.id = jsonobject["id"].I;
					danduMiaoShu.jiexianzhi = jsonobject["jiexianzhi"].I;
					danduMiaoShu.name = jsonobject["name"].Str;
					danduMiaoShu.desc = jsonobject["desc"].Str;
					if (DanduMiaoShu.DataDict.ContainsKey(danduMiaoShu.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DanduMiaoShu.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", danduMiaoShu.id));
					}
					else
					{
						DanduMiaoShu.DataDict.Add(danduMiaoShu.id, danduMiaoShu);
						DanduMiaoShu.DataList.Add(danduMiaoShu);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DanduMiaoShu.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DanduMiaoShu.OnInitFinishAction != null)
			{
				DanduMiaoShu.OnInitFinishAction();
			}
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045EC RID: 17900
		public static Dictionary<int, DanduMiaoShu> DataDict = new Dictionary<int, DanduMiaoShu>();

		// Token: 0x040045ED RID: 17901
		public static List<DanduMiaoShu> DataList = new List<DanduMiaoShu>();

		// Token: 0x040045EE RID: 17902
		public static Action OnInitFinishAction = new Action(DanduMiaoShu.OnInitFinish);

		// Token: 0x040045EF RID: 17903
		public int id;

		// Token: 0x040045F0 RID: 17904
		public int jiexianzhi;

		// Token: 0x040045F1 RID: 17905
		public string name;

		// Token: 0x040045F2 RID: 17906
		public string desc;
	}
}
