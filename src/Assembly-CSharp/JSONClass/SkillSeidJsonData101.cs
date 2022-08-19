using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E4 RID: 2276
	public class SkillSeidJsonData101 : IJSONClass
	{
		// Token: 0x060041A3 RID: 16803 RVA: 0x001C13C4 File Offset: 0x001BF5C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[101].list)
			{
				try
				{
					SkillSeidJsonData101 skillSeidJsonData = new SkillSeidJsonData101();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData101.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData101.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData101.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData101.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData101.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData101.OnInitFinishAction != null)
			{
				SkillSeidJsonData101.OnInitFinishAction();
			}
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004116 RID: 16662
		public static int SEIDID = 101;

		// Token: 0x04004117 RID: 16663
		public static Dictionary<int, SkillSeidJsonData101> DataDict = new Dictionary<int, SkillSeidJsonData101>();

		// Token: 0x04004118 RID: 16664
		public static List<SkillSeidJsonData101> DataList = new List<SkillSeidJsonData101>();

		// Token: 0x04004119 RID: 16665
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData101.OnInitFinish);

		// Token: 0x0400411A RID: 16666
		public int skillid;

		// Token: 0x0400411B RID: 16667
		public int value1;
	}
}
