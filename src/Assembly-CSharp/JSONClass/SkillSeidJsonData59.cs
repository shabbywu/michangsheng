using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200093A RID: 2362
	public class SkillSeidJsonData59 : IJSONClass
	{
		// Token: 0x060042FA RID: 17146 RVA: 0x001C9008 File Offset: 0x001C7208
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[59].list)
			{
				try
				{
					SkillSeidJsonData59 skillSeidJsonData = new SkillSeidJsonData59();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData59.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData59.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData59.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData59.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData59.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData59.OnInitFinishAction != null)
			{
				SkillSeidJsonData59.OnInitFinishAction();
			}
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004363 RID: 17251
		public static int SEIDID = 59;

		// Token: 0x04004364 RID: 17252
		public static Dictionary<int, SkillSeidJsonData59> DataDict = new Dictionary<int, SkillSeidJsonData59>();

		// Token: 0x04004365 RID: 17253
		public static List<SkillSeidJsonData59> DataList = new List<SkillSeidJsonData59>();

		// Token: 0x04004366 RID: 17254
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData59.OnInitFinish);

		// Token: 0x04004367 RID: 17255
		public int skillid;

		// Token: 0x04004368 RID: 17256
		public int value1;
	}
}
