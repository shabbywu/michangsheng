using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C9A RID: 3226
	public class SkillSeidJsonData17 : IJSONClass
	{
		// Token: 0x06004DD0 RID: 19920 RVA: 0x0020C8AC File Offset: 0x0020AAAC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[17].list)
			{
				try
				{
					SkillSeidJsonData17 skillSeidJsonData = new SkillSeidJsonData17();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData17.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData17.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData17.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData17.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData17.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData17.OnInitFinishAction != null)
			{
				SkillSeidJsonData17.OnInitFinishAction();
			}
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D7F RID: 19839
		public static int SEIDID = 17;

		// Token: 0x04004D80 RID: 19840
		public static Dictionary<int, SkillSeidJsonData17> DataDict = new Dictionary<int, SkillSeidJsonData17>();

		// Token: 0x04004D81 RID: 19841
		public static List<SkillSeidJsonData17> DataList = new List<SkillSeidJsonData17>();

		// Token: 0x04004D82 RID: 19842
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData17.OnInitFinish);

		// Token: 0x04004D83 RID: 19843
		public int skillid;

		// Token: 0x04004D84 RID: 19844
		public int value1;

		// Token: 0x04004D85 RID: 19845
		public int value2;
	}
}
