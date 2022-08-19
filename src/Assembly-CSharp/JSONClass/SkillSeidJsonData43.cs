using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200092B RID: 2347
	public class SkillSeidJsonData43 : IJSONClass
	{
		// Token: 0x060042BE RID: 17086 RVA: 0x001C7A10 File Offset: 0x001C5C10
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[43].list)
			{
				try
				{
					SkillSeidJsonData43 skillSeidJsonData = new SkillSeidJsonData43();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData43.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData43.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData43.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData43.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData43.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData43.OnInitFinishAction != null)
			{
				SkillSeidJsonData43.OnInitFinishAction();
			}
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042F9 RID: 17145
		public static int SEIDID = 43;

		// Token: 0x040042FA RID: 17146
		public static Dictionary<int, SkillSeidJsonData43> DataDict = new Dictionary<int, SkillSeidJsonData43>();

		// Token: 0x040042FB RID: 17147
		public static List<SkillSeidJsonData43> DataList = new List<SkillSeidJsonData43>();

		// Token: 0x040042FC RID: 17148
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData43.OnInitFinish);

		// Token: 0x040042FD RID: 17149
		public int skillid;

		// Token: 0x040042FE RID: 17150
		public List<int> value1 = new List<int>();

		// Token: 0x040042FF RID: 17151
		public List<int> value2 = new List<int>();
	}
}
