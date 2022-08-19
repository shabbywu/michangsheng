using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000952 RID: 2386
	public class SkillSeidJsonData87 : IJSONClass
	{
		// Token: 0x0600435A RID: 17242 RVA: 0x001CB1F0 File Offset: 0x001C93F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[87].list)
			{
				try
				{
					SkillSeidJsonData87 skillSeidJsonData = new SkillSeidJsonData87();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData87.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData87.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData87.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData87.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData87.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData87.OnInitFinishAction != null)
			{
				SkillSeidJsonData87.OnInitFinishAction();
			}
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004401 RID: 17409
		public static int SEIDID = 87;

		// Token: 0x04004402 RID: 17410
		public static Dictionary<int, SkillSeidJsonData87> DataDict = new Dictionary<int, SkillSeidJsonData87>();

		// Token: 0x04004403 RID: 17411
		public static List<SkillSeidJsonData87> DataList = new List<SkillSeidJsonData87>();

		// Token: 0x04004404 RID: 17412
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData87.OnInitFinish);

		// Token: 0x04004405 RID: 17413
		public int skillid;

		// Token: 0x04004406 RID: 17414
		public int value1;

		// Token: 0x04004407 RID: 17415
		public int value2;
	}
}
