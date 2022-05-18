using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C7B RID: 3195
	public class SkillSeidJsonData111 : IJSONClass
	{
		// Token: 0x06004D55 RID: 19797 RVA: 0x0020A1DC File Offset: 0x002083DC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[111].list)
			{
				try
				{
					SkillSeidJsonData111 skillSeidJsonData = new SkillSeidJsonData111();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData111.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData111.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData111.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData111.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData111.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData111.OnInitFinishAction != null)
			{
				SkillSeidJsonData111.OnInitFinishAction();
			}
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CA5 RID: 19621
		public static int SEIDID = 111;

		// Token: 0x04004CA6 RID: 19622
		public static Dictionary<int, SkillSeidJsonData111> DataDict = new Dictionary<int, SkillSeidJsonData111>();

		// Token: 0x04004CA7 RID: 19623
		public static List<SkillSeidJsonData111> DataList = new List<SkillSeidJsonData111>();

		// Token: 0x04004CA8 RID: 19624
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData111.OnInitFinish);

		// Token: 0x04004CA9 RID: 19625
		public int id;

		// Token: 0x04004CAA RID: 19626
		public int value1;
	}
}
