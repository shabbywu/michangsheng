using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC1 RID: 3265
	public class SkillSeidJsonData62 : IJSONClass
	{
		// Token: 0x06004E6C RID: 20076 RVA: 0x0020F808 File Offset: 0x0020DA08
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[62].list)
			{
				try
				{
					SkillSeidJsonData62 skillSeidJsonData = new SkillSeidJsonData62();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData62.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData62.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData62.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData62.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData62.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData62.OnInitFinishAction != null)
			{
				SkillSeidJsonData62.OnInitFinishAction();
			}
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E85 RID: 20101
		public static int SEIDID = 62;

		// Token: 0x04004E86 RID: 20102
		public static Dictionary<int, SkillSeidJsonData62> DataDict = new Dictionary<int, SkillSeidJsonData62>();

		// Token: 0x04004E87 RID: 20103
		public static List<SkillSeidJsonData62> DataList = new List<SkillSeidJsonData62>();

		// Token: 0x04004E88 RID: 20104
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData62.OnInitFinish);

		// Token: 0x04004E89 RID: 20105
		public int skillid;

		// Token: 0x04004E8A RID: 20106
		public int value1;
	}
}
