using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C91 RID: 3217
	public class SkillSeidJsonData152 : IJSONClass
	{
		// Token: 0x06004DAD RID: 19885 RVA: 0x0020BD6C File Offset: 0x00209F6C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[152].list)
			{
				try
				{
					SkillSeidJsonData152 skillSeidJsonData = new SkillSeidJsonData152();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData152.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData152.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData152.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData152.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData152.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData152.OnInitFinishAction != null)
			{
				SkillSeidJsonData152.OnInitFinishAction();
			}
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D3D RID: 19773
		public static int SEIDID = 152;

		// Token: 0x04004D3E RID: 19774
		public static Dictionary<int, SkillSeidJsonData152> DataDict = new Dictionary<int, SkillSeidJsonData152>();

		// Token: 0x04004D3F RID: 19775
		public static List<SkillSeidJsonData152> DataList = new List<SkillSeidJsonData152>();

		// Token: 0x04004D40 RID: 19776
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData152.OnInitFinish);

		// Token: 0x04004D41 RID: 19777
		public int id;

		// Token: 0x04004D42 RID: 19778
		public int target;

		// Token: 0x04004D43 RID: 19779
		public int value1;

		// Token: 0x04004D44 RID: 19780
		public int value2;
	}
}
