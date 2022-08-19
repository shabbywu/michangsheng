using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008EC RID: 2284
	public class SkillSeidJsonData11 : IJSONClass
	{
		// Token: 0x060041C3 RID: 16835 RVA: 0x001C1F80 File Offset: 0x001C0180
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[11].list)
			{
				try
				{
					SkillSeidJsonData11 skillSeidJsonData = new SkillSeidJsonData11();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData11.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData11.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData11.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData11.OnInitFinishAction != null)
			{
				SkillSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400414D RID: 16717
		public static int SEIDID = 11;

		// Token: 0x0400414E RID: 16718
		public static Dictionary<int, SkillSeidJsonData11> DataDict = new Dictionary<int, SkillSeidJsonData11>();

		// Token: 0x0400414F RID: 16719
		public static List<SkillSeidJsonData11> DataList = new List<SkillSeidJsonData11>();

		// Token: 0x04004150 RID: 16720
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData11.OnInitFinish);

		// Token: 0x04004151 RID: 16721
		public int skillid;

		// Token: 0x04004152 RID: 16722
		public int value1;

		// Token: 0x04004153 RID: 16723
		public int value2;

		// Token: 0x04004154 RID: 16724
		public int value3;
	}
}
