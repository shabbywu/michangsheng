using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CDC RID: 3292
	public class SkillSeidJsonData92 : IJSONClass
	{
		// Token: 0x06004ED8 RID: 20184 RVA: 0x002118C8 File Offset: 0x0020FAC8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[92].list)
			{
				try
				{
					SkillSeidJsonData92 skillSeidJsonData = new SkillSeidJsonData92();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData92.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData92.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData92.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData92.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData92.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData92.OnInitFinishAction != null)
			{
				SkillSeidJsonData92.OnInitFinishAction();
			}
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F39 RID: 20281
		public static int SEIDID = 92;

		// Token: 0x04004F3A RID: 20282
		public static Dictionary<int, SkillSeidJsonData92> DataDict = new Dictionary<int, SkillSeidJsonData92>();

		// Token: 0x04004F3B RID: 20283
		public static List<SkillSeidJsonData92> DataList = new List<SkillSeidJsonData92>();

		// Token: 0x04004F3C RID: 20284
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData92.OnInitFinish);

		// Token: 0x04004F3D RID: 20285
		public int skillid;

		// Token: 0x04004F3E RID: 20286
		public int value1;

		// Token: 0x04004F3F RID: 20287
		public int value2;
	}
}
