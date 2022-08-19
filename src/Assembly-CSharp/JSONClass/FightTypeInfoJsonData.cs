using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000840 RID: 2112
	public class FightTypeInfoJsonData : IJSONClass
	{
		// Token: 0x06003F12 RID: 16146 RVA: 0x001AEE24 File Offset: 0x001AD024
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.FightTypeInfoJsonData.list)
			{
				try
				{
					FightTypeInfoJsonData fightTypeInfoJsonData = new FightTypeInfoJsonData();
					fightTypeInfoJsonData.id = jsonobject["id"].I;
					fightTypeInfoJsonData.Type1 = jsonobject["Type1"].I;
					fightTypeInfoJsonData.Type2 = jsonobject["Type2"].I;
					fightTypeInfoJsonData.Type9 = jsonobject["Type9"].I;
					fightTypeInfoJsonData.Type3 = jsonobject["Type3"].I;
					fightTypeInfoJsonData.Type4 = jsonobject["Type4"].I;
					fightTypeInfoJsonData.Type5 = jsonobject["Type5"].I;
					fightTypeInfoJsonData.Type6 = jsonobject["Type6"].I;
					fightTypeInfoJsonData.Type7 = jsonobject["Type7"].I;
					fightTypeInfoJsonData.Type8 = jsonobject["Type8"].I;
					fightTypeInfoJsonData.Name = jsonobject["Name"].Str;
					if (FightTypeInfoJsonData.DataDict.ContainsKey(fightTypeInfoJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典FightTypeInfoJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", fightTypeInfoJsonData.id));
					}
					else
					{
						FightTypeInfoJsonData.DataDict.Add(fightTypeInfoJsonData.id, fightTypeInfoJsonData);
						FightTypeInfoJsonData.DataList.Add(fightTypeInfoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典FightTypeInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (FightTypeInfoJsonData.OnInitFinishAction != null)
			{
				FightTypeInfoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AF7 RID: 15095
		public static Dictionary<int, FightTypeInfoJsonData> DataDict = new Dictionary<int, FightTypeInfoJsonData>();

		// Token: 0x04003AF8 RID: 15096
		public static List<FightTypeInfoJsonData> DataList = new List<FightTypeInfoJsonData>();

		// Token: 0x04003AF9 RID: 15097
		public static Action OnInitFinishAction = new Action(FightTypeInfoJsonData.OnInitFinish);

		// Token: 0x04003AFA RID: 15098
		public int id;

		// Token: 0x04003AFB RID: 15099
		public int Type1;

		// Token: 0x04003AFC RID: 15100
		public int Type2;

		// Token: 0x04003AFD RID: 15101
		public int Type9;

		// Token: 0x04003AFE RID: 15102
		public int Type3;

		// Token: 0x04003AFF RID: 15103
		public int Type4;

		// Token: 0x04003B00 RID: 15104
		public int Type5;

		// Token: 0x04003B01 RID: 15105
		public int Type6;

		// Token: 0x04003B02 RID: 15106
		public int Type7;

		// Token: 0x04003B03 RID: 15107
		public int Type8;

		// Token: 0x04003B04 RID: 15108
		public string Name;
	}
}
