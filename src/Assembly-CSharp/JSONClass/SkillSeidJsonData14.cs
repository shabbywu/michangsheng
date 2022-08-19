using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008FC RID: 2300
	public class SkillSeidJsonData14 : IJSONClass
	{
		// Token: 0x06004203 RID: 16899 RVA: 0x001C366C File Offset: 0x001C186C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[14].list)
			{
				try
				{
					SkillSeidJsonData14 skillSeidJsonData = new SkillSeidJsonData14();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData14.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData14.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData14.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData14.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData14.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData14.OnInitFinishAction != null)
			{
				SkillSeidJsonData14.OnInitFinishAction();
			}
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041B9 RID: 16825
		public static int SEIDID = 14;

		// Token: 0x040041BA RID: 16826
		public static Dictionary<int, SkillSeidJsonData14> DataDict = new Dictionary<int, SkillSeidJsonData14>();

		// Token: 0x040041BB RID: 16827
		public static List<SkillSeidJsonData14> DataList = new List<SkillSeidJsonData14>();

		// Token: 0x040041BC RID: 16828
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData14.OnInitFinish);

		// Token: 0x040041BD RID: 16829
		public int skillid;

		// Token: 0x040041BE RID: 16830
		public int value1;
	}
}
