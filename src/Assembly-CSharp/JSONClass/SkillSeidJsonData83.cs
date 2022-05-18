using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD2 RID: 3282
	public class SkillSeidJsonData83 : IJSONClass
	{
		// Token: 0x06004EB0 RID: 20144 RVA: 0x00210CAC File Offset: 0x0020EEAC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[83].list)
			{
				try
				{
					SkillSeidJsonData83 skillSeidJsonData = new SkillSeidJsonData83();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData83.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData83.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData83.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData83.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData83.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData83.OnInitFinishAction != null)
			{
				SkillSeidJsonData83.OnInitFinishAction();
			}
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EF6 RID: 20214
		public static int SEIDID = 83;

		// Token: 0x04004EF7 RID: 20215
		public static Dictionary<int, SkillSeidJsonData83> DataDict = new Dictionary<int, SkillSeidJsonData83>();

		// Token: 0x04004EF8 RID: 20216
		public static List<SkillSeidJsonData83> DataList = new List<SkillSeidJsonData83>();

		// Token: 0x04004EF9 RID: 20217
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData83.OnInitFinish);

		// Token: 0x04004EFA RID: 20218
		public int skillid;

		// Token: 0x04004EFB RID: 20219
		public List<int> value1 = new List<int>();

		// Token: 0x04004EFC RID: 20220
		public List<int> value2 = new List<int>();
	}
}
