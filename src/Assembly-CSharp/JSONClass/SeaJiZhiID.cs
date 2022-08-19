using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D6 RID: 2262
	public class SeaJiZhiID : IJSONClass
	{
		// Token: 0x0600416B RID: 16747 RVA: 0x001BFECC File Offset: 0x001BE0CC
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

		// Token: 0x0600416C RID: 16748 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040B5 RID: 16565
		public static Dictionary<int, SeaJiZhiID> DataDict = new Dictionary<int, SeaJiZhiID>();

		// Token: 0x040040B6 RID: 16566
		public static List<SeaJiZhiID> DataList = new List<SeaJiZhiID>();

		// Token: 0x040040B7 RID: 16567
		public static Action OnInitFinishAction = new Action(SeaJiZhiID.OnInitFinish);

		// Token: 0x040040B8 RID: 16568
		public int id;

		// Token: 0x040040B9 RID: 16569
		public int Type;

		// Token: 0x040040BA RID: 16570
		public int TalkID;

		// Token: 0x040040BB RID: 16571
		public int FuBenType;

		// Token: 0x040040BC RID: 16572
		public int XingXiang;

		// Token: 0x040040BD RID: 16573
		public List<int> AvatarID = new List<int>();
	}
}
