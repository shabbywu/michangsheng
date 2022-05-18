using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C73 RID: 3187
	public class SkillSeidJsonData104 : IJSONClass
	{
		// Token: 0x06004D35 RID: 19765 RVA: 0x002097D8 File Offset: 0x002079D8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[104].list)
			{
				try
				{
					SkillSeidJsonData104 skillSeidJsonData = new SkillSeidJsonData104();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData104.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData104.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData104.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData104.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData104.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData104.OnInitFinishAction != null)
			{
				SkillSeidJsonData104.OnInitFinishAction();
			}
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C6D RID: 19565
		public static int SEIDID = 104;

		// Token: 0x04004C6E RID: 19566
		public static Dictionary<int, SkillSeidJsonData104> DataDict = new Dictionary<int, SkillSeidJsonData104>();

		// Token: 0x04004C6F RID: 19567
		public static List<SkillSeidJsonData104> DataList = new List<SkillSeidJsonData104>();

		// Token: 0x04004C70 RID: 19568
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData104.OnInitFinish);

		// Token: 0x04004C71 RID: 19569
		public int skillid;

		// Token: 0x04004C72 RID: 19570
		public int value1;
	}
}
