using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008EB RID: 2283
	public class SkillSeidJsonData109 : IJSONClass
	{
		// Token: 0x060041BF RID: 16831 RVA: 0x001C1E28 File Offset: 0x001C0028
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[109].list)
			{
				try
				{
					SkillSeidJsonData109 skillSeidJsonData = new SkillSeidJsonData109();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData109.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData109.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData109.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData109.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData109.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData109.OnInitFinishAction != null)
			{
				SkillSeidJsonData109.OnInitFinishAction();
			}
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004147 RID: 16711
		public static int SEIDID = 109;

		// Token: 0x04004148 RID: 16712
		public static Dictionary<int, SkillSeidJsonData109> DataDict = new Dictionary<int, SkillSeidJsonData109>();

		// Token: 0x04004149 RID: 16713
		public static List<SkillSeidJsonData109> DataList = new List<SkillSeidJsonData109>();

		// Token: 0x0400414A RID: 16714
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData109.OnInitFinish);

		// Token: 0x0400414B RID: 16715
		public int skillid;

		// Token: 0x0400414C RID: 16716
		public int value1;
	}
}
