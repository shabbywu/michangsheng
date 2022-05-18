using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB3 RID: 3251
	public class SkillSeidJsonData47 : IJSONClass
	{
		// Token: 0x06004E34 RID: 20020 RVA: 0x0020E704 File Offset: 0x0020C904
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[47].list)
			{
				try
				{
					SkillSeidJsonData47 skillSeidJsonData = new SkillSeidJsonData47();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData47.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData47.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData47.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData47.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData47.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData47.OnInitFinishAction != null)
			{
				SkillSeidJsonData47.OnInitFinishAction();
			}
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E27 RID: 20007
		public static int SEIDID = 47;

		// Token: 0x04004E28 RID: 20008
		public static Dictionary<int, SkillSeidJsonData47> DataDict = new Dictionary<int, SkillSeidJsonData47>();

		// Token: 0x04004E29 RID: 20009
		public static List<SkillSeidJsonData47> DataList = new List<SkillSeidJsonData47>();

		// Token: 0x04004E2A RID: 20010
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData47.OnInitFinish);

		// Token: 0x04004E2B RID: 20011
		public int skillid;

		// Token: 0x04004E2C RID: 20012
		public int value1;

		// Token: 0x04004E2D RID: 20013
		public int value2;

		// Token: 0x04004E2E RID: 20014
		public int value3;
	}
}
