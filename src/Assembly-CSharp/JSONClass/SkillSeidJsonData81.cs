using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD0 RID: 3280
	public class SkillSeidJsonData81 : IJSONClass
	{
		// Token: 0x06004EA8 RID: 20136 RVA: 0x00210A48 File Offset: 0x0020EC48
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[81].list)
			{
				try
				{
					SkillSeidJsonData81 skillSeidJsonData = new SkillSeidJsonData81();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData81.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData81.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData81.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData81.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData81.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData81.OnInitFinishAction != null)
			{
				SkillSeidJsonData81.OnInitFinishAction();
			}
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EE9 RID: 20201
		public static int SEIDID = 81;

		// Token: 0x04004EEA RID: 20202
		public static Dictionary<int, SkillSeidJsonData81> DataDict = new Dictionary<int, SkillSeidJsonData81>();

		// Token: 0x04004EEB RID: 20203
		public static List<SkillSeidJsonData81> DataList = new List<SkillSeidJsonData81>();

		// Token: 0x04004EEC RID: 20204
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData81.OnInitFinish);

		// Token: 0x04004EED RID: 20205
		public int skillid;

		// Token: 0x04004EEE RID: 20206
		public int value1;
	}
}
