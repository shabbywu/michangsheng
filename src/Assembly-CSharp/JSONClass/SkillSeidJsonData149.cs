using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000906 RID: 2310
	public class SkillSeidJsonData149 : IJSONClass
	{
		// Token: 0x0600422B RID: 16939 RVA: 0x001C44E4 File Offset: 0x001C26E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[149].list)
			{
				try
				{
					SkillSeidJsonData149 skillSeidJsonData = new SkillSeidJsonData149();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (SkillSeidJsonData149.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData149.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData149.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData149.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData149.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData149.OnInitFinishAction != null)
			{
				SkillSeidJsonData149.OnInitFinishAction();
			}
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041FB RID: 16891
		public static int SEIDID = 149;

		// Token: 0x040041FC RID: 16892
		public static Dictionary<int, SkillSeidJsonData149> DataDict = new Dictionary<int, SkillSeidJsonData149>();

		// Token: 0x040041FD RID: 16893
		public static List<SkillSeidJsonData149> DataList = new List<SkillSeidJsonData149>();

		// Token: 0x040041FE RID: 16894
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData149.OnInitFinish);

		// Token: 0x040041FF RID: 16895
		public int skillid;

		// Token: 0x04004200 RID: 16896
		public int target;

		// Token: 0x04004201 RID: 16897
		public int value1;

		// Token: 0x04004202 RID: 16898
		public int value2;

		// Token: 0x04004203 RID: 16899
		public string panduan;
	}
}
