using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008FA RID: 2298
	public class SkillSeidJsonData124 : IJSONClass
	{
		// Token: 0x060041FB RID: 16891 RVA: 0x001C33A8 File Offset: 0x001C15A8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[124].list)
			{
				try
				{
					SkillSeidJsonData124 skillSeidJsonData = new SkillSeidJsonData124();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData124.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData124.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData124.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData124.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData124.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData124.OnInitFinishAction != null)
			{
				SkillSeidJsonData124.OnInitFinishAction();
			}
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041AC RID: 16812
		public static int SEIDID = 124;

		// Token: 0x040041AD RID: 16813
		public static Dictionary<int, SkillSeidJsonData124> DataDict = new Dictionary<int, SkillSeidJsonData124>();

		// Token: 0x040041AE RID: 16814
		public static List<SkillSeidJsonData124> DataList = new List<SkillSeidJsonData124>();

		// Token: 0x040041AF RID: 16815
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData124.OnInitFinish);

		// Token: 0x040041B0 RID: 16816
		public int skillid;

		// Token: 0x040041B1 RID: 16817
		public int value1;
	}
}
