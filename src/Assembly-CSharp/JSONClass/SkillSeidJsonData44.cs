using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB0 RID: 3248
	public class SkillSeidJsonData44 : IJSONClass
	{
		// Token: 0x06004E28 RID: 20008 RVA: 0x0020E320 File Offset: 0x0020C520
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[44].list)
			{
				try
				{
					SkillSeidJsonData44 skillSeidJsonData = new SkillSeidJsonData44();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData44.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData44.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData44.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData44.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData44.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData44.OnInitFinishAction != null)
			{
				SkillSeidJsonData44.OnInitFinishAction();
			}
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E10 RID: 19984
		public static int SEIDID = 44;

		// Token: 0x04004E11 RID: 19985
		public static Dictionary<int, SkillSeidJsonData44> DataDict = new Dictionary<int, SkillSeidJsonData44>();

		// Token: 0x04004E12 RID: 19986
		public static List<SkillSeidJsonData44> DataList = new List<SkillSeidJsonData44>();

		// Token: 0x04004E13 RID: 19987
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData44.OnInitFinish);

		// Token: 0x04004E14 RID: 19988
		public int skillid;

		// Token: 0x04004E15 RID: 19989
		public int value1;

		// Token: 0x04004E16 RID: 19990
		public int value2;
	}
}
