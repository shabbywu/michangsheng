using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C9B RID: 3227
	public class SkillSeidJsonData2 : IJSONClass
	{
		// Token: 0x06004DD4 RID: 19924 RVA: 0x0020C9E8 File Offset: 0x0020ABE8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[2].list)
			{
				try
				{
					SkillSeidJsonData2 skillSeidJsonData = new SkillSeidJsonData2();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData2.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData2.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData2.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData2.OnInitFinishAction != null)
			{
				SkillSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D86 RID: 19846
		public static int SEIDID = 2;

		// Token: 0x04004D87 RID: 19847
		public static Dictionary<int, SkillSeidJsonData2> DataDict = new Dictionary<int, SkillSeidJsonData2>();

		// Token: 0x04004D88 RID: 19848
		public static List<SkillSeidJsonData2> DataList = new List<SkillSeidJsonData2>();

		// Token: 0x04004D89 RID: 19849
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData2.OnInitFinish);

		// Token: 0x04004D8A RID: 19850
		public int skillid;

		// Token: 0x04004D8B RID: 19851
		public int value1;
	}
}
