using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CBD RID: 3261
	public class SkillSeidJsonData58 : IJSONClass
	{
		// Token: 0x06004E5C RID: 20060 RVA: 0x0020F368 File Offset: 0x0020D568
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[58].list)
			{
				try
				{
					SkillSeidJsonData58 skillSeidJsonData = new SkillSeidJsonData58();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData58.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData58.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData58.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData58.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData58.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData58.OnInitFinishAction != null)
			{
				SkillSeidJsonData58.OnInitFinishAction();
			}
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E6D RID: 20077
		public static int SEIDID = 58;

		// Token: 0x04004E6E RID: 20078
		public static Dictionary<int, SkillSeidJsonData58> DataDict = new Dictionary<int, SkillSeidJsonData58>();

		// Token: 0x04004E6F RID: 20079
		public static List<SkillSeidJsonData58> DataList = new List<SkillSeidJsonData58>();

		// Token: 0x04004E70 RID: 20080
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData58.OnInitFinish);

		// Token: 0x04004E71 RID: 20081
		public int skillid;

		// Token: 0x04004E72 RID: 20082
		public int value1;
	}
}
