using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CCE RID: 3278
	public class SkillSeidJsonData8 : IJSONClass
	{
		// Token: 0x06004EA0 RID: 20128 RVA: 0x002107E4 File Offset: 0x0020E9E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[8].list)
			{
				try
				{
					SkillSeidJsonData8 skillSeidJsonData = new SkillSeidJsonData8();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData8.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData8.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData8.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData8.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData8.OnInitFinishAction != null)
			{
				SkillSeidJsonData8.OnInitFinishAction();
			}
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EDC RID: 20188
		public static int SEIDID = 8;

		// Token: 0x04004EDD RID: 20189
		public static Dictionary<int, SkillSeidJsonData8> DataDict = new Dictionary<int, SkillSeidJsonData8>();

		// Token: 0x04004EDE RID: 20190
		public static List<SkillSeidJsonData8> DataList = new List<SkillSeidJsonData8>();

		// Token: 0x04004EDF RID: 20191
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData8.OnInitFinish);

		// Token: 0x04004EE0 RID: 20192
		public int skillid;

		// Token: 0x04004EE1 RID: 20193
		public int value1;
	}
}
