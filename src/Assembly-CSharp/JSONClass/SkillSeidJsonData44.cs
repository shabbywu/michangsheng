using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200092C RID: 2348
	public class SkillSeidJsonData44 : IJSONClass
	{
		// Token: 0x060042C2 RID: 17090 RVA: 0x001C7B98 File Offset: 0x001C5D98
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[44].list)
			{
				try
				{
					SkillSeidJsonData44 skillSeidJsonData = new SkillSeidJsonData44();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData44.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData44.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData44.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData44.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData44.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData44.OnInitFinishAction != null)
			{
				SkillSeidJsonData44.OnInitFinishAction();
			}
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004300 RID: 17152
		public static int SEIDID = 44;

		// Token: 0x04004301 RID: 17153
		public static Dictionary<int, SkillSeidJsonData44> DataDict = new Dictionary<int, SkillSeidJsonData44>();

		// Token: 0x04004302 RID: 17154
		public static List<SkillSeidJsonData44> DataList = new List<SkillSeidJsonData44>();

		// Token: 0x04004303 RID: 17155
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData44.OnInitFinish);

		// Token: 0x04004304 RID: 17156
		public int skillid;

		// Token: 0x04004305 RID: 17157
		public int value1;

		// Token: 0x04004306 RID: 17158
		public int value2;
	}
}
