using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD0 RID: 3024
	public class FightAIData : IJSONClass
	{
		// Token: 0x06004AA8 RID: 19112 RVA: 0x001F953C File Offset: 0x001F773C
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

		// Token: 0x06004AA9 RID: 19113 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004660 RID: 18016
		public static Dictionary<int, FightAIData> DataDict = new Dictionary<int, FightAIData>();

		// Token: 0x04004661 RID: 18017
		public static List<FightAIData> DataList = new List<FightAIData>();

		// Token: 0x04004662 RID: 18018
		public static Action OnInitFinishAction = new Action(FightAIData.OnInitFinish);

		// Token: 0x04004663 RID: 18019
		public int id;

		// Token: 0x04004664 RID: 18020
		public List<int> ShunXu = new List<int>();
	}
}
