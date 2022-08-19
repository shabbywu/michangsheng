using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000905 RID: 2309
	public class SkillSeidJsonData148 : IJSONClass
	{
		// Token: 0x06004227 RID: 16935 RVA: 0x001C432C File Offset: 0x001C252C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[148].list)
			{
				try
				{
					SkillSeidJsonData148 skillSeidJsonData = new SkillSeidJsonData148();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (SkillSeidJsonData148.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData148.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData148.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData148.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData148.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData148.OnInitFinishAction != null)
			{
				SkillSeidJsonData148.OnInitFinishAction();
			}
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041F2 RID: 16882
		public static int SEIDID = 148;

		// Token: 0x040041F3 RID: 16883
		public static Dictionary<int, SkillSeidJsonData148> DataDict = new Dictionary<int, SkillSeidJsonData148>();

		// Token: 0x040041F4 RID: 16884
		public static List<SkillSeidJsonData148> DataList = new List<SkillSeidJsonData148>();

		// Token: 0x040041F5 RID: 16885
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData148.OnInitFinish);

		// Token: 0x040041F6 RID: 16886
		public int skillid;

		// Token: 0x040041F7 RID: 16887
		public int target;

		// Token: 0x040041F8 RID: 16888
		public int value1;

		// Token: 0x040041F9 RID: 16889
		public int value2;

		// Token: 0x040041FA RID: 16890
		public string panduan;
	}
}
