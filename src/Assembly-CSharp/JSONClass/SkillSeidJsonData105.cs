using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C74 RID: 3188
	public class SkillSeidJsonData105 : IJSONClass
	{
		// Token: 0x06004D39 RID: 19769 RVA: 0x00209900 File Offset: 0x00207B00
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[105].list)
			{
				try
				{
					SkillSeidJsonData105 skillSeidJsonData = new SkillSeidJsonData105();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData105.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData105.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData105.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData105.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData105.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData105.OnInitFinishAction != null)
			{
				SkillSeidJsonData105.OnInitFinishAction();
			}
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C73 RID: 19571
		public static int SEIDID = 105;

		// Token: 0x04004C74 RID: 19572
		public static Dictionary<int, SkillSeidJsonData105> DataDict = new Dictionary<int, SkillSeidJsonData105>();

		// Token: 0x04004C75 RID: 19573
		public static List<SkillSeidJsonData105> DataList = new List<SkillSeidJsonData105>();

		// Token: 0x04004C76 RID: 19574
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData105.OnInitFinish);

		// Token: 0x04004C77 RID: 19575
		public int skillid;

		// Token: 0x04004C78 RID: 19576
		public int value1;
	}
}
