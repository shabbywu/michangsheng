using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C22 RID: 3106
	public class NPCActionDate : IJSONClass
	{
		// Token: 0x06004BF1 RID: 19441 RVA: 0x00200DEC File Offset: 0x001FEFEC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCActionDate.list)
			{
				try
				{
					NPCActionDate npcactionDate = new NPCActionDate();
					npcactionDate.id = jsonobject["id"].I;
					npcactionDate.QuanZhong = jsonobject["QuanZhong"].I;
					npcactionDate.PanDing = jsonobject["PanDing"].I;
					npcactionDate.AllMap = jsonobject["AllMap"].I;
					npcactionDate.FuBen = jsonobject["FuBen"].I;
					npcactionDate.IsTask = jsonobject["IsTask"].I;
					npcactionDate.ThreeSence = jsonobject["ThreeSence"].Str;
					npcactionDate.GuanLianTalk = jsonobject["GuanLianTalk"].Str;
					if (NPCActionDate.DataDict.ContainsKey(npcactionDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCActionDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcactionDate.id));
					}
					else
					{
						NPCActionDate.DataDict.Add(npcactionDate.id, npcactionDate);
						NPCActionDate.DataList.Add(npcactionDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCActionDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCActionDate.OnInitFinishAction != null)
			{
				NPCActionDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400491E RID: 18718
		public static Dictionary<int, NPCActionDate> DataDict = new Dictionary<int, NPCActionDate>();

		// Token: 0x0400491F RID: 18719
		public static List<NPCActionDate> DataList = new List<NPCActionDate>();

		// Token: 0x04004920 RID: 18720
		public static Action OnInitFinishAction = new Action(NPCActionDate.OnInitFinish);

		// Token: 0x04004921 RID: 18721
		public int id;

		// Token: 0x04004922 RID: 18722
		public int QuanZhong;

		// Token: 0x04004923 RID: 18723
		public int PanDing;

		// Token: 0x04004924 RID: 18724
		public int AllMap;

		// Token: 0x04004925 RID: 18725
		public int FuBen;

		// Token: 0x04004926 RID: 18726
		public int IsTask;

		// Token: 0x04004927 RID: 18727
		public string ThreeSence;

		// Token: 0x04004928 RID: 18728
		public string GuanLianTalk;
	}
}
