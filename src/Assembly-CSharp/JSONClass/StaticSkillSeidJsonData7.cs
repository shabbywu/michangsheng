using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000967 RID: 2407
	public class StaticSkillSeidJsonData7 : IJSONClass
	{
		// Token: 0x060043AE RID: 17326 RVA: 0x001CD0BC File Offset: 0x001CB2BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[7].list)
			{
				try
				{
					StaticSkillSeidJsonData7 staticSkillSeidJsonData = new StaticSkillSeidJsonData7();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData7.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData7.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData7.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData7.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004497 RID: 17559
		public static int SEIDID = 7;

		// Token: 0x04004498 RID: 17560
		public static Dictionary<int, StaticSkillSeidJsonData7> DataDict = new Dictionary<int, StaticSkillSeidJsonData7>();

		// Token: 0x04004499 RID: 17561
		public static List<StaticSkillSeidJsonData7> DataList = new List<StaticSkillSeidJsonData7>();

		// Token: 0x0400449A RID: 17562
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData7.OnInitFinish);

		// Token: 0x0400449B RID: 17563
		public int skillid;

		// Token: 0x0400449C RID: 17564
		public int value1;
	}
}
