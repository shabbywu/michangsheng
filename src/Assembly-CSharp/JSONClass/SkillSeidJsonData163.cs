using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000911 RID: 2321
	public class SkillSeidJsonData163 : IJSONClass
	{
		// Token: 0x06004256 RID: 16982 RVA: 0x001C5588 File Offset: 0x001C3788
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[163].list)
			{
				try
				{
					SkillSeidJsonData163 skillSeidJsonData = new SkillSeidJsonData163();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData163.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData163.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData163.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData163.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData163.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData163.OnInitFinishAction != null)
			{
				SkillSeidJsonData163.OnInitFinishAction();
			}
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400424F RID: 16975
		public static int SEIDID = 163;

		// Token: 0x04004250 RID: 16976
		public static Dictionary<int, SkillSeidJsonData163> DataDict = new Dictionary<int, SkillSeidJsonData163>();

		// Token: 0x04004251 RID: 16977
		public static List<SkillSeidJsonData163> DataList = new List<SkillSeidJsonData163>();

		// Token: 0x04004252 RID: 16978
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData163.OnInitFinish);

		// Token: 0x04004253 RID: 16979
		public int id;

		// Token: 0x04004254 RID: 16980
		public int value1;
	}
}
