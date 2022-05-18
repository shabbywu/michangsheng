using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB6 RID: 3254
	public class SkillSeidJsonData50 : IJSONClass
	{
		// Token: 0x06004E40 RID: 20032 RVA: 0x0020EAE8 File Offset: 0x0020CCE8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[50].list)
			{
				try
				{
					SkillSeidJsonData50 skillSeidJsonData = new SkillSeidJsonData50();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData50.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData50.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData50.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData50.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData50.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData50.OnInitFinishAction != null)
			{
				SkillSeidJsonData50.OnInitFinishAction();
			}
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E3E RID: 20030
		public static int SEIDID = 50;

		// Token: 0x04004E3F RID: 20031
		public static Dictionary<int, SkillSeidJsonData50> DataDict = new Dictionary<int, SkillSeidJsonData50>();

		// Token: 0x04004E40 RID: 20032
		public static List<SkillSeidJsonData50> DataList = new List<SkillSeidJsonData50>();

		// Token: 0x04004E41 RID: 20033
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData50.OnInitFinish);

		// Token: 0x04004E42 RID: 20034
		public int skillid;

		// Token: 0x04004E43 RID: 20035
		public int value1;

		// Token: 0x04004E44 RID: 20036
		public int value2;
	}
}
