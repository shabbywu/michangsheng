using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000907 RID: 2311
	public class SkillSeidJsonData15 : IJSONClass
	{
		// Token: 0x0600422F RID: 16943 RVA: 0x001C469C File Offset: 0x001C289C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[15].list)
			{
				try
				{
					SkillSeidJsonData15 skillSeidJsonData = new SkillSeidJsonData15();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData15.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData15.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData15.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData15.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData15.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData15.OnInitFinishAction != null)
			{
				SkillSeidJsonData15.OnInitFinishAction();
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004204 RID: 16900
		public static int SEIDID = 15;

		// Token: 0x04004205 RID: 16901
		public static Dictionary<int, SkillSeidJsonData15> DataDict = new Dictionary<int, SkillSeidJsonData15>();

		// Token: 0x04004206 RID: 16902
		public static List<SkillSeidJsonData15> DataList = new List<SkillSeidJsonData15>();

		// Token: 0x04004207 RID: 16903
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData15.OnInitFinish);

		// Token: 0x04004208 RID: 16904
		public int skillid;

		// Token: 0x04004209 RID: 16905
		public int value1;

		// Token: 0x0400420A RID: 16906
		public int value2;
	}
}
