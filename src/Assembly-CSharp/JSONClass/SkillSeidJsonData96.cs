using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CDF RID: 3295
	public class SkillSeidJsonData96 : IJSONClass
	{
		// Token: 0x06004EE4 RID: 20196 RVA: 0x00211C68 File Offset: 0x0020FE68
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[96].list)
			{
				try
				{
					SkillSeidJsonData96 skillSeidJsonData = new SkillSeidJsonData96();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData96.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData96.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData96.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData96.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData96.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData96.OnInitFinishAction != null)
			{
				SkillSeidJsonData96.OnInitFinishAction();
			}
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F4D RID: 20301
		public static int SEIDID = 96;

		// Token: 0x04004F4E RID: 20302
		public static Dictionary<int, SkillSeidJsonData96> DataDict = new Dictionary<int, SkillSeidJsonData96>();

		// Token: 0x04004F4F RID: 20303
		public static List<SkillSeidJsonData96> DataList = new List<SkillSeidJsonData96>();

		// Token: 0x04004F50 RID: 20304
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData96.OnInitFinish);

		// Token: 0x04004F51 RID: 20305
		public int skillid;

		// Token: 0x04004F52 RID: 20306
		public int value1;
	}
}
