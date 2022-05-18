using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CBC RID: 3260
	public class SkillSeidJsonData57 : IJSONClass
	{
		// Token: 0x06004E58 RID: 20056 RVA: 0x0020F22C File Offset: 0x0020D42C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[57].list)
			{
				try
				{
					SkillSeidJsonData57 skillSeidJsonData = new SkillSeidJsonData57();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData57.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData57.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData57.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData57.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData57.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData57.OnInitFinishAction != null)
			{
				SkillSeidJsonData57.OnInitFinishAction();
			}
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E66 RID: 20070
		public static int SEIDID = 57;

		// Token: 0x04004E67 RID: 20071
		public static Dictionary<int, SkillSeidJsonData57> DataDict = new Dictionary<int, SkillSeidJsonData57>();

		// Token: 0x04004E68 RID: 20072
		public static List<SkillSeidJsonData57> DataList = new List<SkillSeidJsonData57>();

		// Token: 0x04004E69 RID: 20073
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData57.OnInitFinish);

		// Token: 0x04004E6A RID: 20074
		public int skillid;

		// Token: 0x04004E6B RID: 20075
		public List<int> value1 = new List<int>();

		// Token: 0x04004E6C RID: 20076
		public List<int> value2 = new List<int>();
	}
}
