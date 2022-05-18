using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CAB RID: 3243
	public class SkillSeidJsonData4 : IJSONClass
	{
		// Token: 0x06004E14 RID: 19988 RVA: 0x0020DCF4 File Offset: 0x0020BEF4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[4].list)
			{
				try
				{
					SkillSeidJsonData4 skillSeidJsonData = new SkillSeidJsonData4();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData4.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData4.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData4.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData4.OnInitFinishAction != null)
			{
				SkillSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DED RID: 19949
		public static int SEIDID = 4;

		// Token: 0x04004DEE RID: 19950
		public static Dictionary<int, SkillSeidJsonData4> DataDict = new Dictionary<int, SkillSeidJsonData4>();

		// Token: 0x04004DEF RID: 19951
		public static List<SkillSeidJsonData4> DataList = new List<SkillSeidJsonData4>();

		// Token: 0x04004DF0 RID: 19952
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData4.OnInitFinish);

		// Token: 0x04004DF1 RID: 19953
		public int skillid;

		// Token: 0x04004DF2 RID: 19954
		public List<int> value1 = new List<int>();

		// Token: 0x04004DF3 RID: 19955
		public List<int> value2 = new List<int>();
	}
}
