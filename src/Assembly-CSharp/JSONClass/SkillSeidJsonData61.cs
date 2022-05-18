using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC0 RID: 3264
	public class SkillSeidJsonData61 : IJSONClass
	{
		// Token: 0x06004E68 RID: 20072 RVA: 0x0020F6E0 File Offset: 0x0020D8E0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[61].list)
			{
				try
				{
					SkillSeidJsonData61 skillSeidJsonData = new SkillSeidJsonData61();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData61.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData61.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData61.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData61.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData61.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData61.OnInitFinishAction != null)
			{
				SkillSeidJsonData61.OnInitFinishAction();
			}
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E7F RID: 20095
		public static int SEIDID = 61;

		// Token: 0x04004E80 RID: 20096
		public static Dictionary<int, SkillSeidJsonData61> DataDict = new Dictionary<int, SkillSeidJsonData61>();

		// Token: 0x04004E81 RID: 20097
		public static List<SkillSeidJsonData61> DataList = new List<SkillSeidJsonData61>();

		// Token: 0x04004E82 RID: 20098
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData61.OnInitFinish);

		// Token: 0x04004E83 RID: 20099
		public int skillid;

		// Token: 0x04004E84 RID: 20100
		public int value1;
	}
}
