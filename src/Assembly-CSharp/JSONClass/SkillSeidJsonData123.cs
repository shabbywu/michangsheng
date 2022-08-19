using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F9 RID: 2297
	public class SkillSeidJsonData123 : IJSONClass
	{
		// Token: 0x060041F7 RID: 16887 RVA: 0x001C3220 File Offset: 0x001C1420
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[123].list)
			{
				try
				{
					SkillSeidJsonData123 skillSeidJsonData = new SkillSeidJsonData123();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData123.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData123.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData123.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData123.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData123.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData123.OnInitFinishAction != null)
			{
				SkillSeidJsonData123.OnInitFinishAction();
			}
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041A5 RID: 16805
		public static int SEIDID = 123;

		// Token: 0x040041A6 RID: 16806
		public static Dictionary<int, SkillSeidJsonData123> DataDict = new Dictionary<int, SkillSeidJsonData123>();

		// Token: 0x040041A7 RID: 16807
		public static List<SkillSeidJsonData123> DataList = new List<SkillSeidJsonData123>();

		// Token: 0x040041A8 RID: 16808
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData123.OnInitFinish);

		// Token: 0x040041A9 RID: 16809
		public int skillid;

		// Token: 0x040041AA RID: 16810
		public List<int> value1 = new List<int>();

		// Token: 0x040041AB RID: 16811
		public List<int> value2 = new List<int>();
	}
}
