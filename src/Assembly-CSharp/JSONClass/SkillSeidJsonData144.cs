using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C88 RID: 3208
	public class SkillSeidJsonData144 : IJSONClass
	{
		// Token: 0x06004D89 RID: 19849 RVA: 0x0020B170 File Offset: 0x00209370
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[144].list)
			{
				try
				{
					SkillSeidJsonData144 skillSeidJsonData = new SkillSeidJsonData144();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData144.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData144.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData144.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData144.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData144.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData144.OnInitFinishAction != null)
			{
				SkillSeidJsonData144.OnInitFinishAction();
			}
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CF9 RID: 19705
		public static int SEIDID = 144;

		// Token: 0x04004CFA RID: 19706
		public static Dictionary<int, SkillSeidJsonData144> DataDict = new Dictionary<int, SkillSeidJsonData144>();

		// Token: 0x04004CFB RID: 19707
		public static List<SkillSeidJsonData144> DataList = new List<SkillSeidJsonData144>();

		// Token: 0x04004CFC RID: 19708
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData144.OnInitFinish);

		// Token: 0x04004CFD RID: 19709
		public int skillid;

		// Token: 0x04004CFE RID: 19710
		public int value1;
	}
}
