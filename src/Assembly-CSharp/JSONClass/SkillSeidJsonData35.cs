using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000922 RID: 2338
	public class SkillSeidJsonData35 : IJSONClass
	{
		// Token: 0x0600429A RID: 17050 RVA: 0x001C6D64 File Offset: 0x001C4F64
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[35].list)
			{
				try
				{
					SkillSeidJsonData35 skillSeidJsonData = new SkillSeidJsonData35();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData35.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData35.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData35.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData35.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData35.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData35.OnInitFinishAction != null)
			{
				SkillSeidJsonData35.OnInitFinishAction();
			}
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042BD RID: 17085
		public static int SEIDID = 35;

		// Token: 0x040042BE RID: 17086
		public static Dictionary<int, SkillSeidJsonData35> DataDict = new Dictionary<int, SkillSeidJsonData35>();

		// Token: 0x040042BF RID: 17087
		public static List<SkillSeidJsonData35> DataList = new List<SkillSeidJsonData35>();

		// Token: 0x040042C0 RID: 17088
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData35.OnInitFinish);

		// Token: 0x040042C1 RID: 17089
		public int skillid;

		// Token: 0x040042C2 RID: 17090
		public int value1;
	}
}
