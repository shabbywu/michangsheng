using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC6 RID: 3270
	public class SkillSeidJsonData67 : IJSONClass
	{
		// Token: 0x06004E80 RID: 20096 RVA: 0x0020FDE4 File Offset: 0x0020DFE4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[67].list)
			{
				try
				{
					SkillSeidJsonData67 skillSeidJsonData = new SkillSeidJsonData67();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					skillSeidJsonData.value4 = jsonobject["value4"].I;
					if (SkillSeidJsonData67.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData67.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData67.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData67.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData67.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData67.OnInitFinishAction != null)
			{
				SkillSeidJsonData67.OnInitFinishAction();
			}
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EA4 RID: 20132
		public static int SEIDID = 67;

		// Token: 0x04004EA5 RID: 20133
		public static Dictionary<int, SkillSeidJsonData67> DataDict = new Dictionary<int, SkillSeidJsonData67>();

		// Token: 0x04004EA6 RID: 20134
		public static List<SkillSeidJsonData67> DataList = new List<SkillSeidJsonData67>();

		// Token: 0x04004EA7 RID: 20135
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData67.OnInitFinish);

		// Token: 0x04004EA8 RID: 20136
		public int skillid;

		// Token: 0x04004EA9 RID: 20137
		public int value1;

		// Token: 0x04004EAA RID: 20138
		public int value2;

		// Token: 0x04004EAB RID: 20139
		public int value3;

		// Token: 0x04004EAC RID: 20140
		public int value4;
	}
}
