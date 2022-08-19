using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000935 RID: 2357
	public class SkillSeidJsonData54 : IJSONClass
	{
		// Token: 0x060042E6 RID: 17126 RVA: 0x001C890C File Offset: 0x001C6B0C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[54].list)
			{
				try
				{
					SkillSeidJsonData54 skillSeidJsonData = new SkillSeidJsonData54();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData54.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData54.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData54.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData54.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData54.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData54.OnInitFinishAction != null)
			{
				SkillSeidJsonData54.OnInitFinishAction();
			}
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004343 RID: 17219
		public static int SEIDID = 54;

		// Token: 0x04004344 RID: 17220
		public static Dictionary<int, SkillSeidJsonData54> DataDict = new Dictionary<int, SkillSeidJsonData54>();

		// Token: 0x04004345 RID: 17221
		public static List<SkillSeidJsonData54> DataList = new List<SkillSeidJsonData54>();

		// Token: 0x04004346 RID: 17222
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData54.OnInitFinish);

		// Token: 0x04004347 RID: 17223
		public int skillid;

		// Token: 0x04004348 RID: 17224
		public int value1;
	}
}
