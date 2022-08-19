using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200095E RID: 2398
	public class SkillTextInfoJsonData : IJSONClass
	{
		// Token: 0x0600438A RID: 17290 RVA: 0x001CC288 File Offset: 0x001CA488
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillTextInfoJsonData.list)
			{
				try
				{
					SkillTextInfoJsonData skillTextInfoJsonData = new SkillTextInfoJsonData();
					skillTextInfoJsonData.id = jsonobject["id"].I;
					skillTextInfoJsonData.name = jsonobject["name"].Str;
					skillTextInfoJsonData.descr = jsonobject["descr"].Str;
					if (SkillTextInfoJsonData.DataDict.ContainsKey(skillTextInfoJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillTextInfoJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillTextInfoJsonData.id));
					}
					else
					{
						SkillTextInfoJsonData.DataDict.Add(skillTextInfoJsonData.id, skillTextInfoJsonData);
						SkillTextInfoJsonData.DataList.Add(skillTextInfoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillTextInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillTextInfoJsonData.OnInitFinishAction != null)
			{
				SkillTextInfoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400444F RID: 17487
		public static Dictionary<int, SkillTextInfoJsonData> DataDict = new Dictionary<int, SkillTextInfoJsonData>();

		// Token: 0x04004450 RID: 17488
		public static List<SkillTextInfoJsonData> DataList = new List<SkillTextInfoJsonData>();

		// Token: 0x04004451 RID: 17489
		public static Action OnInitFinishAction = new Action(SkillTextInfoJsonData.OnInitFinish);

		// Token: 0x04004452 RID: 17490
		public int id;

		// Token: 0x04004453 RID: 17491
		public string name;

		// Token: 0x04004454 RID: 17492
		public string descr;
	}
}
