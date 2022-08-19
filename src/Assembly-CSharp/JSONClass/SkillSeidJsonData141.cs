using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008FE RID: 2302
	public class SkillSeidJsonData141 : IJSONClass
	{
		// Token: 0x0600420B RID: 16907 RVA: 0x001C3934 File Offset: 0x001C1B34
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[141].list)
			{
				try
				{
					SkillSeidJsonData141 skillSeidJsonData = new SkillSeidJsonData141();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData141.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData141.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData141.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData141.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData141.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData141.OnInitFinishAction != null)
			{
				SkillSeidJsonData141.OnInitFinishAction();
			}
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041C5 RID: 16837
		public static int SEIDID = 141;

		// Token: 0x040041C6 RID: 16838
		public static Dictionary<int, SkillSeidJsonData141> DataDict = new Dictionary<int, SkillSeidJsonData141>();

		// Token: 0x040041C7 RID: 16839
		public static List<SkillSeidJsonData141> DataList = new List<SkillSeidJsonData141>();

		// Token: 0x040041C8 RID: 16840
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData141.OnInitFinish);

		// Token: 0x040041C9 RID: 16841
		public int skillid;

		// Token: 0x040041CA RID: 16842
		public int value1;
	}
}
