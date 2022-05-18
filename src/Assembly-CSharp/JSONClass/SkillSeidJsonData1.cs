using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C6F RID: 3183
	public class SkillSeidJsonData1 : IJSONClass
	{
		// Token: 0x06004D25 RID: 19749 RVA: 0x002092F8 File Offset: 0x002074F8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[1].list)
			{
				try
				{
					SkillSeidJsonData1 skillSeidJsonData = new SkillSeidJsonData1();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData1.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData1.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData1.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData1.OnInitFinishAction != null)
			{
				SkillSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C52 RID: 19538
		public static int SEIDID = 1;

		// Token: 0x04004C53 RID: 19539
		public static Dictionary<int, SkillSeidJsonData1> DataDict = new Dictionary<int, SkillSeidJsonData1>();

		// Token: 0x04004C54 RID: 19540
		public static List<SkillSeidJsonData1> DataList = new List<SkillSeidJsonData1>();

		// Token: 0x04004C55 RID: 19541
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData1.OnInitFinish);

		// Token: 0x04004C56 RID: 19542
		public int skillid;

		// Token: 0x04004C57 RID: 19543
		public int value1;
	}
}
