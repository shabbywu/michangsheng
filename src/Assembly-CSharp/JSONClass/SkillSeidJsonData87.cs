using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD6 RID: 3286
	public class SkillSeidJsonData87 : IJSONClass
	{
		// Token: 0x06004EC0 RID: 20160 RVA: 0x00211188 File Offset: 0x0020F388
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[87].list)
			{
				try
				{
					SkillSeidJsonData87 skillSeidJsonData = new SkillSeidJsonData87();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData87.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData87.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData87.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData87.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData87.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData87.OnInitFinishAction != null)
			{
				SkillSeidJsonData87.OnInitFinishAction();
			}
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F11 RID: 20241
		public static int SEIDID = 87;

		// Token: 0x04004F12 RID: 20242
		public static Dictionary<int, SkillSeidJsonData87> DataDict = new Dictionary<int, SkillSeidJsonData87>();

		// Token: 0x04004F13 RID: 20243
		public static List<SkillSeidJsonData87> DataList = new List<SkillSeidJsonData87>();

		// Token: 0x04004F14 RID: 20244
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData87.OnInitFinish);

		// Token: 0x04004F15 RID: 20245
		public int skillid;

		// Token: 0x04004F16 RID: 20246
		public int value1;

		// Token: 0x04004F17 RID: 20247
		public int value2;
	}
}
