using System;
using System.Collections.Generic;
using UnityEngine;
using YSGame;

// Token: 0x020002AF RID: 687
public class SetAutoFace : MonoBehaviour
{
	// Token: 0x060014DD RID: 5341 RVA: 0x0001326C File Offset: 0x0001146C
	private void Awake()
	{
		this.StaticRandomInfo = SetAvatarFaceRandomInfo.inst.StaticRandomInfo;
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x000BC4BC File Offset: 0x000BA6BC
	public void SetStaticRandomInfo()
	{
		StaticFaceInfo staticFaceInfo = new StaticFaceInfo();
		for (int i = 0; i < this.StaticRandomInfo.Count; i++)
		{
			if (this.StaticRandomInfo[i].AvatarScope == this.AvatarID)
			{
				this.StaticRandomInfo[i].name = this.AvatarName;
				this.StaticRandomInfo[i].AvatarScope = this.AvatarID;
				this.StaticRandomInfo[i].faceinfoList = this.getCurDate();
				return;
			}
		}
		staticFaceInfo.name = this.AvatarName;
		staticFaceInfo.AvatarScope = this.AvatarID;
		staticFaceInfo.faceinfoList = this.getCurDate();
		this.StaticRandomInfo.Add(staticFaceInfo);
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000BC574 File Offset: 0x000BA774
	public void showCurAvatarByAvatarID()
	{
		for (int i = 0; i < this.StaticRandomInfo.Count; i++)
		{
			if (this.StaticRandomInfo[i].AvatarScope == this.AvatarID)
			{
				for (int j = 0; j < this.StaticRandomInfo[i].faceinfoList.Count; j++)
				{
					jsonData.instance.AvatarRandomJsonData["1"].SetField(this.StaticRandomInfo[i].faceinfoList[j].SkinTypeName.ToString(), this.StaticRandomInfo[i].faceinfoList[j].SkinTypeScope);
				}
				jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", jsonData.instance.AvatarJsonData[this.StaticRandomInfo[i].AvatarScope.ToString()]["SexType"].I);
				this.AvatarName = this.StaticRandomInfo[i].name;
				MainUIPlayerInfo.inst.playerFace.randomAvatar(1);
				return;
			}
		}
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x0001327E File Offset: 0x0001147E
	public void DaoChuDate()
	{
		ResManager.inst.SaveStream("Data/StaticRandomInfo.bin", this.StaticRandomInfo);
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x0001326C File Offset: 0x0001146C
	public void getOldDate()
	{
		this.StaticRandomInfo = SetAvatarFaceRandomInfo.inst.StaticRandomInfo;
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x000BC6B0 File Offset: 0x000BA8B0
	private List<StaticFaceRandomInfo> getCurDate()
	{
		List<StaticFaceRandomInfo> list = new List<StaticFaceRandomInfo>();
		JSONObject jsonobject = jsonData.instance.AvatarRandomJsonData["1"];
		JSONObject jsonobject2 = new JSONObject();
		if (jsonobject["Sex"].I == 1)
		{
			jsonobject2 = jsonData.instance.NanZuJianBiao;
		}
		else
		{
			jsonobject2 = jsonData.instance.NvZuJianBiao;
		}
		for (int i = 0; i < jsonobject2.Count; i++)
		{
			StaticFaceRandomInfo staticFaceRandomInfo = new StaticFaceRandomInfo();
			Enum.TryParse<SetAvatarFaceRandomInfo.InfoName>(jsonobject2[i]["StrID"].str, true, out staticFaceRandomInfo.SkinTypeName);
			staticFaceRandomInfo.SkinTypeScope = jsonobject[jsonobject2[i]["StrID"].str].I;
			list.Add(staticFaceRandomInfo);
		}
		return list;
	}

	// Token: 0x04001009 RID: 4105
	[Header("角色名称")]
	public string AvatarName;

	// Token: 0x0400100A RID: 4106
	[Header("角色ID")]
	public int AvatarID;

	// Token: 0x0400100B RID: 4107
	public List<StaticFaceInfo> StaticRandomInfo = new List<StaticFaceInfo>();

	// Token: 0x0400100C RID: 4108
	public List<AvatarFaceInfo> RandomInfo = new List<AvatarFaceInfo>();
}
