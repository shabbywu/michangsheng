using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000833 RID: 2099
	public class DropInfoJsonData : IJSONClass
	{
		// Token: 0x06003EDE RID: 16094 RVA: 0x001ADB38 File Offset: 0x001ABD38
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

		// Token: 0x06003EDF RID: 16095 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AA1 RID: 15009
		public static Dictionary<int, DropInfoJsonData> DataDict = new Dictionary<int, DropInfoJsonData>();

		// Token: 0x04003AA2 RID: 15010
		public static List<DropInfoJsonData> DataList = new List<DropInfoJsonData>();

		// Token: 0x04003AA3 RID: 15011
		public static Action OnInitFinishAction = new Action(DropInfoJsonData.OnInitFinish);

		// Token: 0x04003AA4 RID: 15012
		public int id;

		// Token: 0x04003AA5 RID: 15013
		public int dropType;

		// Token: 0x04003AA6 RID: 15014
		public int loseHp;

		// Token: 0x04003AA7 RID: 15015
		public int round;

		// Token: 0x04003AA8 RID: 15016
		public int moneydrop;

		// Token: 0x04003AA9 RID: 15017
		public int backpack;

		// Token: 0x04003AAA RID: 15018
		public int wepen;

		// Token: 0x04003AAB RID: 15019
		public int cloth;

		// Token: 0x04003AAC RID: 15020
		public int ring;

		// Token: 0x04003AAD RID: 15021
		public string Title;

		// Token: 0x04003AAE RID: 15022
		public string TextDesc;
	}
}
