using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000934 RID: 2356
	public class SkillSeidJsonData52 : IJSONClass
	{
		// Token: 0x060042E2 RID: 17122 RVA: 0x001C87B4 File Offset: 0x001C69B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[52].list)
			{
				try
				{
					SkillSeidJsonData52 skillSeidJsonData = new SkillSeidJsonData52();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData52.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData52.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData52.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData52.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData52.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData52.OnInitFinishAction != null)
			{
				SkillSeidJsonData52.OnInitFinishAction();
			}
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400433D RID: 17213
		public static int SEIDID = 52;

		// Token: 0x0400433E RID: 17214
		public static Dictionary<int, SkillSeidJsonData52> DataDict = new Dictionary<int, SkillSeidJsonData52>();

		// Token: 0x0400433F RID: 17215
		public static List<SkillSeidJsonData52> DataList = new List<SkillSeidJsonData52>();

		// Token: 0x04004340 RID: 17216
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData52.OnInitFinish);

		// Token: 0x04004341 RID: 17217
		public int skillid;

		// Token: 0x04004342 RID: 17218
		public int value1;
	}
}
