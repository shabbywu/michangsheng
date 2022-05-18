using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CBF RID: 3263
	public class SkillSeidJsonData60 : IJSONClass
	{
		// Token: 0x06004E64 RID: 20068 RVA: 0x0020F5B8 File Offset: 0x0020D7B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[60].list)
			{
				try
				{
					SkillSeidJsonData60 skillSeidJsonData = new SkillSeidJsonData60();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData60.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData60.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData60.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData60.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData60.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData60.OnInitFinishAction != null)
			{
				SkillSeidJsonData60.OnInitFinishAction();
			}
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E79 RID: 20089
		public static int SEIDID = 60;

		// Token: 0x04004E7A RID: 20090
		public static Dictionary<int, SkillSeidJsonData60> DataDict = new Dictionary<int, SkillSeidJsonData60>();

		// Token: 0x04004E7B RID: 20091
		public static List<SkillSeidJsonData60> DataList = new List<SkillSeidJsonData60>();

		// Token: 0x04004E7C RID: 20092
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData60.OnInitFinish);

		// Token: 0x04004E7D RID: 20093
		public int skillid;

		// Token: 0x04004E7E RID: 20094
		public int value1;
	}
}
