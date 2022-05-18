using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C76 RID: 3190
	public class SkillSeidJsonData107 : IJSONClass
	{
		// Token: 0x06004D41 RID: 19777 RVA: 0x00209BA8 File Offset: 0x00207DA8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[107].list)
			{
				try
				{
					SkillSeidJsonData107 skillSeidJsonData = new SkillSeidJsonData107();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData107.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData107.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData107.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData107.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData107.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData107.OnInitFinishAction != null)
			{
				SkillSeidJsonData107.OnInitFinishAction();
			}
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C82 RID: 19586
		public static int SEIDID = 107;

		// Token: 0x04004C83 RID: 19587
		public static Dictionary<int, SkillSeidJsonData107> DataDict = new Dictionary<int, SkillSeidJsonData107>();

		// Token: 0x04004C84 RID: 19588
		public static List<SkillSeidJsonData107> DataList = new List<SkillSeidJsonData107>();

		// Token: 0x04004C85 RID: 19589
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData107.OnInitFinish);

		// Token: 0x04004C86 RID: 19590
		public int skillid;

		// Token: 0x04004C87 RID: 19591
		public int target;

		// Token: 0x04004C88 RID: 19592
		public int value1;
	}
}
