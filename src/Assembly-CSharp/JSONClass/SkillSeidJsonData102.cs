using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C72 RID: 3186
	public class SkillSeidJsonData102 : IJSONClass
	{
		// Token: 0x06004D31 RID: 19761 RVA: 0x0020969C File Offset: 0x0020789C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[102].list)
			{
				try
				{
					SkillSeidJsonData102 skillSeidJsonData = new SkillSeidJsonData102();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData102.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData102.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData102.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData102.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData102.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData102.OnInitFinishAction != null)
			{
				SkillSeidJsonData102.OnInitFinishAction();
			}
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C66 RID: 19558
		public static int SEIDID = 102;

		// Token: 0x04004C67 RID: 19559
		public static Dictionary<int, SkillSeidJsonData102> DataDict = new Dictionary<int, SkillSeidJsonData102>();

		// Token: 0x04004C68 RID: 19560
		public static List<SkillSeidJsonData102> DataList = new List<SkillSeidJsonData102>();

		// Token: 0x04004C69 RID: 19561
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData102.OnInitFinish);

		// Token: 0x04004C6A RID: 19562
		public int skillid;

		// Token: 0x04004C6B RID: 19563
		public List<int> value1 = new List<int>();

		// Token: 0x04004C6C RID: 19564
		public List<int> value2 = new List<int>();
	}
}
