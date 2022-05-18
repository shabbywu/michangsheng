using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CCF RID: 3279
	public class SkillSeidJsonData80 : IJSONClass
	{
		// Token: 0x06004EA4 RID: 20132 RVA: 0x0021090C File Offset: 0x0020EB0C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[80].list)
			{
				try
				{
					SkillSeidJsonData80 skillSeidJsonData = new SkillSeidJsonData80();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData80.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData80.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData80.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData80.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData80.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData80.OnInitFinishAction != null)
			{
				SkillSeidJsonData80.OnInitFinishAction();
			}
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EE2 RID: 20194
		public static int SEIDID = 80;

		// Token: 0x04004EE3 RID: 20195
		public static Dictionary<int, SkillSeidJsonData80> DataDict = new Dictionary<int, SkillSeidJsonData80>();

		// Token: 0x04004EE4 RID: 20196
		public static List<SkillSeidJsonData80> DataList = new List<SkillSeidJsonData80>();

		// Token: 0x04004EE5 RID: 20197
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData80.OnInitFinish);

		// Token: 0x04004EE6 RID: 20198
		public int skillid;

		// Token: 0x04004EE7 RID: 20199
		public List<int> value1 = new List<int>();

		// Token: 0x04004EE8 RID: 20200
		public List<int> value2 = new List<int>();
	}
}
