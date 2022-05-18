using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB1 RID: 3249
	public class SkillSeidJsonData45 : IJSONClass
	{
		// Token: 0x06004E2C RID: 20012 RVA: 0x0020E45C File Offset: 0x0020C65C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[45].list)
			{
				try
				{
					SkillSeidJsonData45 skillSeidJsonData = new SkillSeidJsonData45();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData45.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData45.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData45.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData45.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData45.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData45.OnInitFinishAction != null)
			{
				SkillSeidJsonData45.OnInitFinishAction();
			}
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E17 RID: 19991
		public static int SEIDID = 45;

		// Token: 0x04004E18 RID: 19992
		public static Dictionary<int, SkillSeidJsonData45> DataDict = new Dictionary<int, SkillSeidJsonData45>();

		// Token: 0x04004E19 RID: 19993
		public static List<SkillSeidJsonData45> DataList = new List<SkillSeidJsonData45>();

		// Token: 0x04004E1A RID: 19994
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData45.OnInitFinish);

		// Token: 0x04004E1B RID: 19995
		public int skillid;

		// Token: 0x04004E1C RID: 19996
		public int value1;

		// Token: 0x04004E1D RID: 19997
		public int value2;

		// Token: 0x04004E1E RID: 19998
		public int value3;
	}
}
