using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC7 RID: 3015
	public class DropInfoJsonData : IJSONClass
	{
		// Token: 0x06004A84 RID: 19076 RVA: 0x001F8950 File Offset: 0x001F6B50
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DropInfoJsonData.list)
			{
				try
				{
					DropInfoJsonData dropInfoJsonData = new DropInfoJsonData();
					dropInfoJsonData.id = jsonobject["id"].I;
					dropInfoJsonData.dropType = jsonobject["dropType"].I;
					dropInfoJsonData.loseHp = jsonobject["loseHp"].I;
					dropInfoJsonData.round = jsonobject["round"].I;
					dropInfoJsonData.moneydrop = jsonobject["moneydrop"].I;
					dropInfoJsonData.backpack = jsonobject["backpack"].I;
					dropInfoJsonData.wepen = jsonobject["wepen"].I;
					dropInfoJsonData.cloth = jsonobject["cloth"].I;
					dropInfoJsonData.ring = jsonobject["ring"].I;
					dropInfoJsonData.Title = jsonobject["Title"].Str;
					dropInfoJsonData.TextDesc = jsonobject["TextDesc"].Str;
					if (DropInfoJsonData.DataDict.ContainsKey(dropInfoJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DropInfoJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", dropInfoJsonData.id));
					}
					else
					{
						DropInfoJsonData.DataDict.Add(dropInfoJsonData.id, dropInfoJsonData);
						DropInfoJsonData.DataList.Add(dropInfoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DropInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DropInfoJsonData.OnInitFinishAction != null)
			{
				DropInfoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400461E RID: 17950
		public static Dictionary<int, DropInfoJsonData> DataDict = new Dictionary<int, DropInfoJsonData>();

		// Token: 0x0400461F RID: 17951
		public static List<DropInfoJsonData> DataList = new List<DropInfoJsonData>();

		// Token: 0x04004620 RID: 17952
		public static Action OnInitFinishAction = new Action(DropInfoJsonData.OnInitFinish);

		// Token: 0x04004621 RID: 17953
		public int id;

		// Token: 0x04004622 RID: 17954
		public int dropType;

		// Token: 0x04004623 RID: 17955
		public int loseHp;

		// Token: 0x04004624 RID: 17956
		public int round;

		// Token: 0x04004625 RID: 17957
		public int moneydrop;

		// Token: 0x04004626 RID: 17958
		public int backpack;

		// Token: 0x04004627 RID: 17959
		public int wepen;

		// Token: 0x04004628 RID: 17960
		public int cloth;

		// Token: 0x04004629 RID: 17961
		public int ring;

		// Token: 0x0400462A RID: 17962
		public string Title;

		// Token: 0x0400462B RID: 17963
		public string TextDesc;
	}
}
