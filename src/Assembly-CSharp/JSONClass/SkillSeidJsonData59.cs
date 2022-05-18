using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CBE RID: 3262
	public class SkillSeidJsonData59 : IJSONClass
	{
		// Token: 0x06004E60 RID: 20064 RVA: 0x0020F490 File Offset: 0x0020D690
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[59].list)
			{
				try
				{
					SkillSeidJsonData59 skillSeidJsonData = new SkillSeidJsonData59();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData59.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData59.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData59.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData59.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData59.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData59.OnInitFinishAction != null)
			{
				SkillSeidJsonData59.OnInitFinishAction();
			}
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E73 RID: 20083
		public static int SEIDID = 59;

		// Token: 0x04004E74 RID: 20084
		public static Dictionary<int, SkillSeidJsonData59> DataDict = new Dictionary<int, SkillSeidJsonData59>();

		// Token: 0x04004E75 RID: 20085
		public static List<SkillSeidJsonData59> DataList = new List<SkillSeidJsonData59>();

		// Token: 0x04004E76 RID: 20086
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData59.OnInitFinish);

		// Token: 0x04004E77 RID: 20087
		public int skillid;

		// Token: 0x04004E78 RID: 20088
		public int value1;
	}
}
