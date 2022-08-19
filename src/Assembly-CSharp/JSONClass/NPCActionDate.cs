using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000894 RID: 2196
	public class NPCActionDate : IJSONClass
	{
		// Token: 0x06004063 RID: 16483 RVA: 0x001B79E0 File Offset: 0x001B5BE0
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

		// Token: 0x06004064 RID: 16484 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DC5 RID: 15813
		public static Dictionary<int, NPCActionDate> DataDict = new Dictionary<int, NPCActionDate>();

		// Token: 0x04003DC6 RID: 15814
		public static List<NPCActionDate> DataList = new List<NPCActionDate>();

		// Token: 0x04003DC7 RID: 15815
		public static Action OnInitFinishAction = new Action(NPCActionDate.OnInitFinish);

		// Token: 0x04003DC8 RID: 15816
		public int id;

		// Token: 0x04003DC9 RID: 15817
		public int QuanZhong;

		// Token: 0x04003DCA RID: 15818
		public int PanDing;

		// Token: 0x04003DCB RID: 15819
		public int AllMap;

		// Token: 0x04003DCC RID: 15820
		public int FuBen;

		// Token: 0x04003DCD RID: 15821
		public int IsTask;

		// Token: 0x04003DCE RID: 15822
		public string ThreeSence;

		// Token: 0x04003DCF RID: 15823
		public string GuanLianTalk;
	}
}
