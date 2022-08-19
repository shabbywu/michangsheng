using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F0 RID: 2288
	public class SkillSeidJsonData114 : IJSONClass
	{
		// Token: 0x060041D3 RID: 16851 RVA: 0x001C253C File Offset: 0x001C073C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[114].list)
			{
				try
				{
					SkillSeidJsonData114 skillSeidJsonData = new SkillSeidJsonData114();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData114.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData114.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData114.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData114.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData114.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData114.OnInitFinishAction != null)
			{
				SkillSeidJsonData114.OnInitFinishAction();
			}
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004168 RID: 16744
		public static int SEIDID = 114;

		// Token: 0x04004169 RID: 16745
		public static Dictionary<int, SkillSeidJsonData114> DataDict = new Dictionary<int, SkillSeidJsonData114>();

		// Token: 0x0400416A RID: 16746
		public static List<SkillSeidJsonData114> DataList = new List<SkillSeidJsonData114>();

		// Token: 0x0400416B RID: 16747
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData114.OnInitFinish);

		// Token: 0x0400416C RID: 16748
		public int skillid;

		// Token: 0x0400416D RID: 16749
		public List<int> value1 = new List<int>();

		// Token: 0x0400416E RID: 16750
		public List<int> value2 = new List<int>();
	}
}
