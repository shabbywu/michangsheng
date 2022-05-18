using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C81 RID: 3201
	public class SkillSeidJsonData12 : IJSONClass
	{
		// Token: 0x06004D6D RID: 19821 RVA: 0x0020A934 File Offset: 0x00208B34
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[12].list)
			{
				try
				{
					SkillSeidJsonData12 skillSeidJsonData = new SkillSeidJsonData12();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData12.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData12.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData12.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData12.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData12.OnInitFinishAction != null)
			{
				SkillSeidJsonData12.OnInitFinishAction();
			}
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CCE RID: 19662
		public static int SEIDID = 12;

		// Token: 0x04004CCF RID: 19663
		public static Dictionary<int, SkillSeidJsonData12> DataDict = new Dictionary<int, SkillSeidJsonData12>();

		// Token: 0x04004CD0 RID: 19664
		public static List<SkillSeidJsonData12> DataList = new List<SkillSeidJsonData12>();

		// Token: 0x04004CD1 RID: 19665
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData12.OnInitFinish);

		// Token: 0x04004CD2 RID: 19666
		public int skillid;

		// Token: 0x04004CD3 RID: 19667
		public int value1;
	}
}
