using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008FD RID: 2301
	public class SkillSeidJsonData140 : IJSONClass
	{
		// Token: 0x06004207 RID: 16903 RVA: 0x001C37C4 File Offset: 0x001C19C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[140].list)
			{
				try
				{
					SkillSeidJsonData140 skillSeidJsonData = new SkillSeidJsonData140();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (SkillSeidJsonData140.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData140.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData140.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData140.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData140.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData140.OnInitFinishAction != null)
			{
				SkillSeidJsonData140.OnInitFinishAction();
			}
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041BF RID: 16831
		public static int SEIDID = 140;

		// Token: 0x040041C0 RID: 16832
		public static Dictionary<int, SkillSeidJsonData140> DataDict = new Dictionary<int, SkillSeidJsonData140>();

		// Token: 0x040041C1 RID: 16833
		public static List<SkillSeidJsonData140> DataList = new List<SkillSeidJsonData140>();

		// Token: 0x040041C2 RID: 16834
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData140.OnInitFinish);

		// Token: 0x040041C3 RID: 16835
		public int skillid;

		// Token: 0x040041C4 RID: 16836
		public List<int> value1 = new List<int>();
	}
}
