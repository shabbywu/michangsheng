using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008FF RID: 2303
	public class SkillSeidJsonData142 : IJSONClass
	{
		// Token: 0x0600420F RID: 16911 RVA: 0x001C3A94 File Offset: 0x001C1C94
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[142].list)
			{
				try
				{
					SkillSeidJsonData142 skillSeidJsonData = new SkillSeidJsonData142();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData142.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData142.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData142.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData142.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData142.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData142.OnInitFinishAction != null)
			{
				SkillSeidJsonData142.OnInitFinishAction();
			}
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041CB RID: 16843
		public static int SEIDID = 142;

		// Token: 0x040041CC RID: 16844
		public static Dictionary<int, SkillSeidJsonData142> DataDict = new Dictionary<int, SkillSeidJsonData142>();

		// Token: 0x040041CD RID: 16845
		public static List<SkillSeidJsonData142> DataList = new List<SkillSeidJsonData142>();

		// Token: 0x040041CE RID: 16846
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData142.OnInitFinish);

		// Token: 0x040041CF RID: 16847
		public int skillid;

		// Token: 0x040041D0 RID: 16848
		public int value1;
	}
}
