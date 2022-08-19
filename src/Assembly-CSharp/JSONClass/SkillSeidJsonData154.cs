using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200090C RID: 2316
	public class SkillSeidJsonData154 : IJSONClass
	{
		// Token: 0x06004242 RID: 16962 RVA: 0x001C4DE0 File Offset: 0x001C2FE0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[154].list)
			{
				try
				{
					SkillSeidJsonData154 skillSeidJsonData = new SkillSeidJsonData154();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData154.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData154.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData154.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData154.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData154.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData154.OnInitFinishAction != null)
			{
				SkillSeidJsonData154.OnInitFinishAction();
			}
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400422A RID: 16938
		public static int SEIDID = 154;

		// Token: 0x0400422B RID: 16939
		public static Dictionary<int, SkillSeidJsonData154> DataDict = new Dictionary<int, SkillSeidJsonData154>();

		// Token: 0x0400422C RID: 16940
		public static List<SkillSeidJsonData154> DataList = new List<SkillSeidJsonData154>();

		// Token: 0x0400422D RID: 16941
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData154.OnInitFinish);

		// Token: 0x0400422E RID: 16942
		public int id;

		// Token: 0x0400422F RID: 16943
		public int value1;

		// Token: 0x04004230 RID: 16944
		public int value2;
	}
}
