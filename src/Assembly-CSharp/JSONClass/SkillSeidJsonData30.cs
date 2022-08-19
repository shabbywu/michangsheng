using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200091D RID: 2333
	public class SkillSeidJsonData30 : IJSONClass
	{
		// Token: 0x06004286 RID: 17030 RVA: 0x001C6640 File Offset: 0x001C4840
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[30].list)
			{
				try
				{
					SkillSeidJsonData30 skillSeidJsonData = new SkillSeidJsonData30();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData30.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData30.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData30.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData30.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData30.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData30.OnInitFinishAction != null)
			{
				SkillSeidJsonData30.OnInitFinishAction();
			}
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400429B RID: 17051
		public static int SEIDID = 30;

		// Token: 0x0400429C RID: 17052
		public static Dictionary<int, SkillSeidJsonData30> DataDict = new Dictionary<int, SkillSeidJsonData30>();

		// Token: 0x0400429D RID: 17053
		public static List<SkillSeidJsonData30> DataList = new List<SkillSeidJsonData30>();

		// Token: 0x0400429E RID: 17054
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData30.OnInitFinish);

		// Token: 0x0400429F RID: 17055
		public int skillid;

		// Token: 0x040042A0 RID: 17056
		public int value1;

		// Token: 0x040042A1 RID: 17057
		public int value2;
	}
}
