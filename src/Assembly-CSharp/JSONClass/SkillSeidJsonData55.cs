using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CBA RID: 3258
	public class SkillSeidJsonData55 : IJSONClass
	{
		// Token: 0x06004E50 RID: 20048 RVA: 0x0020EFC8 File Offset: 0x0020D1C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[55].list)
			{
				try
				{
					SkillSeidJsonData55 skillSeidJsonData = new SkillSeidJsonData55();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData55.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData55.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData55.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData55.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData55.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData55.OnInitFinishAction != null)
			{
				SkillSeidJsonData55.OnInitFinishAction();
			}
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E59 RID: 20057
		public static int SEIDID = 55;

		// Token: 0x04004E5A RID: 20058
		public static Dictionary<int, SkillSeidJsonData55> DataDict = new Dictionary<int, SkillSeidJsonData55>();

		// Token: 0x04004E5B RID: 20059
		public static List<SkillSeidJsonData55> DataList = new List<SkillSeidJsonData55>();

		// Token: 0x04004E5C RID: 20060
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData55.OnInitFinish);

		// Token: 0x04004E5D RID: 20061
		public int skillid;

		// Token: 0x04004E5E RID: 20062
		public int value1;
	}
}
