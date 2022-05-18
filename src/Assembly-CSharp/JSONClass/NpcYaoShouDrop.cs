using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C45 RID: 3141
	public class NpcYaoShouDrop : IJSONClass
	{
		// Token: 0x06004C7D RID: 19581 RVA: 0x00205424 File Offset: 0x00203624
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcYaoShouDrop.list)
			{
				try
				{
					NpcYaoShouDrop npcYaoShouDrop = new NpcYaoShouDrop();
					npcYaoShouDrop.id = jsonobject["id"].I;
					npcYaoShouDrop.avatarid = jsonobject["avatarid"].I;
					npcYaoShouDrop.jingjie = jsonobject["jingjie"].I;
					npcYaoShouDrop.NingZhou = jsonobject["NingZhou"].I;
					npcYaoShouDrop.HaiShang = jsonobject["HaiShang"].I;
					npcYaoShouDrop.chanchu = jsonobject["chanchu"].ToList();
					if (NpcYaoShouDrop.DataDict.ContainsKey(npcYaoShouDrop.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcYaoShouDrop.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcYaoShouDrop.id));
					}
					else
					{
						NpcYaoShouDrop.DataDict.Add(npcYaoShouDrop.id, npcYaoShouDrop);
						NpcYaoShouDrop.DataList.Add(npcYaoShouDrop);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcYaoShouDrop.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcYaoShouDrop.OnInitFinishAction != null)
			{
				NpcYaoShouDrop.OnInitFinishAction();
			}
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004AF1 RID: 19185
		public static Dictionary<int, NpcYaoShouDrop> DataDict = new Dictionary<int, NpcYaoShouDrop>();

		// Token: 0x04004AF2 RID: 19186
		public static List<NpcYaoShouDrop> DataList = new List<NpcYaoShouDrop>();

		// Token: 0x04004AF3 RID: 19187
		public static Action OnInitFinishAction = new Action(NpcYaoShouDrop.OnInitFinish);

		// Token: 0x04004AF4 RID: 19188
		public int id;

		// Token: 0x04004AF5 RID: 19189
		public int avatarid;

		// Token: 0x04004AF6 RID: 19190
		public int jingjie;

		// Token: 0x04004AF7 RID: 19191
		public int NingZhou;

		// Token: 0x04004AF8 RID: 19192
		public int HaiShang;

		// Token: 0x04004AF9 RID: 19193
		public List<int> chanchu = new List<int>();
	}
}
