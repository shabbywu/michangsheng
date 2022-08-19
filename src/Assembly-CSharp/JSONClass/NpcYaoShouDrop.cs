using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B7 RID: 2231
	public class NpcYaoShouDrop : IJSONClass
	{
		// Token: 0x060040EF RID: 16623 RVA: 0x001BC7CC File Offset: 0x001BA9CC
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

		// Token: 0x060040F0 RID: 16624 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F9D RID: 16285
		public static Dictionary<int, NpcYaoShouDrop> DataDict = new Dictionary<int, NpcYaoShouDrop>();

		// Token: 0x04003F9E RID: 16286
		public static List<NpcYaoShouDrop> DataList = new List<NpcYaoShouDrop>();

		// Token: 0x04003F9F RID: 16287
		public static Action OnInitFinishAction = new Action(NpcYaoShouDrop.OnInitFinish);

		// Token: 0x04003FA0 RID: 16288
		public int id;

		// Token: 0x04003FA1 RID: 16289
		public int avatarid;

		// Token: 0x04003FA2 RID: 16290
		public int jingjie;

		// Token: 0x04003FA3 RID: 16291
		public int NingZhou;

		// Token: 0x04003FA4 RID: 16292
		public int HaiShang;

		// Token: 0x04003FA5 RID: 16293
		public List<int> chanchu = new List<int>();
	}
}
