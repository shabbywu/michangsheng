using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A9 RID: 2217
	public class NpcQingJiaoXiaoHaoData : IJSONClass
	{
		// Token: 0x060040B7 RID: 16567 RVA: 0x001BA6DC File Offset: 0x001B88DC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcQingJiaoXiaoHaoData.list)
			{
				try
				{
					NpcQingJiaoXiaoHaoData npcQingJiaoXiaoHaoData = new NpcQingJiaoXiaoHaoData();
					npcQingJiaoXiaoHaoData.id = jsonobject["id"].I;
					npcQingJiaoXiaoHaoData.Type = jsonobject["Type"].I;
					npcQingJiaoXiaoHaoData.quality = jsonobject["quality"].I;
					npcQingJiaoXiaoHaoData.typePinJie = jsonobject["typePinJie"].I;
					npcQingJiaoXiaoHaoData.QingFen = jsonobject["QingFen"].I;
					if (NpcQingJiaoXiaoHaoData.DataDict.ContainsKey(npcQingJiaoXiaoHaoData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcQingJiaoXiaoHaoData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcQingJiaoXiaoHaoData.id));
					}
					else
					{
						NpcQingJiaoXiaoHaoData.DataDict.Add(npcQingJiaoXiaoHaoData.id, npcQingJiaoXiaoHaoData);
						NpcQingJiaoXiaoHaoData.DataList.Add(npcQingJiaoXiaoHaoData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcQingJiaoXiaoHaoData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcQingJiaoXiaoHaoData.OnInitFinishAction != null)
			{
				NpcQingJiaoXiaoHaoData.OnInitFinishAction();
			}
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003ECA RID: 16074
		public static Dictionary<int, NpcQingJiaoXiaoHaoData> DataDict = new Dictionary<int, NpcQingJiaoXiaoHaoData>();

		// Token: 0x04003ECB RID: 16075
		public static List<NpcQingJiaoXiaoHaoData> DataList = new List<NpcQingJiaoXiaoHaoData>();

		// Token: 0x04003ECC RID: 16076
		public static Action OnInitFinishAction = new Action(NpcQingJiaoXiaoHaoData.OnInitFinish);

		// Token: 0x04003ECD RID: 16077
		public int id;

		// Token: 0x04003ECE RID: 16078
		public int Type;

		// Token: 0x04003ECF RID: 16079
		public int quality;

		// Token: 0x04003ED0 RID: 16080
		public int typePinJie;

		// Token: 0x04003ED1 RID: 16081
		public int QingFen;
	}
}
