using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CAE RID: 3246
	public class SkillSeidJsonData42 : IJSONClass
	{
		// Token: 0x06004E20 RID: 20000 RVA: 0x0020E0A8 File Offset: 0x0020C2A8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[42].list)
			{
				try
				{
					SkillSeidJsonData42 skillSeidJsonData = new SkillSeidJsonData42();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData42.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData42.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData42.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData42.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData42.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData42.OnInitFinishAction != null)
			{
				SkillSeidJsonData42.OnInitFinishAction();
			}
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E02 RID: 19970
		public static int SEIDID = 42;

		// Token: 0x04004E03 RID: 19971
		public static Dictionary<int, SkillSeidJsonData42> DataDict = new Dictionary<int, SkillSeidJsonData42>();

		// Token: 0x04004E04 RID: 19972
		public static List<SkillSeidJsonData42> DataList = new List<SkillSeidJsonData42>();

		// Token: 0x04004E05 RID: 19973
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData42.OnInitFinish);

		// Token: 0x04004E06 RID: 19974
		public int skillid;

		// Token: 0x04004E07 RID: 19975
		public int value1;

		// Token: 0x04004E08 RID: 19976
		public int value2;
	}
}
