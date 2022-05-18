using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C37 RID: 3127
	public class NpcQingJiaoXiaoHaoData : IJSONClass
	{
		// Token: 0x06004C45 RID: 19525 RVA: 0x00203630 File Offset: 0x00201830
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

		// Token: 0x06004C46 RID: 19526 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A22 RID: 18978
		public static Dictionary<int, NpcQingJiaoXiaoHaoData> DataDict = new Dictionary<int, NpcQingJiaoXiaoHaoData>();

		// Token: 0x04004A23 RID: 18979
		public static List<NpcQingJiaoXiaoHaoData> DataList = new List<NpcQingJiaoXiaoHaoData>();

		// Token: 0x04004A24 RID: 18980
		public static Action OnInitFinishAction = new Action(NpcQingJiaoXiaoHaoData.OnInitFinish);

		// Token: 0x04004A25 RID: 18981
		public int id;

		// Token: 0x04004A26 RID: 18982
		public int Type;

		// Token: 0x04004A27 RID: 18983
		public int quality;

		// Token: 0x04004A28 RID: 18984
		public int typePinJie;

		// Token: 0x04004A29 RID: 18985
		public int QingFen;
	}
}
