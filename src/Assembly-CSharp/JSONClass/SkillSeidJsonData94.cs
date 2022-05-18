using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CDD RID: 3293
	public class SkillSeidJsonData94 : IJSONClass
	{
		// Token: 0x06004EDC RID: 20188 RVA: 0x00211A04 File Offset: 0x0020FC04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[94].list)
			{
				try
				{
					SkillSeidJsonData94 skillSeidJsonData = new SkillSeidJsonData94();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData94.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData94.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData94.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData94.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData94.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData94.OnInitFinishAction != null)
			{
				SkillSeidJsonData94.OnInitFinishAction();
			}
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F40 RID: 20288
		public static int SEIDID = 94;

		// Token: 0x04004F41 RID: 20289
		public static Dictionary<int, SkillSeidJsonData94> DataDict = new Dictionary<int, SkillSeidJsonData94>();

		// Token: 0x04004F42 RID: 20290
		public static List<SkillSeidJsonData94> DataList = new List<SkillSeidJsonData94>();

		// Token: 0x04004F43 RID: 20291
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData94.OnInitFinish);

		// Token: 0x04004F44 RID: 20292
		public int skillid;

		// Token: 0x04004F45 RID: 20293
		public int value1;

		// Token: 0x04004F46 RID: 20294
		public int value2;
	}
}
