using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000930 RID: 2352
	public class SkillSeidJsonData48 : IJSONClass
	{
		// Token: 0x060042D2 RID: 17106 RVA: 0x001C8190 File Offset: 0x001C6390
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[48].list)
			{
				try
				{
					SkillSeidJsonData48 skillSeidJsonData = new SkillSeidJsonData48();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData48.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData48.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData48.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData48.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData48.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData48.OnInitFinishAction != null)
			{
				SkillSeidJsonData48.OnInitFinishAction();
			}
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400431F RID: 17183
		public static int SEIDID = 48;

		// Token: 0x04004320 RID: 17184
		public static Dictionary<int, SkillSeidJsonData48> DataDict = new Dictionary<int, SkillSeidJsonData48>();

		// Token: 0x04004321 RID: 17185
		public static List<SkillSeidJsonData48> DataList = new List<SkillSeidJsonData48>();

		// Token: 0x04004322 RID: 17186
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData48.OnInitFinish);

		// Token: 0x04004323 RID: 17187
		public int skillid;

		// Token: 0x04004324 RID: 17188
		public int value1;

		// Token: 0x04004325 RID: 17189
		public int value2;

		// Token: 0x04004326 RID: 17190
		public int value3;
	}
}
