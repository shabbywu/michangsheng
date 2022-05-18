using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C8E RID: 3214
	public class SkillSeidJsonData15 : IJSONClass
	{
		// Token: 0x06004DA1 RID: 19873 RVA: 0x0020B980 File Offset: 0x00209B80
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[15].list)
			{
				try
				{
					SkillSeidJsonData15 skillSeidJsonData = new SkillSeidJsonData15();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData15.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData15.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData15.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData15.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData15.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData15.OnInitFinishAction != null)
			{
				SkillSeidJsonData15.OnInitFinishAction();
			}
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D26 RID: 19750
		public static int SEIDID = 15;

		// Token: 0x04004D27 RID: 19751
		public static Dictionary<int, SkillSeidJsonData15> DataDict = new Dictionary<int, SkillSeidJsonData15>();

		// Token: 0x04004D28 RID: 19752
		public static List<SkillSeidJsonData15> DataList = new List<SkillSeidJsonData15>();

		// Token: 0x04004D29 RID: 19753
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData15.OnInitFinish);

		// Token: 0x04004D2A RID: 19754
		public int skillid;

		// Token: 0x04004D2B RID: 19755
		public int value1;

		// Token: 0x04004D2C RID: 19756
		public int value2;
	}
}
