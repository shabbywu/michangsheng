using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000938 RID: 2360
	public class SkillSeidJsonData57 : IJSONClass
	{
		// Token: 0x060042F2 RID: 17138 RVA: 0x001C8D28 File Offset: 0x001C6F28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[57].list)
			{
				try
				{
					SkillSeidJsonData57 skillSeidJsonData = new SkillSeidJsonData57();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData57.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData57.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData57.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData57.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData57.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData57.OnInitFinishAction != null)
			{
				SkillSeidJsonData57.OnInitFinishAction();
			}
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004356 RID: 17238
		public static int SEIDID = 57;

		// Token: 0x04004357 RID: 17239
		public static Dictionary<int, SkillSeidJsonData57> DataDict = new Dictionary<int, SkillSeidJsonData57>();

		// Token: 0x04004358 RID: 17240
		public static List<SkillSeidJsonData57> DataList = new List<SkillSeidJsonData57>();

		// Token: 0x04004359 RID: 17241
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData57.OnInitFinish);

		// Token: 0x0400435A RID: 17242
		public int skillid;

		// Token: 0x0400435B RID: 17243
		public List<int> value1 = new List<int>();

		// Token: 0x0400435C RID: 17244
		public List<int> value2 = new List<int>();
	}
}
