using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000945 RID: 2373
	public class SkillSeidJsonData74 : IJSONClass
	{
		// Token: 0x06004326 RID: 17190 RVA: 0x001C9F98 File Offset: 0x001C8198
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[74].list)
			{
				try
				{
					SkillSeidJsonData74 skillSeidJsonData = new SkillSeidJsonData74();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData74.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData74.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData74.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData74.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData74.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData74.OnInitFinishAction != null)
			{
				SkillSeidJsonData74.OnInitFinishAction();
			}
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043AC RID: 17324
		public static int SEIDID = 74;

		// Token: 0x040043AD RID: 17325
		public static Dictionary<int, SkillSeidJsonData74> DataDict = new Dictionary<int, SkillSeidJsonData74>();

		// Token: 0x040043AE RID: 17326
		public static List<SkillSeidJsonData74> DataList = new List<SkillSeidJsonData74>();

		// Token: 0x040043AF RID: 17327
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData74.OnInitFinish);

		// Token: 0x040043B0 RID: 17328
		public int skillid;

		// Token: 0x040043B1 RID: 17329
		public int value1;
	}
}
