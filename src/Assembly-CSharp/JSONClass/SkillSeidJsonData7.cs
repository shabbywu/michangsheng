using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC7 RID: 3271
	public class SkillSeidJsonData7 : IJSONClass
	{
		// Token: 0x06004E84 RID: 20100 RVA: 0x0020FF64 File Offset: 0x0020E164
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[7].list)
			{
				try
				{
					SkillSeidJsonData7 skillSeidJsonData = new SkillSeidJsonData7();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData7.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData7.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData7.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData7.OnInitFinishAction != null)
			{
				SkillSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EAD RID: 20141
		public static int SEIDID = 7;

		// Token: 0x04004EAE RID: 20142
		public static Dictionary<int, SkillSeidJsonData7> DataDict = new Dictionary<int, SkillSeidJsonData7>();

		// Token: 0x04004EAF RID: 20143
		public static List<SkillSeidJsonData7> DataList = new List<SkillSeidJsonData7>();

		// Token: 0x04004EB0 RID: 20144
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData7.OnInitFinish);

		// Token: 0x04004EB1 RID: 20145
		public int skillid;

		// Token: 0x04004EB2 RID: 20146
		public List<int> value1 = new List<int>();

		// Token: 0x04004EB3 RID: 20147
		public List<int> value2 = new List<int>();
	}
}
