using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000901 RID: 2305
	public class SkillSeidJsonData144 : IJSONClass
	{
		// Token: 0x06004217 RID: 16919 RVA: 0x001C3D54 File Offset: 0x001C1F54
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[144].list)
			{
				try
				{
					SkillSeidJsonData144 skillSeidJsonData = new SkillSeidJsonData144();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData144.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData144.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData144.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData144.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData144.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData144.OnInitFinishAction != null)
			{
				SkillSeidJsonData144.OnInitFinishAction();
			}
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041D7 RID: 16855
		public static int SEIDID = 144;

		// Token: 0x040041D8 RID: 16856
		public static Dictionary<int, SkillSeidJsonData144> DataDict = new Dictionary<int, SkillSeidJsonData144>();

		// Token: 0x040041D9 RID: 16857
		public static List<SkillSeidJsonData144> DataList = new List<SkillSeidJsonData144>();

		// Token: 0x040041DA RID: 16858
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData144.OnInitFinish);

		// Token: 0x040041DB RID: 16859
		public int skillid;

		// Token: 0x040041DC RID: 16860
		public int value1;
	}
}
