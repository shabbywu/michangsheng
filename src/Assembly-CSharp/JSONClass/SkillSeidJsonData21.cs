using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000918 RID: 2328
	public class SkillSeidJsonData21 : IJSONClass
	{
		// Token: 0x06004272 RID: 17010 RVA: 0x001C5F58 File Offset: 0x001C4158
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[21].list)
			{
				try
				{
					SkillSeidJsonData21 skillSeidJsonData = new SkillSeidJsonData21();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData21.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData21.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData21.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData21.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData21.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData21.OnInitFinishAction != null)
			{
				SkillSeidJsonData21.OnInitFinishAction();
			}
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400427C RID: 17020
		public static int SEIDID = 21;

		// Token: 0x0400427D RID: 17021
		public static Dictionary<int, SkillSeidJsonData21> DataDict = new Dictionary<int, SkillSeidJsonData21>();

		// Token: 0x0400427E RID: 17022
		public static List<SkillSeidJsonData21> DataList = new List<SkillSeidJsonData21>();

		// Token: 0x0400427F RID: 17023
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData21.OnInitFinish);

		// Token: 0x04004280 RID: 17024
		public int skillid;

		// Token: 0x04004281 RID: 17025
		public int value1;
	}
}
