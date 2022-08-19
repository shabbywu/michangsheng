using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F4 RID: 2292
	public class SkillSeidJsonData119 : IJSONClass
	{
		// Token: 0x060041E3 RID: 16867 RVA: 0x001C2B28 File Offset: 0x001C0D28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[119].list)
			{
				try
				{
					SkillSeidJsonData119 skillSeidJsonData = new SkillSeidJsonData119();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData119.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData119.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData119.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData119.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData119.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData119.OnInitFinishAction != null)
			{
				SkillSeidJsonData119.OnInitFinishAction();
			}
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004184 RID: 16772
		public static int SEIDID = 119;

		// Token: 0x04004185 RID: 16773
		public static Dictionary<int, SkillSeidJsonData119> DataDict = new Dictionary<int, SkillSeidJsonData119>();

		// Token: 0x04004186 RID: 16774
		public static List<SkillSeidJsonData119> DataList = new List<SkillSeidJsonData119>();

		// Token: 0x04004187 RID: 16775
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData119.OnInitFinish);

		// Token: 0x04004188 RID: 16776
		public int skillid;

		// Token: 0x04004189 RID: 16777
		public int value1;
	}
}
