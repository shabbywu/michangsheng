using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200090D RID: 2317
	public class SkillSeidJsonData158 : IJSONClass
	{
		// Token: 0x06004246 RID: 16966 RVA: 0x001C4F54 File Offset: 0x001C3154
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[158].list)
			{
				try
				{
					SkillSeidJsonData158 skillSeidJsonData = new SkillSeidJsonData158();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value4 = jsonobject["value4"].ToList();
					if (SkillSeidJsonData158.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData158.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData158.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData158.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData158.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData158.OnInitFinishAction != null)
			{
				SkillSeidJsonData158.OnInitFinishAction();
			}
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004231 RID: 16945
		public static int SEIDID = 158;

		// Token: 0x04004232 RID: 16946
		public static Dictionary<int, SkillSeidJsonData158> DataDict = new Dictionary<int, SkillSeidJsonData158>();

		// Token: 0x04004233 RID: 16947
		public static List<SkillSeidJsonData158> DataList = new List<SkillSeidJsonData158>();

		// Token: 0x04004234 RID: 16948
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData158.OnInitFinish);

		// Token: 0x04004235 RID: 16949
		public int id;

		// Token: 0x04004236 RID: 16950
		public int value1;

		// Token: 0x04004237 RID: 16951
		public int value3;

		// Token: 0x04004238 RID: 16952
		public List<int> value2 = new List<int>();

		// Token: 0x04004239 RID: 16953
		public List<int> value4 = new List<int>();
	}
}
