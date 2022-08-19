using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008EE RID: 2286
	public class SkillSeidJsonData111 : IJSONClass
	{
		// Token: 0x060041CB RID: 16843 RVA: 0x001C225C File Offset: 0x001C045C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[111].list)
			{
				try
				{
					SkillSeidJsonData111 skillSeidJsonData = new SkillSeidJsonData111();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData111.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData111.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData111.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData111.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData111.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData111.OnInitFinishAction != null)
			{
				SkillSeidJsonData111.OnInitFinishAction();
			}
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400415B RID: 16731
		public static int SEIDID = 111;

		// Token: 0x0400415C RID: 16732
		public static Dictionary<int, SkillSeidJsonData111> DataDict = new Dictionary<int, SkillSeidJsonData111>();

		// Token: 0x0400415D RID: 16733
		public static List<SkillSeidJsonData111> DataList = new List<SkillSeidJsonData111>();

		// Token: 0x0400415E RID: 16734
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData111.OnInitFinish);

		// Token: 0x0400415F RID: 16735
		public int id;

		// Token: 0x04004160 RID: 16736
		public int value1;
	}
}
