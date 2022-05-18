using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C04 RID: 3076
	public class LianQiEquipIconBiao : IJSONClass
	{
		// Token: 0x06004B79 RID: 19321 RVA: 0x001FDD90 File Offset: 0x001FBF90
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

		// Token: 0x06004B7A RID: 19322 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004800 RID: 18432
		public static Dictionary<int, LianQiEquipIconBiao> DataDict = new Dictionary<int, LianQiEquipIconBiao>();

		// Token: 0x04004801 RID: 18433
		public static List<LianQiEquipIconBiao> DataList = new List<LianQiEquipIconBiao>();

		// Token: 0x04004802 RID: 18434
		public static Action OnInitFinishAction = new Action(LianQiEquipIconBiao.OnInitFinish);

		// Token: 0x04004803 RID: 18435
		public int id;

		// Token: 0x04004804 RID: 18436
		public int zhonglei;

		// Token: 0x04004805 RID: 18437
		public int quality;

		// Token: 0x04004806 RID: 18438
		public int pingjie;

		// Token: 0x04004807 RID: 18439
		public string desc;
	}
}
