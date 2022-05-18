using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB9 RID: 3257
	public class SkillSeidJsonData54 : IJSONClass
	{
		// Token: 0x06004E4C RID: 20044 RVA: 0x0020EEA0 File Offset: 0x0020D0A0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[54].list)
			{
				try
				{
					SkillSeidJsonData54 skillSeidJsonData = new SkillSeidJsonData54();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData54.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData54.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData54.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData54.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData54.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData54.OnInitFinishAction != null)
			{
				SkillSeidJsonData54.OnInitFinishAction();
			}
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E53 RID: 20051
		public static int SEIDID = 54;

		// Token: 0x04004E54 RID: 20052
		public static Dictionary<int, SkillSeidJsonData54> DataDict = new Dictionary<int, SkillSeidJsonData54>();

		// Token: 0x04004E55 RID: 20053
		public static List<SkillSeidJsonData54> DataList = new List<SkillSeidJsonData54>();

		// Token: 0x04004E56 RID: 20054
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData54.OnInitFinish);

		// Token: 0x04004E57 RID: 20055
		public int skillid;

		// Token: 0x04004E58 RID: 20056
		public int value1;
	}
}
