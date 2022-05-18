using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB7 RID: 3255
	public class SkillSeidJsonData51 : IJSONClass
	{
		// Token: 0x06004E44 RID: 20036 RVA: 0x0020EC24 File Offset: 0x0020CE24
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[51].list)
			{
				try
				{
					SkillSeidJsonData51 skillSeidJsonData = new SkillSeidJsonData51();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (SkillSeidJsonData51.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData51.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData51.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData51.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData51.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData51.OnInitFinishAction != null)
			{
				SkillSeidJsonData51.OnInitFinishAction();
			}
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E45 RID: 20037
		public static int SEIDID = 51;

		// Token: 0x04004E46 RID: 20038
		public static Dictionary<int, SkillSeidJsonData51> DataDict = new Dictionary<int, SkillSeidJsonData51>();

		// Token: 0x04004E47 RID: 20039
		public static List<SkillSeidJsonData51> DataList = new List<SkillSeidJsonData51>();

		// Token: 0x04004E48 RID: 20040
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData51.OnInitFinish);

		// Token: 0x04004E49 RID: 20041
		public int skillid;

		// Token: 0x04004E4A RID: 20042
		public List<int> value1 = new List<int>();

		// Token: 0x04004E4B RID: 20043
		public List<int> value2 = new List<int>();

		// Token: 0x04004E4C RID: 20044
		public List<int> value3 = new List<int>();
	}
}
