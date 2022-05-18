using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC4 RID: 3268
	public class SkillSeidJsonData65 : IJSONClass
	{
		// Token: 0x06004E78 RID: 20088 RVA: 0x0020FB94 File Offset: 0x0020DD94
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[65].list)
			{
				try
				{
					SkillSeidJsonData65 skillSeidJsonData = new SkillSeidJsonData65();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData65.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData65.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData65.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData65.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData65.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData65.OnInitFinishAction != null)
			{
				SkillSeidJsonData65.OnInitFinishAction();
			}
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E98 RID: 20120
		public static int SEIDID = 65;

		// Token: 0x04004E99 RID: 20121
		public static Dictionary<int, SkillSeidJsonData65> DataDict = new Dictionary<int, SkillSeidJsonData65>();

		// Token: 0x04004E9A RID: 20122
		public static List<SkillSeidJsonData65> DataList = new List<SkillSeidJsonData65>();

		// Token: 0x04004E9B RID: 20123
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData65.OnInitFinish);

		// Token: 0x04004E9C RID: 20124
		public int skillid;

		// Token: 0x04004E9D RID: 20125
		public int value1;
	}
}
