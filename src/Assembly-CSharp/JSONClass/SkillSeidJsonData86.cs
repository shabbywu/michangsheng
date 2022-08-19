using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000951 RID: 2385
	public class SkillSeidJsonData86 : IJSONClass
	{
		// Token: 0x06004356 RID: 17238 RVA: 0x001CB098 File Offset: 0x001C9298
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[86].list)
			{
				try
				{
					SkillSeidJsonData86 skillSeidJsonData = new SkillSeidJsonData86();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData86.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData86.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData86.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData86.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData86.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData86.OnInitFinishAction != null)
			{
				SkillSeidJsonData86.OnInitFinishAction();
			}
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043FB RID: 17403
		public static int SEIDID = 86;

		// Token: 0x040043FC RID: 17404
		public static Dictionary<int, SkillSeidJsonData86> DataDict = new Dictionary<int, SkillSeidJsonData86>();

		// Token: 0x040043FD RID: 17405
		public static List<SkillSeidJsonData86> DataList = new List<SkillSeidJsonData86>();

		// Token: 0x040043FE RID: 17406
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData86.OnInitFinish);

		// Token: 0x040043FF RID: 17407
		public int skillid;

		// Token: 0x04004400 RID: 17408
		public int value1;
	}
}
