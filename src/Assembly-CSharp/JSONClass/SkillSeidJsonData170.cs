using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000915 RID: 2325
	public class SkillSeidJsonData170 : IJSONClass
	{
		// Token: 0x06004266 RID: 16998 RVA: 0x001C5B40 File Offset: 0x001C3D40
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[170].list)
			{
				try
				{
					SkillSeidJsonData170 skillSeidJsonData = new SkillSeidJsonData170();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData170.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData170.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData170.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData170.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData170.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData170.OnInitFinishAction != null)
			{
				SkillSeidJsonData170.OnInitFinishAction();
			}
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400426A RID: 17002
		public static int SEIDID = 170;

		// Token: 0x0400426B RID: 17003
		public static Dictionary<int, SkillSeidJsonData170> DataDict = new Dictionary<int, SkillSeidJsonData170>();

		// Token: 0x0400426C RID: 17004
		public static List<SkillSeidJsonData170> DataList = new List<SkillSeidJsonData170>();

		// Token: 0x0400426D RID: 17005
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData170.OnInitFinish);

		// Token: 0x0400426E RID: 17006
		public int skillid;

		// Token: 0x0400426F RID: 17007
		public int value1;
	}
}
