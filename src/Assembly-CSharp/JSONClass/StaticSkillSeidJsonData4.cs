using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE8 RID: 3304
	public class StaticSkillSeidJsonData4 : IJSONClass
	{
		// Token: 0x06004F08 RID: 20232 RVA: 0x002128C8 File Offset: 0x00210AC8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[4].list)
			{
				try
				{
					StaticSkillSeidJsonData4 staticSkillSeidJsonData = new StaticSkillSeidJsonData4();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData4.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData4.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData4.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData4.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F95 RID: 20373
		public static int SEIDID = 4;

		// Token: 0x04004F96 RID: 20374
		public static Dictionary<int, StaticSkillSeidJsonData4> DataDict = new Dictionary<int, StaticSkillSeidJsonData4>();

		// Token: 0x04004F97 RID: 20375
		public static List<StaticSkillSeidJsonData4> DataList = new List<StaticSkillSeidJsonData4>();

		// Token: 0x04004F98 RID: 20376
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData4.OnInitFinish);

		// Token: 0x04004F99 RID: 20377
		public int skillid;

		// Token: 0x04004F9A RID: 20378
		public int value1;
	}
}
