using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000900 RID: 2304
	public class SkillSeidJsonData143 : IJSONClass
	{
		// Token: 0x06004213 RID: 16915 RVA: 0x001C3BF4 File Offset: 0x001C1DF4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[143].list)
			{
				try
				{
					SkillSeidJsonData143 skillSeidJsonData = new SkillSeidJsonData143();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData143.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData143.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData143.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData143.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData143.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData143.OnInitFinishAction != null)
			{
				SkillSeidJsonData143.OnInitFinishAction();
			}
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041D1 RID: 16849
		public static int SEIDID = 143;

		// Token: 0x040041D2 RID: 16850
		public static Dictionary<int, SkillSeidJsonData143> DataDict = new Dictionary<int, SkillSeidJsonData143>();

		// Token: 0x040041D3 RID: 16851
		public static List<SkillSeidJsonData143> DataList = new List<SkillSeidJsonData143>();

		// Token: 0x040041D4 RID: 16852
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData143.OnInitFinish);

		// Token: 0x040041D5 RID: 16853
		public int skillid;

		// Token: 0x040041D6 RID: 16854
		public int value1;
	}
}
