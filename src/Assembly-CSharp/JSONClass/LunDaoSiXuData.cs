using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C16 RID: 3094
	public class LunDaoSiXuData : IJSONClass
	{
		// Token: 0x06004BC1 RID: 19393 RVA: 0x001FFA2C File Offset: 0x001FDC2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LunDaoSiXuData.list)
			{
				try
				{
					LunDaoSiXuData lunDaoSiXuData = new LunDaoSiXuData();
					lunDaoSiXuData.id = jsonobject["id"].I;
					lunDaoSiXuData.PinJie = jsonobject["PinJie"].I;
					lunDaoSiXuData.SiXvXiaoLv = jsonobject["SiXvXiaoLv"].I;
					if (LunDaoSiXuData.DataDict.ContainsKey(lunDaoSiXuData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LunDaoSiXuData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lunDaoSiXuData.id));
					}
					else
					{
						LunDaoSiXuData.DataDict.Add(lunDaoSiXuData.id, lunDaoSiXuData);
						LunDaoSiXuData.DataList.Add(lunDaoSiXuData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LunDaoSiXuData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LunDaoSiXuData.OnInitFinishAction != null)
			{
				LunDaoSiXuData.OnInitFinishAction();
			}
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048A6 RID: 18598
		public static Dictionary<int, LunDaoSiXuData> DataDict = new Dictionary<int, LunDaoSiXuData>();

		// Token: 0x040048A7 RID: 18599
		public static List<LunDaoSiXuData> DataList = new List<LunDaoSiXuData>();

		// Token: 0x040048A8 RID: 18600
		public static Action OnInitFinishAction = new Action(LunDaoSiXuData.OnInitFinish);

		// Token: 0x040048A9 RID: 18601
		public int id;

		// Token: 0x040048AA RID: 18602
		public int PinJie;

		// Token: 0x040048AB RID: 18603
		public int SiXvXiaoLv;
	}
}
