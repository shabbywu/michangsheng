using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C80 RID: 3200
	public class SkillSeidJsonData118 : IJSONClass
	{
		// Token: 0x06004D69 RID: 19817 RVA: 0x0020A7F8 File Offset: 0x002089F8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[118].list)
			{
				try
				{
					SkillSeidJsonData118 skillSeidJsonData = new SkillSeidJsonData118();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData118.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData118.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData118.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData118.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData118.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData118.OnInitFinishAction != null)
			{
				SkillSeidJsonData118.OnInitFinishAction();
			}
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CC7 RID: 19655
		public static int SEIDID = 118;

		// Token: 0x04004CC8 RID: 19656
		public static Dictionary<int, SkillSeidJsonData118> DataDict = new Dictionary<int, SkillSeidJsonData118>();

		// Token: 0x04004CC9 RID: 19657
		public static List<SkillSeidJsonData118> DataList = new List<SkillSeidJsonData118>();

		// Token: 0x04004CCA RID: 19658
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData118.OnInitFinish);

		// Token: 0x04004CCB RID: 19659
		public int id;

		// Token: 0x04004CCC RID: 19660
		public List<int> value1 = new List<int>();

		// Token: 0x04004CCD RID: 19661
		public List<int> value2 = new List<int>();
	}
}
