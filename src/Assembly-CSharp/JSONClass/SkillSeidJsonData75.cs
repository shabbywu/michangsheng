using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CCA RID: 3274
	public class SkillSeidJsonData75 : IJSONClass
	{
		// Token: 0x06004E90 RID: 20112 RVA: 0x0021031C File Offset: 0x0020E51C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[75].list)
			{
				try
				{
					SkillSeidJsonData75 skillSeidJsonData = new SkillSeidJsonData75();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData75.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData75.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData75.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData75.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData75.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData75.OnInitFinishAction != null)
			{
				SkillSeidJsonData75.OnInitFinishAction();
			}
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EC2 RID: 20162
		public static int SEIDID = 75;

		// Token: 0x04004EC3 RID: 20163
		public static Dictionary<int, SkillSeidJsonData75> DataDict = new Dictionary<int, SkillSeidJsonData75>();

		// Token: 0x04004EC4 RID: 20164
		public static List<SkillSeidJsonData75> DataList = new List<SkillSeidJsonData75>();

		// Token: 0x04004EC5 RID: 20165
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData75.OnInitFinish);

		// Token: 0x04004EC6 RID: 20166
		public int skillid;

		// Token: 0x04004EC7 RID: 20167
		public int value1;

		// Token: 0x04004EC8 RID: 20168
		public int value2;
	}
}
