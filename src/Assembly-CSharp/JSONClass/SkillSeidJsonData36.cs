using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000923 RID: 2339
	public class SkillSeidJsonData36 : IJSONClass
	{
		// Token: 0x0600429E RID: 17054 RVA: 0x001C6EBC File Offset: 0x001C50BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[36].list)
			{
				try
				{
					SkillSeidJsonData36 skillSeidJsonData = new SkillSeidJsonData36();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData36.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData36.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData36.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData36.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData36.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData36.OnInitFinishAction != null)
			{
				SkillSeidJsonData36.OnInitFinishAction();
			}
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042C3 RID: 17091
		public static int SEIDID = 36;

		// Token: 0x040042C4 RID: 17092
		public static Dictionary<int, SkillSeidJsonData36> DataDict = new Dictionary<int, SkillSeidJsonData36>();

		// Token: 0x040042C5 RID: 17093
		public static List<SkillSeidJsonData36> DataList = new List<SkillSeidJsonData36>();

		// Token: 0x040042C6 RID: 17094
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData36.OnInitFinish);

		// Token: 0x040042C7 RID: 17095
		public int skillid;

		// Token: 0x040042C8 RID: 17096
		public int value1;

		// Token: 0x040042C9 RID: 17097
		public int value2;
	}
}
