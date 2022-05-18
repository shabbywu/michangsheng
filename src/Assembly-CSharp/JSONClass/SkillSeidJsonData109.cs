using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C78 RID: 3192
	public class SkillSeidJsonData109 : IJSONClass
	{
		// Token: 0x06004D49 RID: 19785 RVA: 0x00209E38 File Offset: 0x00208038
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[109].list)
			{
				try
				{
					SkillSeidJsonData109 skillSeidJsonData = new SkillSeidJsonData109();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData109.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData109.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData109.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData109.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData109.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData109.OnInitFinishAction != null)
			{
				SkillSeidJsonData109.OnInitFinishAction();
			}
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C91 RID: 19601
		public static int SEIDID = 109;

		// Token: 0x04004C92 RID: 19602
		public static Dictionary<int, SkillSeidJsonData109> DataDict = new Dictionary<int, SkillSeidJsonData109>();

		// Token: 0x04004C93 RID: 19603
		public static List<SkillSeidJsonData109> DataList = new List<SkillSeidJsonData109>();

		// Token: 0x04004C94 RID: 19604
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData109.OnInitFinish);

		// Token: 0x04004C95 RID: 19605
		public int skillid;

		// Token: 0x04004C96 RID: 19606
		public int value1;
	}
}
