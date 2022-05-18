using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C29 RID: 3113
	public class NPCChuShiHuaDate : IJSONClass
	{
		// Token: 0x06004C0D RID: 19469 RVA: 0x002019AC File Offset: 0x001FFBAC
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

		// Token: 0x06004C0E RID: 19470 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004966 RID: 18790
		public static Dictionary<int, NPCChuShiHuaDate> DataDict = new Dictionary<int, NPCChuShiHuaDate>();

		// Token: 0x04004967 RID: 18791
		public static List<NPCChuShiHuaDate> DataList = new List<NPCChuShiHuaDate>();

		// Token: 0x04004968 RID: 18792
		public static Action OnInitFinishAction = new Action(NPCChuShiHuaDate.OnInitFinish);

		// Token: 0x04004969 RID: 18793
		public int id;

		// Token: 0x0400496A RID: 18794
		public int LiuPai;

		// Token: 0x0400496B RID: 18795
		public List<int> Level = new List<int>();

		// Token: 0x0400496C RID: 18796
		public List<int> Num = new List<int>();
	}
}
