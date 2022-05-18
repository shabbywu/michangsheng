using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C7A RID: 3194
	public class SkillSeidJsonData110 : IJSONClass
	{
		// Token: 0x06004D51 RID: 19793 RVA: 0x0020A0B4 File Offset: 0x002082B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[110].list)
			{
				try
				{
					SkillSeidJsonData110 skillSeidJsonData = new SkillSeidJsonData110();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData110.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData110.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData110.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData110.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData110.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData110.OnInitFinishAction != null)
			{
				SkillSeidJsonData110.OnInitFinishAction();
			}
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C9F RID: 19615
		public static int SEIDID = 110;

		// Token: 0x04004CA0 RID: 19616
		public static Dictionary<int, SkillSeidJsonData110> DataDict = new Dictionary<int, SkillSeidJsonData110>();

		// Token: 0x04004CA1 RID: 19617
		public static List<SkillSeidJsonData110> DataList = new List<SkillSeidJsonData110>();

		// Token: 0x04004CA2 RID: 19618
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData110.OnInitFinish);

		// Token: 0x04004CA3 RID: 19619
		public int id;

		// Token: 0x04004CA4 RID: 19620
		public int value1;
	}
}
