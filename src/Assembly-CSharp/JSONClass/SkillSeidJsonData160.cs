using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200090F RID: 2319
	public class SkillSeidJsonData160 : IJSONClass
	{
		// Token: 0x0600424E RID: 16974 RVA: 0x001C529C File Offset: 0x001C349C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[160].list)
			{
				try
				{
					SkillSeidJsonData160 skillSeidJsonData = new SkillSeidJsonData160();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData160.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData160.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData160.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData160.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData160.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData160.OnInitFinishAction != null)
			{
				SkillSeidJsonData160.OnInitFinishAction();
			}
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004241 RID: 16961
		public static int SEIDID = 160;

		// Token: 0x04004242 RID: 16962
		public static Dictionary<int, SkillSeidJsonData160> DataDict = new Dictionary<int, SkillSeidJsonData160>();

		// Token: 0x04004243 RID: 16963
		public static List<SkillSeidJsonData160> DataList = new List<SkillSeidJsonData160>();

		// Token: 0x04004244 RID: 16964
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData160.OnInitFinish);

		// Token: 0x04004245 RID: 16965
		public int id;

		// Token: 0x04004246 RID: 16966
		public int value1;
	}
}
