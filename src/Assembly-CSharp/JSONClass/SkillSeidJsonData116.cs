using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F1 RID: 2289
	public class SkillSeidJsonData116 : IJSONClass
	{
		// Token: 0x060041D7 RID: 16855 RVA: 0x001C26C4 File Offset: 0x001C08C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[116].list)
			{
				try
				{
					SkillSeidJsonData116 skillSeidJsonData = new SkillSeidJsonData116();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData116.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData116.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData116.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData116.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData116.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData116.OnInitFinishAction != null)
			{
				SkillSeidJsonData116.OnInitFinishAction();
			}
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400416F RID: 16751
		public static int SEIDID = 116;

		// Token: 0x04004170 RID: 16752
		public static Dictionary<int, SkillSeidJsonData116> DataDict = new Dictionary<int, SkillSeidJsonData116>();

		// Token: 0x04004171 RID: 16753
		public static List<SkillSeidJsonData116> DataList = new List<SkillSeidJsonData116>();

		// Token: 0x04004172 RID: 16754
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData116.OnInitFinish);

		// Token: 0x04004173 RID: 16755
		public int skillid;

		// Token: 0x04004174 RID: 16756
		public int value1;

		// Token: 0x04004175 RID: 16757
		public int value2;

		// Token: 0x04004176 RID: 16758
		public int value3;
	}
}
