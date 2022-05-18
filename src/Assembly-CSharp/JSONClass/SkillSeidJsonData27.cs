using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C9E RID: 3230
	public class SkillSeidJsonData27 : IJSONClass
	{
		// Token: 0x06004DE0 RID: 19936 RVA: 0x0020CD60 File Offset: 0x0020AF60
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[27].list)
			{
				try
				{
					SkillSeidJsonData27 skillSeidJsonData = new SkillSeidJsonData27();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData27.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData27.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData27.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData27.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData27.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData27.OnInitFinishAction != null)
			{
				SkillSeidJsonData27.OnInitFinishAction();
			}
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D98 RID: 19864
		public static int SEIDID = 27;

		// Token: 0x04004D99 RID: 19865
		public static Dictionary<int, SkillSeidJsonData27> DataDict = new Dictionary<int, SkillSeidJsonData27>();

		// Token: 0x04004D9A RID: 19866
		public static List<SkillSeidJsonData27> DataList = new List<SkillSeidJsonData27>();

		// Token: 0x04004D9B RID: 19867
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData27.OnInitFinish);

		// Token: 0x04004D9C RID: 19868
		public int skillid;

		// Token: 0x04004D9D RID: 19869
		public int value1;
	}
}
