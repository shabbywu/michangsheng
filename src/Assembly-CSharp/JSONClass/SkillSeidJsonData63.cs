using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC2 RID: 3266
	public class SkillSeidJsonData63 : IJSONClass
	{
		// Token: 0x06004E70 RID: 20080 RVA: 0x0020F930 File Offset: 0x0020DB30
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[63].list)
			{
				try
				{
					SkillSeidJsonData63 skillSeidJsonData = new SkillSeidJsonData63();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData63.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData63.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData63.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData63.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData63.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData63.OnInitFinishAction != null)
			{
				SkillSeidJsonData63.OnInitFinishAction();
			}
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E8B RID: 20107
		public static int SEIDID = 63;

		// Token: 0x04004E8C RID: 20108
		public static Dictionary<int, SkillSeidJsonData63> DataDict = new Dictionary<int, SkillSeidJsonData63>();

		// Token: 0x04004E8D RID: 20109
		public static List<SkillSeidJsonData63> DataList = new List<SkillSeidJsonData63>();

		// Token: 0x04004E8E RID: 20110
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData63.OnInitFinish);

		// Token: 0x04004E8F RID: 20111
		public int skillid;

		// Token: 0x04004E90 RID: 20112
		public int value1;

		// Token: 0x04004E91 RID: 20113
		public int value2;
	}
}
