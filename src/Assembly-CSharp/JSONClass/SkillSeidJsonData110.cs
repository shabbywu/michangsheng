using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008ED RID: 2285
	public class SkillSeidJsonData110 : IJSONClass
	{
		// Token: 0x060041C7 RID: 16839 RVA: 0x001C2104 File Offset: 0x001C0304
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[110].list)
			{
				try
				{
					SkillSeidJsonData110 skillSeidJsonData = new SkillSeidJsonData110();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData110.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData110.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData110.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData110.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData110.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData110.OnInitFinishAction != null)
			{
				SkillSeidJsonData110.OnInitFinishAction();
			}
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004155 RID: 16725
		public static int SEIDID = 110;

		// Token: 0x04004156 RID: 16726
		public static Dictionary<int, SkillSeidJsonData110> DataDict = new Dictionary<int, SkillSeidJsonData110>();

		// Token: 0x04004157 RID: 16727
		public static List<SkillSeidJsonData110> DataList = new List<SkillSeidJsonData110>();

		// Token: 0x04004158 RID: 16728
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData110.OnInitFinish);

		// Token: 0x04004159 RID: 16729
		public int id;

		// Token: 0x0400415A RID: 16730
		public int value1;
	}
}
