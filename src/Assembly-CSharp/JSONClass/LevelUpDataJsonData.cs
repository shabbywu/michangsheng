using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BFF RID: 3071
	public class LevelUpDataJsonData : IJSONClass
	{
		// Token: 0x06004B65 RID: 19301 RVA: 0x001FD550 File Offset: 0x001FB750
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

		// Token: 0x06004B66 RID: 19302 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047CD RID: 18381
		public static Dictionary<int, LevelUpDataJsonData> DataDict = new Dictionary<int, LevelUpDataJsonData>();

		// Token: 0x040047CE RID: 18382
		public static List<LevelUpDataJsonData> DataList = new List<LevelUpDataJsonData>();

		// Token: 0x040047CF RID: 18383
		public static Action OnInitFinishAction = new Action(LevelUpDataJsonData.OnInitFinish);

		// Token: 0x040047D0 RID: 18384
		public int id;

		// Token: 0x040047D1 RID: 18385
		public int level;

		// Token: 0x040047D2 RID: 18386
		public int AddHp;

		// Token: 0x040047D3 RID: 18387
		public int AddShenShi;

		// Token: 0x040047D4 RID: 18388
		public int AddDunSu;

		// Token: 0x040047D5 RID: 18389
		public int AddShouYuan;

		// Token: 0x040047D6 RID: 18390
		public int MaxExp;

		// Token: 0x040047D7 RID: 18391
		public int wudaodian;

		// Token: 0x040047D8 RID: 18392
		public string Name;
	}
}
