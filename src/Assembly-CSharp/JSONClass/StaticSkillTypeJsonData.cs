using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200096A RID: 2410
	public class StaticSkillTypeJsonData : IJSONClass
	{
		// Token: 0x060043BA RID: 17338 RVA: 0x001CD534 File Offset: 0x001CB734
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillTypeJsonData.list)
			{
				try
				{
					StaticSkillTypeJsonData staticSkillTypeJsonData = new StaticSkillTypeJsonData();
					staticSkillTypeJsonData.id = jsonobject["id"].I;
					staticSkillTypeJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillTypeJsonData.DataDict.ContainsKey(staticSkillTypeJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillTypeJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillTypeJsonData.id));
					}
					else
					{
						StaticSkillTypeJsonData.DataDict.Add(staticSkillTypeJsonData.id, staticSkillTypeJsonData);
						StaticSkillTypeJsonData.DataList.Add(staticSkillTypeJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillTypeJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillTypeJsonData.OnInitFinishAction != null)
			{
				StaticSkillTypeJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044AD RID: 17581
		public static Dictionary<int, StaticSkillTypeJsonData> DataDict = new Dictionary<int, StaticSkillTypeJsonData>();

		// Token: 0x040044AE RID: 17582
		public static List<StaticSkillTypeJsonData> DataList = new List<StaticSkillTypeJsonData>();

		// Token: 0x040044AF RID: 17583
		public static Action OnInitFinishAction = new Action(StaticSkillTypeJsonData.OnInitFinish);

		// Token: 0x040044B0 RID: 17584
		public int id;

		// Token: 0x040044B1 RID: 17585
		public int value1;
	}
}
