using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000912 RID: 2322
	public class SkillSeidJsonData164 : IJSONClass
	{
		// Token: 0x0600425A RID: 16986 RVA: 0x001C56E8 File Offset: 0x001C38E8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[164].list)
			{
				try
				{
					SkillSeidJsonData164 skillSeidJsonData = new SkillSeidJsonData164();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (SkillSeidJsonData164.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData164.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData164.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData164.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData164.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData164.OnInitFinishAction != null)
			{
				SkillSeidJsonData164.OnInitFinishAction();
			}
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004255 RID: 16981
		public static int SEIDID = 164;

		// Token: 0x04004256 RID: 16982
		public static Dictionary<int, SkillSeidJsonData164> DataDict = new Dictionary<int, SkillSeidJsonData164>();

		// Token: 0x04004257 RID: 16983
		public static List<SkillSeidJsonData164> DataList = new List<SkillSeidJsonData164>();

		// Token: 0x04004258 RID: 16984
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData164.OnInitFinish);

		// Token: 0x04004259 RID: 16985
		public int skillid;

		// Token: 0x0400425A RID: 16986
		public int target;

		// Token: 0x0400425B RID: 16987
		public int value1;

		// Token: 0x0400425C RID: 16988
		public string panduan;
	}
}
