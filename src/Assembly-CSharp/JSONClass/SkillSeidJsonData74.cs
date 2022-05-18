using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC9 RID: 3273
	public class SkillSeidJsonData74 : IJSONClass
	{
		// Token: 0x06004E8C RID: 20108 RVA: 0x002101F4 File Offset: 0x0020E3F4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[74].list)
			{
				try
				{
					SkillSeidJsonData74 skillSeidJsonData = new SkillSeidJsonData74();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData74.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData74.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData74.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData74.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData74.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData74.OnInitFinishAction != null)
			{
				SkillSeidJsonData74.OnInitFinishAction();
			}
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EBC RID: 20156
		public static int SEIDID = 74;

		// Token: 0x04004EBD RID: 20157
		public static Dictionary<int, SkillSeidJsonData74> DataDict = new Dictionary<int, SkillSeidJsonData74>();

		// Token: 0x04004EBE RID: 20158
		public static List<SkillSeidJsonData74> DataList = new List<SkillSeidJsonData74>();

		// Token: 0x04004EBF RID: 20159
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData74.OnInitFinish);

		// Token: 0x04004EC0 RID: 20160
		public int skillid;

		// Token: 0x04004EC1 RID: 20161
		public int value1;
	}
}
