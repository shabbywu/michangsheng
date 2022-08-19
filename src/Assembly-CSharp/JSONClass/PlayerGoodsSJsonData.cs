using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008CB RID: 2251
	public class PlayerGoodsSJsonData : IJSONClass
	{
		// Token: 0x0600413F RID: 16703 RVA: 0x001BECC8 File Offset: 0x001BCEC8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.PlayerGoodsSJsonData.list)
			{
				try
				{
					PlayerGoodsSJsonData playerGoodsSJsonData = new PlayerGoodsSJsonData();
					playerGoodsSJsonData.id = jsonobject["id"].I;
					playerGoodsSJsonData.itemStack = jsonobject["itemStack"].I;
					playerGoodsSJsonData.onlyOne = jsonobject["onlyOne"].I;
					playerGoodsSJsonData.script = jsonobject["script"].Str;
					playerGoodsSJsonData.name = jsonobject["name"].Str;
					playerGoodsSJsonData.type = jsonobject["type"].Str;
					if (PlayerGoodsSJsonData.DataDict.ContainsKey(playerGoodsSJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典PlayerGoodsSJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", playerGoodsSJsonData.id));
					}
					else
					{
						PlayerGoodsSJsonData.DataDict.Add(playerGoodsSJsonData.id, playerGoodsSJsonData);
						PlayerGoodsSJsonData.DataList.Add(playerGoodsSJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典PlayerGoodsSJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (PlayerGoodsSJsonData.OnInitFinishAction != null)
			{
				PlayerGoodsSJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004060 RID: 16480
		public static Dictionary<int, PlayerGoodsSJsonData> DataDict = new Dictionary<int, PlayerGoodsSJsonData>();

		// Token: 0x04004061 RID: 16481
		public static List<PlayerGoodsSJsonData> DataList = new List<PlayerGoodsSJsonData>();

		// Token: 0x04004062 RID: 16482
		public static Action OnInitFinishAction = new Action(PlayerGoodsSJsonData.OnInitFinish);

		// Token: 0x04004063 RID: 16483
		public int id;

		// Token: 0x04004064 RID: 16484
		public int itemStack;

		// Token: 0x04004065 RID: 16485
		public int onlyOne;

		// Token: 0x04004066 RID: 16486
		public string script;

		// Token: 0x04004067 RID: 16487
		public string name;

		// Token: 0x04004068 RID: 16488
		public string type;
	}
}
