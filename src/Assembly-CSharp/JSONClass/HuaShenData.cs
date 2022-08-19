using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200084A RID: 2122
	public class HuaShenData : IJSONClass
	{
		// Token: 0x06003F3A RID: 16186 RVA: 0x001B028C File Offset: 0x001AE48C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.HuaShenData.list)
			{
				try
				{
					HuaShenData huaShenData = new HuaShenData();
					huaShenData.id = jsonobject["id"].I;
					huaShenData.Buff = jsonobject["Buff"].I;
					huaShenData.Skill = jsonobject["Skill"].I;
					huaShenData.Name = jsonobject["Name"].Str;
					if (HuaShenData.DataDict.ContainsKey(huaShenData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典HuaShenData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", huaShenData.id));
					}
					else
					{
						HuaShenData.DataDict.Add(huaShenData.id, huaShenData);
						HuaShenData.DataList.Add(huaShenData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典HuaShenData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (HuaShenData.OnInitFinishAction != null)
			{
				HuaShenData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B70 RID: 15216
		public static Dictionary<int, HuaShenData> DataDict = new Dictionary<int, HuaShenData>();

		// Token: 0x04003B71 RID: 15217
		public static List<HuaShenData> DataList = new List<HuaShenData>();

		// Token: 0x04003B72 RID: 15218
		public static Action OnInitFinishAction = new Action(HuaShenData.OnInitFinish);

		// Token: 0x04003B73 RID: 15219
		public int id;

		// Token: 0x04003B74 RID: 15220
		public int Buff;

		// Token: 0x04003B75 RID: 15221
		public int Skill;

		// Token: 0x04003B76 RID: 15222
		public string Name;
	}
}
