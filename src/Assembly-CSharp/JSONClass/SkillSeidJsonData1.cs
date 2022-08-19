using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E2 RID: 2274
	public class SkillSeidJsonData1 : IJSONClass
	{
		// Token: 0x0600419B RID: 16795 RVA: 0x001C10E8 File Offset: 0x001BF2E8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[1].list)
			{
				try
				{
					SkillSeidJsonData1 skillSeidJsonData = new SkillSeidJsonData1();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData1.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData1.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData1.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData1.OnInitFinishAction != null)
			{
				SkillSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004108 RID: 16648
		public static int SEIDID = 1;

		// Token: 0x04004109 RID: 16649
		public static Dictionary<int, SkillSeidJsonData1> DataDict = new Dictionary<int, SkillSeidJsonData1>();

		// Token: 0x0400410A RID: 16650
		public static List<SkillSeidJsonData1> DataList = new List<SkillSeidJsonData1>();

		// Token: 0x0400410B RID: 16651
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData1.OnInitFinish);

		// Token: 0x0400410C RID: 16652
		public int skillid;

		// Token: 0x0400410D RID: 16653
		public int value1;
	}
}
