using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000949 RID: 2377
	public class SkillSeidJsonData79 : IJSONClass
	{
		// Token: 0x06004336 RID: 17206 RVA: 0x001CA50C File Offset: 0x001C870C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[79].list)
			{
				try
				{
					SkillSeidJsonData79 skillSeidJsonData = new SkillSeidJsonData79();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData79.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData79.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData79.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData79.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData79.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData79.OnInitFinishAction != null)
			{
				SkillSeidJsonData79.OnInitFinishAction();
			}
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043C5 RID: 17349
		public static int SEIDID = 79;

		// Token: 0x040043C6 RID: 17350
		public static Dictionary<int, SkillSeidJsonData79> DataDict = new Dictionary<int, SkillSeidJsonData79>();

		// Token: 0x040043C7 RID: 17351
		public static List<SkillSeidJsonData79> DataList = new List<SkillSeidJsonData79>();

		// Token: 0x040043C8 RID: 17352
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData79.OnInitFinish);

		// Token: 0x040043C9 RID: 17353
		public int skillid;

		// Token: 0x040043CA RID: 17354
		public List<int> value1 = new List<int>();

		// Token: 0x040043CB RID: 17355
		public List<int> value2 = new List<int>();
	}
}
