using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C99 RID: 3225
	public class SkillSeidJsonData164 : IJSONClass
	{
		// Token: 0x06004DCC RID: 19916 RVA: 0x0020C754 File Offset: 0x0020A954
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[164].list)
			{
				try
				{
					SkillSeidJsonData164 skillSeidJsonData = new SkillSeidJsonData164();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (SkillSeidJsonData164.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData164.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData164.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData164.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData164.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData164.OnInitFinishAction != null)
			{
				SkillSeidJsonData164.OnInitFinishAction();
			}
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D77 RID: 19831
		public static int SEIDID = 164;

		// Token: 0x04004D78 RID: 19832
		public static Dictionary<int, SkillSeidJsonData164> DataDict = new Dictionary<int, SkillSeidJsonData164>();

		// Token: 0x04004D79 RID: 19833
		public static List<SkillSeidJsonData164> DataList = new List<SkillSeidJsonData164>();

		// Token: 0x04004D7A RID: 19834
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData164.OnInitFinish);

		// Token: 0x04004D7B RID: 19835
		public int skillid;

		// Token: 0x04004D7C RID: 19836
		public int target;

		// Token: 0x04004D7D RID: 19837
		public int value1;

		// Token: 0x04004D7E RID: 19838
		public string panduan;
	}
}
