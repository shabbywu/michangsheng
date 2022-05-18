using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CAF RID: 3247
	public class SkillSeidJsonData43 : IJSONClass
	{
		// Token: 0x06004E24 RID: 20004 RVA: 0x0020E1E4 File Offset: 0x0020C3E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[43].list)
			{
				try
				{
					SkillSeidJsonData43 skillSeidJsonData = new SkillSeidJsonData43();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData43.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData43.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData43.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData43.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData43.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData43.OnInitFinishAction != null)
			{
				SkillSeidJsonData43.OnInitFinishAction();
			}
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E09 RID: 19977
		public static int SEIDID = 43;

		// Token: 0x04004E0A RID: 19978
		public static Dictionary<int, SkillSeidJsonData43> DataDict = new Dictionary<int, SkillSeidJsonData43>();

		// Token: 0x04004E0B RID: 19979
		public static List<SkillSeidJsonData43> DataList = new List<SkillSeidJsonData43>();

		// Token: 0x04004E0C RID: 19980
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData43.OnInitFinish);

		// Token: 0x04004E0D RID: 19981
		public int skillid;

		// Token: 0x04004E0E RID: 19982
		public List<int> value1 = new List<int>();

		// Token: 0x04004E0F RID: 19983
		public List<int> value2 = new List<int>();
	}
}
