using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD1 RID: 3025
	public class FightTypeInfoJsonData : IJSONClass
	{
		// Token: 0x06004AAC RID: 19116 RVA: 0x001F9660 File Offset: 0x001F7860
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

		// Token: 0x06004AAD RID: 19117 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004665 RID: 18021
		public static Dictionary<int, FightTypeInfoJsonData> DataDict = new Dictionary<int, FightTypeInfoJsonData>();

		// Token: 0x04004666 RID: 18022
		public static List<FightTypeInfoJsonData> DataList = new List<FightTypeInfoJsonData>();

		// Token: 0x04004667 RID: 18023
		public static Action OnInitFinishAction = new Action(FightTypeInfoJsonData.OnInitFinish);

		// Token: 0x04004668 RID: 18024
		public int id;

		// Token: 0x04004669 RID: 18025
		public int Type1;

		// Token: 0x0400466A RID: 18026
		public int Type2;

		// Token: 0x0400466B RID: 18027
		public int Type9;

		// Token: 0x0400466C RID: 18028
		public int Type3;

		// Token: 0x0400466D RID: 18029
		public int Type4;

		// Token: 0x0400466E RID: 18030
		public int Type5;

		// Token: 0x0400466F RID: 18031
		public int Type6;

		// Token: 0x04004670 RID: 18032
		public int Type7;

		// Token: 0x04004671 RID: 18033
		public int Type8;

		// Token: 0x04004672 RID: 18034
		public string Name;
	}
}
