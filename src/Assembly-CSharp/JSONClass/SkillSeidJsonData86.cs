using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD5 RID: 3285
	public class SkillSeidJsonData86 : IJSONClass
	{
		// Token: 0x06004EBC RID: 20156 RVA: 0x00211060 File Offset: 0x0020F260
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[86].list)
			{
				try
				{
					SkillSeidJsonData86 skillSeidJsonData = new SkillSeidJsonData86();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData86.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData86.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData86.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData86.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData86.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData86.OnInitFinishAction != null)
			{
				SkillSeidJsonData86.OnInitFinishAction();
			}
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F0B RID: 20235
		public static int SEIDID = 86;

		// Token: 0x04004F0C RID: 20236
		public static Dictionary<int, SkillSeidJsonData86> DataDict = new Dictionary<int, SkillSeidJsonData86>();

		// Token: 0x04004F0D RID: 20237
		public static List<SkillSeidJsonData86> DataList = new List<SkillSeidJsonData86>();

		// Token: 0x04004F0E RID: 20238
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData86.OnInitFinish);

		// Token: 0x04004F0F RID: 20239
		public int skillid;

		// Token: 0x04004F10 RID: 20240
		public int value1;
	}
}
