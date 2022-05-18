using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C84 RID: 3204
	public class SkillSeidJsonData140 : IJSONClass
	{
		// Token: 0x06004D79 RID: 19833 RVA: 0x0020ACC0 File Offset: 0x00208EC0
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

		// Token: 0x06004D7A RID: 19834 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CE1 RID: 19681
		public static int SEIDID = 140;

		// Token: 0x04004CE2 RID: 19682
		public static Dictionary<int, SkillSeidJsonData140> DataDict = new Dictionary<int, SkillSeidJsonData140>();

		// Token: 0x04004CE3 RID: 19683
		public static List<SkillSeidJsonData140> DataList = new List<SkillSeidJsonData140>();

		// Token: 0x04004CE4 RID: 19684
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData140.OnInitFinish);

		// Token: 0x04004CE5 RID: 19685
		public int skillid;

		// Token: 0x04004CE6 RID: 19686
		public List<int> value1 = new List<int>();
	}
}
