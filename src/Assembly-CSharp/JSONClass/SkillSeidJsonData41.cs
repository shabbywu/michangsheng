using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000929 RID: 2345
	public class SkillSeidJsonData41 : IJSONClass
	{
		// Token: 0x060042B6 RID: 17078 RVA: 0x001C7738 File Offset: 0x001C5938
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[41].list)
			{
				try
				{
					SkillSeidJsonData41 skillSeidJsonData = new SkillSeidJsonData41();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData41.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData41.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData41.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData41.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData41.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData41.OnInitFinishAction != null)
			{
				SkillSeidJsonData41.OnInitFinishAction();
			}
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042EB RID: 17131
		public static int SEIDID = 41;

		// Token: 0x040042EC RID: 17132
		public static Dictionary<int, SkillSeidJsonData41> DataDict = new Dictionary<int, SkillSeidJsonData41>();

		// Token: 0x040042ED RID: 17133
		public static List<SkillSeidJsonData41> DataList = new List<SkillSeidJsonData41>();

		// Token: 0x040042EE RID: 17134
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData41.OnInitFinish);

		// Token: 0x040042EF RID: 17135
		public int skillid;

		// Token: 0x040042F0 RID: 17136
		public int value1;

		// Token: 0x040042F1 RID: 17137
		public int value2;
	}
}
