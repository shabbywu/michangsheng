using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C77 RID: 3191
	public class SkillSeidJsonData108 : IJSONClass
	{
		// Token: 0x06004D45 RID: 19781 RVA: 0x00209CE4 File Offset: 0x00207EE4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[108].list)
			{
				try
				{
					SkillSeidJsonData108 skillSeidJsonData = new SkillSeidJsonData108();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData108.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData108.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData108.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData108.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData108.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData108.OnInitFinishAction != null)
			{
				SkillSeidJsonData108.OnInitFinishAction();
			}
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C89 RID: 19593
		public static int SEIDID = 108;

		// Token: 0x04004C8A RID: 19594
		public static Dictionary<int, SkillSeidJsonData108> DataDict = new Dictionary<int, SkillSeidJsonData108>();

		// Token: 0x04004C8B RID: 19595
		public static List<SkillSeidJsonData108> DataList = new List<SkillSeidJsonData108>();

		// Token: 0x04004C8C RID: 19596
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData108.OnInitFinish);

		// Token: 0x04004C8D RID: 19597
		public int skillid;

		// Token: 0x04004C8E RID: 19598
		public int value1;

		// Token: 0x04004C8F RID: 19599
		public int value2;

		// Token: 0x04004C90 RID: 19600
		public int value3;
	}
}
