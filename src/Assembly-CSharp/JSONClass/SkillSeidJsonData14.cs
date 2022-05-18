using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C83 RID: 3203
	public class SkillSeidJsonData14 : IJSONClass
	{
		// Token: 0x06004D75 RID: 19829 RVA: 0x0020AB98 File Offset: 0x00208D98
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[14].list)
			{
				try
				{
					SkillSeidJsonData14 skillSeidJsonData = new SkillSeidJsonData14();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData14.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData14.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData14.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData14.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData14.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData14.OnInitFinishAction != null)
			{
				SkillSeidJsonData14.OnInitFinishAction();
			}
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CDB RID: 19675
		public static int SEIDID = 14;

		// Token: 0x04004CDC RID: 19676
		public static Dictionary<int, SkillSeidJsonData14> DataDict = new Dictionary<int, SkillSeidJsonData14>();

		// Token: 0x04004CDD RID: 19677
		public static List<SkillSeidJsonData14> DataList = new List<SkillSeidJsonData14>();

		// Token: 0x04004CDE RID: 19678
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData14.OnInitFinish);

		// Token: 0x04004CDF RID: 19679
		public int skillid;

		// Token: 0x04004CE0 RID: 19680
		public int value1;
	}
}
