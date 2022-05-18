using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C89 RID: 3209
	public class SkillSeidJsonData145 : IJSONClass
	{
		// Token: 0x06004D8D RID: 19853 RVA: 0x0020B29C File Offset: 0x0020949C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[145].list)
			{
				try
				{
					SkillSeidJsonData145 skillSeidJsonData = new SkillSeidJsonData145();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData145.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData145.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData145.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData145.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData145.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData145.OnInitFinishAction != null)
			{
				SkillSeidJsonData145.OnInitFinishAction();
			}
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CFF RID: 19711
		public static int SEIDID = 145;

		// Token: 0x04004D00 RID: 19712
		public static Dictionary<int, SkillSeidJsonData145> DataDict = new Dictionary<int, SkillSeidJsonData145>();

		// Token: 0x04004D01 RID: 19713
		public static List<SkillSeidJsonData145> DataList = new List<SkillSeidJsonData145>();

		// Token: 0x04004D02 RID: 19714
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData145.OnInitFinish);

		// Token: 0x04004D03 RID: 19715
		public int skillid;

		// Token: 0x04004D04 RID: 19716
		public int value1;
	}
}
