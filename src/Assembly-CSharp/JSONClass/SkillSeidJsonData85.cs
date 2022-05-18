using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD4 RID: 3284
	public class SkillSeidJsonData85 : IJSONClass
	{
		// Token: 0x06004EB8 RID: 20152 RVA: 0x00210F24 File Offset: 0x0020F124
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[85].list)
			{
				try
				{
					SkillSeidJsonData85 skillSeidJsonData = new SkillSeidJsonData85();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData85.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData85.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData85.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData85.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData85.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData85.OnInitFinishAction != null)
			{
				SkillSeidJsonData85.OnInitFinishAction();
			}
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F04 RID: 20228
		public static int SEIDID = 85;

		// Token: 0x04004F05 RID: 20229
		public static Dictionary<int, SkillSeidJsonData85> DataDict = new Dictionary<int, SkillSeidJsonData85>();

		// Token: 0x04004F06 RID: 20230
		public static List<SkillSeidJsonData85> DataList = new List<SkillSeidJsonData85>();

		// Token: 0x04004F07 RID: 20231
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData85.OnInitFinish);

		// Token: 0x04004F08 RID: 20232
		public int skillid;

		// Token: 0x04004F09 RID: 20233
		public int value1;

		// Token: 0x04004F0A RID: 20234
		public int value2;
	}
}
