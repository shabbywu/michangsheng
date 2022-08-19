using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F8 RID: 2296
	public class SkillSeidJsonData122 : IJSONClass
	{
		// Token: 0x060041F3 RID: 16883 RVA: 0x001C30C8 File Offset: 0x001C12C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[122].list)
			{
				try
				{
					SkillSeidJsonData122 skillSeidJsonData = new SkillSeidJsonData122();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData122.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData122.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData122.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData122.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData122.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData122.OnInitFinishAction != null)
			{
				SkillSeidJsonData122.OnInitFinishAction();
			}
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400419F RID: 16799
		public static int SEIDID = 122;

		// Token: 0x040041A0 RID: 16800
		public static Dictionary<int, SkillSeidJsonData122> DataDict = new Dictionary<int, SkillSeidJsonData122>();

		// Token: 0x040041A1 RID: 16801
		public static List<SkillSeidJsonData122> DataList = new List<SkillSeidJsonData122>();

		// Token: 0x040041A2 RID: 16802
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData122.OnInitFinish);

		// Token: 0x040041A3 RID: 16803
		public int skillid;

		// Token: 0x040041A4 RID: 16804
		public int value1;
	}
}
