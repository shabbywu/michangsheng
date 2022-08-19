using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200093D RID: 2365
	public class SkillSeidJsonData62 : IJSONClass
	{
		// Token: 0x06004306 RID: 17158 RVA: 0x001C9410 File Offset: 0x001C7610
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[62].list)
			{
				try
				{
					SkillSeidJsonData62 skillSeidJsonData = new SkillSeidJsonData62();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData62.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData62.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData62.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData62.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData62.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData62.OnInitFinishAction != null)
			{
				SkillSeidJsonData62.OnInitFinishAction();
			}
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004375 RID: 17269
		public static int SEIDID = 62;

		// Token: 0x04004376 RID: 17270
		public static Dictionary<int, SkillSeidJsonData62> DataDict = new Dictionary<int, SkillSeidJsonData62>();

		// Token: 0x04004377 RID: 17271
		public static List<SkillSeidJsonData62> DataList = new List<SkillSeidJsonData62>();

		// Token: 0x04004378 RID: 17272
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData62.OnInitFinish);

		// Token: 0x04004379 RID: 17273
		public int skillid;

		// Token: 0x0400437A RID: 17274
		public int value1;
	}
}
