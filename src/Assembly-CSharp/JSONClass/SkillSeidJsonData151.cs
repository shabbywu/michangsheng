using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C90 RID: 3216
	public class SkillSeidJsonData151 : IJSONClass
	{
		// Token: 0x06004DA9 RID: 19881 RVA: 0x0020BC14 File Offset: 0x00209E14
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[151].list)
			{
				try
				{
					SkillSeidJsonData151 skillSeidJsonData = new SkillSeidJsonData151();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (SkillSeidJsonData151.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData151.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData151.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData151.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData151.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData151.OnInitFinishAction != null)
			{
				SkillSeidJsonData151.OnInitFinishAction();
			}
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D35 RID: 19765
		public static int SEIDID = 151;

		// Token: 0x04004D36 RID: 19766
		public static Dictionary<int, SkillSeidJsonData151> DataDict = new Dictionary<int, SkillSeidJsonData151>();

		// Token: 0x04004D37 RID: 19767
		public static List<SkillSeidJsonData151> DataList = new List<SkillSeidJsonData151>();

		// Token: 0x04004D38 RID: 19768
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData151.OnInitFinish);

		// Token: 0x04004D39 RID: 19769
		public int skillid;

		// Token: 0x04004D3A RID: 19770
		public int value1;

		// Token: 0x04004D3B RID: 19771
		public List<int> value2 = new List<int>();

		// Token: 0x04004D3C RID: 19772
		public List<int> value3 = new List<int>();
	}
}
