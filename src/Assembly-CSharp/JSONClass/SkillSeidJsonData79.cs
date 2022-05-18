using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CCD RID: 3277
	public class SkillSeidJsonData79 : IJSONClass
	{
		// Token: 0x06004E9C RID: 20124 RVA: 0x002106A8 File Offset: 0x0020E8A8
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

		// Token: 0x06004E9D RID: 20125 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004ED5 RID: 20181
		public static int SEIDID = 79;

		// Token: 0x04004ED6 RID: 20182
		public static Dictionary<int, SkillSeidJsonData79> DataDict = new Dictionary<int, SkillSeidJsonData79>();

		// Token: 0x04004ED7 RID: 20183
		public static List<SkillSeidJsonData79> DataList = new List<SkillSeidJsonData79>();

		// Token: 0x04004ED8 RID: 20184
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData79.OnInitFinish);

		// Token: 0x04004ED9 RID: 20185
		public int skillid;

		// Token: 0x04004EDA RID: 20186
		public List<int> value1 = new List<int>();

		// Token: 0x04004EDB RID: 20187
		public List<int> value2 = new List<int>();
	}
}
