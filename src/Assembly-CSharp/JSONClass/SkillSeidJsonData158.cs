using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C94 RID: 3220
	public class SkillSeidJsonData158 : IJSONClass
	{
		// Token: 0x06004DB8 RID: 19896 RVA: 0x0020C0E0 File Offset: 0x0020A2E0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[158].list)
			{
				try
				{
					SkillSeidJsonData158 skillSeidJsonData = new SkillSeidJsonData158();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value4 = jsonobject["value4"].ToList();
					if (SkillSeidJsonData158.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData158.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData158.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData158.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData158.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData158.OnInitFinishAction != null)
			{
				SkillSeidJsonData158.OnInitFinishAction();
			}
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D53 RID: 19795
		public static int SEIDID = 158;

		// Token: 0x04004D54 RID: 19796
		public static Dictionary<int, SkillSeidJsonData158> DataDict = new Dictionary<int, SkillSeidJsonData158>();

		// Token: 0x04004D55 RID: 19797
		public static List<SkillSeidJsonData158> DataList = new List<SkillSeidJsonData158>();

		// Token: 0x04004D56 RID: 19798
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData158.OnInitFinish);

		// Token: 0x04004D57 RID: 19799
		public int id;

		// Token: 0x04004D58 RID: 19800
		public int value1;

		// Token: 0x04004D59 RID: 19801
		public int value3;

		// Token: 0x04004D5A RID: 19802
		public List<int> value2 = new List<int>();

		// Token: 0x04004D5B RID: 19803
		public List<int> value4 = new List<int>();
	}
}
