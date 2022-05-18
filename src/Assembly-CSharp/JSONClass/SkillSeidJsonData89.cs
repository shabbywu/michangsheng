using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD8 RID: 3288
	public class SkillSeidJsonData89 : IJSONClass
	{
		// Token: 0x06004EC8 RID: 20168 RVA: 0x00211400 File Offset: 0x0020F600
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[89].list)
			{
				try
				{
					SkillSeidJsonData89 skillSeidJsonData = new SkillSeidJsonData89();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData89.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData89.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData89.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData89.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData89.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData89.OnInitFinishAction != null)
			{
				SkillSeidJsonData89.OnInitFinishAction();
			}
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F1F RID: 20255
		public static int SEIDID = 89;

		// Token: 0x04004F20 RID: 20256
		public static Dictionary<int, SkillSeidJsonData89> DataDict = new Dictionary<int, SkillSeidJsonData89>();

		// Token: 0x04004F21 RID: 20257
		public static List<SkillSeidJsonData89> DataList = new List<SkillSeidJsonData89>();

		// Token: 0x04004F22 RID: 20258
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData89.OnInitFinish);

		// Token: 0x04004F23 RID: 20259
		public int skillid;

		// Token: 0x04004F24 RID: 20260
		public int value1;
	}
}
