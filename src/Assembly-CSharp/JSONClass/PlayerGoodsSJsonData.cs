using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C59 RID: 3161
	public class PlayerGoodsSJsonData : IJSONClass
	{
		// Token: 0x06004CCD RID: 19661 RVA: 0x002074D4 File Offset: 0x002056D4
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

		// Token: 0x06004CCE RID: 19662 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BB4 RID: 19380
		public static Dictionary<int, PlayerGoodsSJsonData> DataDict = new Dictionary<int, PlayerGoodsSJsonData>();

		// Token: 0x04004BB5 RID: 19381
		public static List<PlayerGoodsSJsonData> DataList = new List<PlayerGoodsSJsonData>();

		// Token: 0x04004BB6 RID: 19382
		public static Action OnInitFinishAction = new Action(PlayerGoodsSJsonData.OnInitFinish);

		// Token: 0x04004BB7 RID: 19383
		public int id;

		// Token: 0x04004BB8 RID: 19384
		public int itemStack;

		// Token: 0x04004BB9 RID: 19385
		public int onlyOne;

		// Token: 0x04004BBA RID: 19386
		public string script;

		// Token: 0x04004BBB RID: 19387
		public string name;

		// Token: 0x04004BBC RID: 19388
		public string type;
	}
}
