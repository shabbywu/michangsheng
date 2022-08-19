using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200083F RID: 2111
	public class FightAIData : IJSONClass
	{
		// Token: 0x06003F0E RID: 16142 RVA: 0x001AECC4 File Offset: 0x001ACEC4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.FightAIData.list)
			{
				try
				{
					FightAIData fightAIData = new FightAIData();
					fightAIData.id = jsonobject["id"].I;
					fightAIData.ShunXu = jsonobject["ShunXu"].ToList();
					if (FightAIData.DataDict.ContainsKey(fightAIData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典FightAIData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", fightAIData.id));
					}
					else
					{
						FightAIData.DataDict.Add(fightAIData.id, fightAIData);
						FightAIData.DataList.Add(fightAIData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典FightAIData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (FightAIData.OnInitFinishAction != null)
			{
				FightAIData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AF2 RID: 15090
		public static Dictionary<int, FightAIData> DataDict = new Dictionary<int, FightAIData>();

		// Token: 0x04003AF3 RID: 15091
		public static List<FightAIData> DataList = new List<FightAIData>();

		// Token: 0x04003AF4 RID: 15092
		public static Action OnInitFinishAction = new Action(FightAIData.OnInitFinish);

		// Token: 0x04003AF5 RID: 15093
		public int id;

		// Token: 0x04003AF6 RID: 15094
		public List<int> ShunXu = new List<int>();
	}
}
