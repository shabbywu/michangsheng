using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C3D RID: 3133
	public class NpcTalkHouXuJiaoTanData : IJSONClass
	{
		// Token: 0x06004C5D RID: 19549 RVA: 0x00203F40 File Offset: 0x00202140
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkHouXuJiaoTanData.list)
			{
				try
				{
					NpcTalkHouXuJiaoTanData npcTalkHouXuJiaoTanData = new NpcTalkHouXuJiaoTanData();
					npcTalkHouXuJiaoTanData.id = jsonobject["id"].I;
					npcTalkHouXuJiaoTanData.JingJie = jsonobject["JingJie"].I;
					npcTalkHouXuJiaoTanData.XingGe = jsonobject["XingGe"].I;
					npcTalkHouXuJiaoTanData.HaoGanDu = jsonobject["HaoGanDu"].I;
					npcTalkHouXuJiaoTanData.OtherTalk = jsonobject["OtherTalk"].Str;
					if (NpcTalkHouXuJiaoTanData.DataDict.ContainsKey(npcTalkHouXuJiaoTanData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcTalkHouXuJiaoTanData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcTalkHouXuJiaoTanData.id));
					}
					else
					{
						NpcTalkHouXuJiaoTanData.DataDict.Add(npcTalkHouXuJiaoTanData.id, npcTalkHouXuJiaoTanData);
						NpcTalkHouXuJiaoTanData.DataList.Add(npcTalkHouXuJiaoTanData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcTalkHouXuJiaoTanData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcTalkHouXuJiaoTanData.OnInitFinishAction != null)
			{
				NpcTalkHouXuJiaoTanData.OnInitFinishAction();
			}
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A55 RID: 19029
		public static Dictionary<int, NpcTalkHouXuJiaoTanData> DataDict = new Dictionary<int, NpcTalkHouXuJiaoTanData>();

		// Token: 0x04004A56 RID: 19030
		public static List<NpcTalkHouXuJiaoTanData> DataList = new List<NpcTalkHouXuJiaoTanData>();

		// Token: 0x04004A57 RID: 19031
		public static Action OnInitFinishAction = new Action(NpcTalkHouXuJiaoTanData.OnInitFinish);

		// Token: 0x04004A58 RID: 19032
		public int id;

		// Token: 0x04004A59 RID: 19033
		public int JingJie;

		// Token: 0x04004A5A RID: 19034
		public int XingGe;

		// Token: 0x04004A5B RID: 19035
		public int HaoGanDu;

		// Token: 0x04004A5C RID: 19036
		public string OtherTalk;
	}
}
