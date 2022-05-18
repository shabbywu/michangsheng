using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC3 RID: 3267
	public class SkillSeidJsonData64 : IJSONClass
	{
		// Token: 0x06004E74 RID: 20084 RVA: 0x0020FA6C File Offset: 0x0020DC6C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[64].list)
			{
				try
				{
					SkillSeidJsonData64 skillSeidJsonData = new SkillSeidJsonData64();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData64.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData64.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData64.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData64.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData64.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData64.OnInitFinishAction != null)
			{
				SkillSeidJsonData64.OnInitFinishAction();
			}
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E92 RID: 20114
		public static int SEIDID = 64;

		// Token: 0x04004E93 RID: 20115
		public static Dictionary<int, SkillSeidJsonData64> DataDict = new Dictionary<int, SkillSeidJsonData64>();

		// Token: 0x04004E94 RID: 20116
		public static List<SkillSeidJsonData64> DataList = new List<SkillSeidJsonData64>();

		// Token: 0x04004E95 RID: 20117
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData64.OnInitFinish);

		// Token: 0x04004E96 RID: 20118
		public int skillid;

		// Token: 0x04004E97 RID: 20119
		public int value1;
	}
}
