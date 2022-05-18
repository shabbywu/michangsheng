using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA2 RID: 3234
	public class SkillSeidJsonData31 : IJSONClass
	{
		// Token: 0x06004DF0 RID: 19952 RVA: 0x0020D228 File Offset: 0x0020B428
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[31].list)
			{
				try
				{
					SkillSeidJsonData31 skillSeidJsonData = new SkillSeidJsonData31();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData31.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData31.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData31.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData31.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData31.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData31.OnInitFinishAction != null)
			{
				SkillSeidJsonData31.OnInitFinishAction();
			}
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DB2 RID: 19890
		public static int SEIDID = 31;

		// Token: 0x04004DB3 RID: 19891
		public static Dictionary<int, SkillSeidJsonData31> DataDict = new Dictionary<int, SkillSeidJsonData31>();

		// Token: 0x04004DB4 RID: 19892
		public static List<SkillSeidJsonData31> DataList = new List<SkillSeidJsonData31>();

		// Token: 0x04004DB5 RID: 19893
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData31.OnInitFinish);

		// Token: 0x04004DB6 RID: 19894
		public int skillid;

		// Token: 0x04004DB7 RID: 19895
		public List<int> value1 = new List<int>();

		// Token: 0x04004DB8 RID: 19896
		public List<int> value2 = new List<int>();
	}
}
