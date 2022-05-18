using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE2 RID: 3298
	public class SkillTextInfoJsonData : IJSONClass
	{
		// Token: 0x06004EF0 RID: 20208 RVA: 0x00211FE0 File Offset: 0x002101E0
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

		// Token: 0x06004EF1 RID: 20209 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F5F RID: 20319
		public static Dictionary<int, SkillTextInfoJsonData> DataDict = new Dictionary<int, SkillTextInfoJsonData>();

		// Token: 0x04004F60 RID: 20320
		public static List<SkillTextInfoJsonData> DataList = new List<SkillTextInfoJsonData>();

		// Token: 0x04004F61 RID: 20321
		public static Action OnInitFinishAction = new Action(SkillTextInfoJsonData.OnInitFinish);

		// Token: 0x04004F62 RID: 20322
		public int id;

		// Token: 0x04004F63 RID: 20323
		public string name;

		// Token: 0x04004F64 RID: 20324
		public string descr;
	}
}
