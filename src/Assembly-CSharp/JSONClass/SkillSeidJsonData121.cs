using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F7 RID: 2295
	public class SkillSeidJsonData121 : IJSONClass
	{
		// Token: 0x060041EF RID: 16879 RVA: 0x001C2F44 File Offset: 0x001C1144
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[121].list)
			{
				try
				{
					SkillSeidJsonData121 skillSeidJsonData = new SkillSeidJsonData121();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData121.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData121.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData121.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData121.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData121.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData121.OnInitFinishAction != null)
			{
				SkillSeidJsonData121.OnInitFinishAction();
			}
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004197 RID: 16791
		public static int SEIDID = 121;

		// Token: 0x04004198 RID: 16792
		public static Dictionary<int, SkillSeidJsonData121> DataDict = new Dictionary<int, SkillSeidJsonData121>();

		// Token: 0x04004199 RID: 16793
		public static List<SkillSeidJsonData121> DataList = new List<SkillSeidJsonData121>();

		// Token: 0x0400419A RID: 16794
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData121.OnInitFinish);

		// Token: 0x0400419B RID: 16795
		public int skillid;

		// Token: 0x0400419C RID: 16796
		public int value1;

		// Token: 0x0400419D RID: 16797
		public int value2;

		// Token: 0x0400419E RID: 16798
		public int value3;
	}
}
