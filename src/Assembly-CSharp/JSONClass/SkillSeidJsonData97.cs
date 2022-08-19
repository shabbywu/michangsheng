using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200095C RID: 2396
	public class SkillSeidJsonData97 : IJSONClass
	{
		// Token: 0x06004382 RID: 17282 RVA: 0x001CBFD8 File Offset: 0x001CA1D8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[97].list)
			{
				try
				{
					SkillSeidJsonData97 skillSeidJsonData = new SkillSeidJsonData97();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData97.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData97.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData97.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData97.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData97.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData97.OnInitFinishAction != null)
			{
				SkillSeidJsonData97.OnInitFinishAction();
			}
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004443 RID: 17475
		public static int SEIDID = 97;

		// Token: 0x04004444 RID: 17476
		public static Dictionary<int, SkillSeidJsonData97> DataDict = new Dictionary<int, SkillSeidJsonData97>();

		// Token: 0x04004445 RID: 17477
		public static List<SkillSeidJsonData97> DataList = new List<SkillSeidJsonData97>();

		// Token: 0x04004446 RID: 17478
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData97.OnInitFinish);

		// Token: 0x04004447 RID: 17479
		public int skillid;

		// Token: 0x04004448 RID: 17480
		public int value1;
	}
}
