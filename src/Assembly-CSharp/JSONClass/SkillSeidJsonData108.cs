using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008EA RID: 2282
	public class SkillSeidJsonData108 : IJSONClass
	{
		// Token: 0x060041BB RID: 16827 RVA: 0x001C1CA4 File Offset: 0x001BFEA4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[108].list)
			{
				try
				{
					SkillSeidJsonData108 skillSeidJsonData = new SkillSeidJsonData108();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData108.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData108.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData108.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData108.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData108.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData108.OnInitFinishAction != null)
			{
				SkillSeidJsonData108.OnInitFinishAction();
			}
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400413F RID: 16703
		public static int SEIDID = 108;

		// Token: 0x04004140 RID: 16704
		public static Dictionary<int, SkillSeidJsonData108> DataDict = new Dictionary<int, SkillSeidJsonData108>();

		// Token: 0x04004141 RID: 16705
		public static List<SkillSeidJsonData108> DataList = new List<SkillSeidJsonData108>();

		// Token: 0x04004142 RID: 16706
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData108.OnInitFinish);

		// Token: 0x04004143 RID: 16707
		public int skillid;

		// Token: 0x04004144 RID: 16708
		public int value1;

		// Token: 0x04004145 RID: 16709
		public int value2;

		// Token: 0x04004146 RID: 16710
		public int value3;
	}
}
