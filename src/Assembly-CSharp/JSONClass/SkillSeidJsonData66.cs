using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC5 RID: 3269
	public class SkillSeidJsonData66 : IJSONClass
	{
		// Token: 0x06004E7C RID: 20092 RVA: 0x0020FCBC File Offset: 0x0020DEBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[66].list)
			{
				try
				{
					SkillSeidJsonData66 skillSeidJsonData = new SkillSeidJsonData66();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData66.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData66.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData66.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData66.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData66.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData66.OnInitFinishAction != null)
			{
				SkillSeidJsonData66.OnInitFinishAction();
			}
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E9E RID: 20126
		public static int SEIDID = 66;

		// Token: 0x04004E9F RID: 20127
		public static Dictionary<int, SkillSeidJsonData66> DataDict = new Dictionary<int, SkillSeidJsonData66>();

		// Token: 0x04004EA0 RID: 20128
		public static List<SkillSeidJsonData66> DataList = new List<SkillSeidJsonData66>();

		// Token: 0x04004EA1 RID: 20129
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData66.OnInitFinish);

		// Token: 0x04004EA2 RID: 20130
		public int skillid;

		// Token: 0x04004EA3 RID: 20131
		public int value1;
	}
}
