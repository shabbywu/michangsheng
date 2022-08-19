using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200089B RID: 2203
	public class NPCChuShiHuaDate : IJSONClass
	{
		// Token: 0x0600407F RID: 16511 RVA: 0x001B8728 File Offset: 0x001B6928
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCChuShiHuaDate.list)
			{
				try
				{
					NPCChuShiHuaDate npcchuShiHuaDate = new NPCChuShiHuaDate();
					npcchuShiHuaDate.id = jsonobject["id"].I;
					npcchuShiHuaDate.LiuPai = jsonobject["LiuPai"].I;
					npcchuShiHuaDate.Level = jsonobject["Level"].ToList();
					npcchuShiHuaDate.Num = jsonobject["Num"].ToList();
					if (NPCChuShiHuaDate.DataDict.ContainsKey(npcchuShiHuaDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCChuShiHuaDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcchuShiHuaDate.id));
					}
					else
					{
						NPCChuShiHuaDate.DataDict.Add(npcchuShiHuaDate.id, npcchuShiHuaDate);
						NPCChuShiHuaDate.DataList.Add(npcchuShiHuaDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCChuShiHuaDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCChuShiHuaDate.OnInitFinishAction != null)
			{
				NPCChuShiHuaDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E0D RID: 15885
		public static Dictionary<int, NPCChuShiHuaDate> DataDict = new Dictionary<int, NPCChuShiHuaDate>();

		// Token: 0x04003E0E RID: 15886
		public static List<NPCChuShiHuaDate> DataList = new List<NPCChuShiHuaDate>();

		// Token: 0x04003E0F RID: 15887
		public static Action OnInitFinishAction = new Action(NPCChuShiHuaDate.OnInitFinish);

		// Token: 0x04003E10 RID: 15888
		public int id;

		// Token: 0x04003E11 RID: 15889
		public int LiuPai;

		// Token: 0x04003E12 RID: 15890
		public List<int> Level = new List<int>();

		// Token: 0x04003E13 RID: 15891
		public List<int> Num = new List<int>();
	}
}
