using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200091C RID: 2332
	public class SkillSeidJsonData3 : IJSONClass
	{
		// Token: 0x06004282 RID: 17026 RVA: 0x001C64B8 File Offset: 0x001C46B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[3].list)
			{
				try
				{
					SkillSeidJsonData3 skillSeidJsonData = new SkillSeidJsonData3();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData3.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData3.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData3.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData3.OnInitFinishAction != null)
			{
				SkillSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004294 RID: 17044
		public static int SEIDID = 3;

		// Token: 0x04004295 RID: 17045
		public static Dictionary<int, SkillSeidJsonData3> DataDict = new Dictionary<int, SkillSeidJsonData3>();

		// Token: 0x04004296 RID: 17046
		public static List<SkillSeidJsonData3> DataList = new List<SkillSeidJsonData3>();

		// Token: 0x04004297 RID: 17047
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData3.OnInitFinish);

		// Token: 0x04004298 RID: 17048
		public int skillid;

		// Token: 0x04004299 RID: 17049
		public List<int> value1 = new List<int>();

		// Token: 0x0400429A RID: 17050
		public List<int> value2 = new List<int>();
	}
}
