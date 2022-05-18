using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB4 RID: 3252
	public class SkillSeidJsonData48 : IJSONClass
	{
		// Token: 0x06004E38 RID: 20024 RVA: 0x0020E858 File Offset: 0x0020CA58
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[48].list)
			{
				try
				{
					SkillSeidJsonData48 skillSeidJsonData = new SkillSeidJsonData48();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData48.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData48.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData48.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData48.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData48.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData48.OnInitFinishAction != null)
			{
				SkillSeidJsonData48.OnInitFinishAction();
			}
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E2F RID: 20015
		public static int SEIDID = 48;

		// Token: 0x04004E30 RID: 20016
		public static Dictionary<int, SkillSeidJsonData48> DataDict = new Dictionary<int, SkillSeidJsonData48>();

		// Token: 0x04004E31 RID: 20017
		public static List<SkillSeidJsonData48> DataList = new List<SkillSeidJsonData48>();

		// Token: 0x04004E32 RID: 20018
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData48.OnInitFinish);

		// Token: 0x04004E33 RID: 20019
		public int skillid;

		// Token: 0x04004E34 RID: 20020
		public int value1;

		// Token: 0x04004E35 RID: 20021
		public int value2;

		// Token: 0x04004E36 RID: 20022
		public int value3;
	}
}
