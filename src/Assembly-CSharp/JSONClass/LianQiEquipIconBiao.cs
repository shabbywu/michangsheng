using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000876 RID: 2166
	public class LianQiEquipIconBiao : IJSONClass
	{
		// Token: 0x06003FEB RID: 16363 RVA: 0x001B4440 File Offset: 0x001B2640
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiEquipIconBiao.list)
			{
				try
				{
					LianQiEquipIconBiao lianQiEquipIconBiao = new LianQiEquipIconBiao();
					lianQiEquipIconBiao.id = jsonobject["id"].I;
					lianQiEquipIconBiao.zhonglei = jsonobject["zhonglei"].I;
					lianQiEquipIconBiao.quality = jsonobject["quality"].I;
					lianQiEquipIconBiao.pingjie = jsonobject["pingjie"].I;
					lianQiEquipIconBiao.desc = jsonobject["desc"].Str;
					if (LianQiEquipIconBiao.DataDict.ContainsKey(lianQiEquipIconBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiEquipIconBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiEquipIconBiao.id));
					}
					else
					{
						LianQiEquipIconBiao.DataDict.Add(lianQiEquipIconBiao.id, lianQiEquipIconBiao);
						LianQiEquipIconBiao.DataList.Add(lianQiEquipIconBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiEquipIconBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiEquipIconBiao.OnInitFinishAction != null)
			{
				LianQiEquipIconBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003CA7 RID: 15527
		public static Dictionary<int, LianQiEquipIconBiao> DataDict = new Dictionary<int, LianQiEquipIconBiao>();

		// Token: 0x04003CA8 RID: 15528
		public static List<LianQiEquipIconBiao> DataList = new List<LianQiEquipIconBiao>();

		// Token: 0x04003CA9 RID: 15529
		public static Action OnInitFinishAction = new Action(LianQiEquipIconBiao.OnInitFinish);

		// Token: 0x04003CAA RID: 15530
		public int id;

		// Token: 0x04003CAB RID: 15531
		public int zhonglei;

		// Token: 0x04003CAC RID: 15532
		public int quality;

		// Token: 0x04003CAD RID: 15533
		public int pingjie;

		// Token: 0x04003CAE RID: 15534
		public string desc;
	}
}
