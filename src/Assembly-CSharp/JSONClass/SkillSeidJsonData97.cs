using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE0 RID: 3296
	public class SkillSeidJsonData97 : IJSONClass
	{
		// Token: 0x06004EE8 RID: 20200 RVA: 0x00211D90 File Offset: 0x0020FF90
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

		// Token: 0x06004EE9 RID: 20201 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F53 RID: 20307
		public static int SEIDID = 97;

		// Token: 0x04004F54 RID: 20308
		public static Dictionary<int, SkillSeidJsonData97> DataDict = new Dictionary<int, SkillSeidJsonData97>();

		// Token: 0x04004F55 RID: 20309
		public static List<SkillSeidJsonData97> DataList = new List<SkillSeidJsonData97>();

		// Token: 0x04004F56 RID: 20310
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData97.OnInitFinish);

		// Token: 0x04004F57 RID: 20311
		public int skillid;

		// Token: 0x04004F58 RID: 20312
		public int value1;
	}
}
