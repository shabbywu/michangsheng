using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CEE RID: 3310
	public class StaticSkillTypeJsonData : IJSONClass
	{
		// Token: 0x06004F20 RID: 20256 RVA: 0x00213028 File Offset: 0x00211228
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillTypeJsonData.list)
			{
				try
				{
					StaticSkillTypeJsonData staticSkillTypeJsonData = new StaticSkillTypeJsonData();
					staticSkillTypeJsonData.id = jsonobject["id"].I;
					staticSkillTypeJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillTypeJsonData.DataDict.ContainsKey(staticSkillTypeJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillTypeJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillTypeJsonData.id));
					}
					else
					{
						StaticSkillTypeJsonData.DataDict.Add(staticSkillTypeJsonData.id, staticSkillTypeJsonData);
						StaticSkillTypeJsonData.DataList.Add(staticSkillTypeJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillTypeJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillTypeJsonData.OnInitFinishAction != null)
			{
				StaticSkillTypeJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FBD RID: 20413
		public static Dictionary<int, StaticSkillTypeJsonData> DataDict = new Dictionary<int, StaticSkillTypeJsonData>();

		// Token: 0x04004FBE RID: 20414
		public static List<StaticSkillTypeJsonData> DataList = new List<StaticSkillTypeJsonData>();

		// Token: 0x04004FBF RID: 20415
		public static Action OnInitFinishAction = new Action(StaticSkillTypeJsonData.OnInitFinish);

		// Token: 0x04004FC0 RID: 20416
		public int id;

		// Token: 0x04004FC1 RID: 20417
		public int value1;
	}
}
