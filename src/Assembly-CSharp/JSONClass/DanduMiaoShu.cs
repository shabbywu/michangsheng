using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200082A RID: 2090
	public class DanduMiaoShu : IJSONClass
	{
		// Token: 0x06003EBA RID: 16058 RVA: 0x001ACDDC File Offset: 0x001AAFDC
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

		// Token: 0x06003EBB RID: 16059 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A64 RID: 14948
		public static Dictionary<int, DanduMiaoShu> DataDict = new Dictionary<int, DanduMiaoShu>();

		// Token: 0x04003A65 RID: 14949
		public static List<DanduMiaoShu> DataList = new List<DanduMiaoShu>();

		// Token: 0x04003A66 RID: 14950
		public static Action OnInitFinishAction = new Action(DanduMiaoShu.OnInitFinish);

		// Token: 0x04003A67 RID: 14951
		public int id;

		// Token: 0x04003A68 RID: 14952
		public int jiexianzhi;

		// Token: 0x04003A69 RID: 14953
		public string name;

		// Token: 0x04003A6A RID: 14954
		public string desc;
	}
}
