using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD3 RID: 3283
	public class SkillSeidJsonData84 : IJSONClass
	{
		// Token: 0x06004EB4 RID: 20148 RVA: 0x00210DE8 File Offset: 0x0020EFE8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[84].list)
			{
				try
				{
					SkillSeidJsonData84 skillSeidJsonData = new SkillSeidJsonData84();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData84.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData84.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData84.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData84.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData84.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData84.OnInitFinishAction != null)
			{
				SkillSeidJsonData84.OnInitFinishAction();
			}
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EFD RID: 20221
		public static int SEIDID = 84;

		// Token: 0x04004EFE RID: 20222
		public static Dictionary<int, SkillSeidJsonData84> DataDict = new Dictionary<int, SkillSeidJsonData84>();

		// Token: 0x04004EFF RID: 20223
		public static List<SkillSeidJsonData84> DataList = new List<SkillSeidJsonData84>();

		// Token: 0x04004F00 RID: 20224
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData84.OnInitFinish);

		// Token: 0x04004F01 RID: 20225
		public int skillid;

		// Token: 0x04004F02 RID: 20226
		public int value1;

		// Token: 0x04004F03 RID: 20227
		public int value2;
	}
}
