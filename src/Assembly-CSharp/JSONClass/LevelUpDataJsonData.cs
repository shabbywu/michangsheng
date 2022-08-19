using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000871 RID: 2161
	public class LevelUpDataJsonData : IJSONClass
	{
		// Token: 0x06003FD7 RID: 16343 RVA: 0x001B3B38 File Offset: 0x001B1D38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LevelUpDataJsonData.list)
			{
				try
				{
					LevelUpDataJsonData levelUpDataJsonData = new LevelUpDataJsonData();
					levelUpDataJsonData.id = jsonobject["id"].I;
					levelUpDataJsonData.level = jsonobject["level"].I;
					levelUpDataJsonData.AddHp = jsonobject["AddHp"].I;
					levelUpDataJsonData.AddShenShi = jsonobject["AddShenShi"].I;
					levelUpDataJsonData.AddDunSu = jsonobject["AddDunSu"].I;
					levelUpDataJsonData.AddShouYuan = jsonobject["AddShouYuan"].I;
					levelUpDataJsonData.MaxExp = jsonobject["MaxExp"].I;
					levelUpDataJsonData.wudaodian = jsonobject["wudaodian"].I;
					levelUpDataJsonData.Name = jsonobject["Name"].Str;
					if (LevelUpDataJsonData.DataDict.ContainsKey(levelUpDataJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LevelUpDataJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", levelUpDataJsonData.id));
					}
					else
					{
						LevelUpDataJsonData.DataDict.Add(levelUpDataJsonData.id, levelUpDataJsonData);
						LevelUpDataJsonData.DataList.Add(levelUpDataJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LevelUpDataJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LevelUpDataJsonData.OnInitFinishAction != null)
			{
				LevelUpDataJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C74 RID: 15476
		public static Dictionary<int, LevelUpDataJsonData> DataDict = new Dictionary<int, LevelUpDataJsonData>();

		// Token: 0x04003C75 RID: 15477
		public static List<LevelUpDataJsonData> DataList = new List<LevelUpDataJsonData>();

		// Token: 0x04003C76 RID: 15478
		public static Action OnInitFinishAction = new Action(LevelUpDataJsonData.OnInitFinish);

		// Token: 0x04003C77 RID: 15479
		public int id;

		// Token: 0x04003C78 RID: 15480
		public int level;

		// Token: 0x04003C79 RID: 15481
		public int AddHp;

		// Token: 0x04003C7A RID: 15482
		public int AddShenShi;

		// Token: 0x04003C7B RID: 15483
		public int AddDunSu;

		// Token: 0x04003C7C RID: 15484
		public int AddShouYuan;

		// Token: 0x04003C7D RID: 15485
		public int MaxExp;

		// Token: 0x04003C7E RID: 15486
		public int wudaodian;

		// Token: 0x04003C7F RID: 15487
		public string Name;
	}
}
