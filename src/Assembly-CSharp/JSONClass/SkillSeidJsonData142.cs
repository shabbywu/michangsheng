using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C86 RID: 3206
	public class SkillSeidJsonData142 : IJSONClass
	{
		// Token: 0x06004D81 RID: 19841 RVA: 0x0020AF18 File Offset: 0x00209118
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[142].list)
			{
				try
				{
					SkillSeidJsonData142 skillSeidJsonData = new SkillSeidJsonData142();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData142.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData142.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData142.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData142.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData142.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData142.OnInitFinishAction != null)
			{
				SkillSeidJsonData142.OnInitFinishAction();
			}
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CED RID: 19693
		public static int SEIDID = 142;

		// Token: 0x04004CEE RID: 19694
		public static Dictionary<int, SkillSeidJsonData142> DataDict = new Dictionary<int, SkillSeidJsonData142>();

		// Token: 0x04004CEF RID: 19695
		public static List<SkillSeidJsonData142> DataList = new List<SkillSeidJsonData142>();

		// Token: 0x04004CF0 RID: 19696
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData142.OnInitFinish);

		// Token: 0x04004CF1 RID: 19697
		public int skillid;

		// Token: 0x04004CF2 RID: 19698
		public int value1;
	}
}
