using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C63 RID: 3171
	public class SeaJiZhiID : IJSONClass
	{
		// Token: 0x06004CF5 RID: 19701 RVA: 0x002082E4 File Offset: 0x002064E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SeaJiZhiID.list)
			{
				try
				{
					SeaJiZhiID seaJiZhiID = new SeaJiZhiID();
					seaJiZhiID.id = jsonobject["id"].I;
					seaJiZhiID.Type = jsonobject["Type"].I;
					seaJiZhiID.TalkID = jsonobject["TalkID"].I;
					seaJiZhiID.FuBenType = jsonobject["FuBenType"].I;
					seaJiZhiID.XingXiang = jsonobject["XingXiang"].I;
					seaJiZhiID.AvatarID = jsonobject["AvatarID"].ToList();
					if (SeaJiZhiID.DataDict.ContainsKey(seaJiZhiID.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SeaJiZhiID.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", seaJiZhiID.id));
					}
					else
					{
						SeaJiZhiID.DataDict.Add(seaJiZhiID.id, seaJiZhiID);
						SeaJiZhiID.DataList.Add(seaJiZhiID);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SeaJiZhiID.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SeaJiZhiID.OnInitFinishAction != null)
			{
				SeaJiZhiID.OnInitFinishAction();
			}
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BFF RID: 19455
		public static Dictionary<int, SeaJiZhiID> DataDict = new Dictionary<int, SeaJiZhiID>();

		// Token: 0x04004C00 RID: 19456
		public static List<SeaJiZhiID> DataList = new List<SeaJiZhiID>();

		// Token: 0x04004C01 RID: 19457
		public static Action OnInitFinishAction = new Action(SeaJiZhiID.OnInitFinish);

		// Token: 0x04004C02 RID: 19458
		public int id;

		// Token: 0x04004C03 RID: 19459
		public int Type;

		// Token: 0x04004C04 RID: 19460
		public int TalkID;

		// Token: 0x04004C05 RID: 19461
		public int FuBenType;

		// Token: 0x04004C06 RID: 19462
		public int XingXiang;

		// Token: 0x04004C07 RID: 19463
		public List<int> AvatarID = new List<int>();
	}
}
