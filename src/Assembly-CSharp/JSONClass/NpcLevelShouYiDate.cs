using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C34 RID: 3124
	public class NpcLevelShouYiDate : IJSONClass
	{
		// Token: 0x06004C39 RID: 19513 RVA: 0x00202F78 File Offset: 0x00201178
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcLevelShouYiDate.list)
			{
				try
				{
					NpcLevelShouYiDate npcLevelShouYiDate = new NpcLevelShouYiDate();
					npcLevelShouYiDate.id = jsonobject["id"].I;
					npcLevelShouYiDate.money = jsonobject["money"].I;
					npcLevelShouYiDate.gongxian = jsonobject["gongxian"].I;
					npcLevelShouYiDate.fabao = jsonobject["fabao"].I;
					npcLevelShouYiDate.wudaoexp = jsonobject["wudaoexp"].I;
					npcLevelShouYiDate.ZengLi = jsonobject["ZengLi"].I;
					npcLevelShouYiDate.jieshapanduan = jsonobject["jieshapanduan"].I;
					npcLevelShouYiDate.siwangjilv = jsonobject["siwangjilv"].I;
					if (NpcLevelShouYiDate.DataDict.ContainsKey(npcLevelShouYiDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcLevelShouYiDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcLevelShouYiDate.id));
					}
					else
					{
						NpcLevelShouYiDate.DataDict.Add(npcLevelShouYiDate.id, npcLevelShouYiDate);
						NpcLevelShouYiDate.DataList.Add(npcLevelShouYiDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcLevelShouYiDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcLevelShouYiDate.OnInitFinishAction != null)
			{
				NpcLevelShouYiDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040049F0 RID: 18928
		public static Dictionary<int, NpcLevelShouYiDate> DataDict = new Dictionary<int, NpcLevelShouYiDate>();

		// Token: 0x040049F1 RID: 18929
		public static List<NpcLevelShouYiDate> DataList = new List<NpcLevelShouYiDate>();

		// Token: 0x040049F2 RID: 18930
		public static Action OnInitFinishAction = new Action(NpcLevelShouYiDate.OnInitFinish);

		// Token: 0x040049F3 RID: 18931
		public int id;

		// Token: 0x040049F4 RID: 18932
		public int money;

		// Token: 0x040049F5 RID: 18933
		public int gongxian;

		// Token: 0x040049F6 RID: 18934
		public int fabao;

		// Token: 0x040049F7 RID: 18935
		public int wudaoexp;

		// Token: 0x040049F8 RID: 18936
		public int ZengLi;

		// Token: 0x040049F9 RID: 18937
		public int jieshapanduan;

		// Token: 0x040049FA RID: 18938
		public int siwangjilv;
	}
}
