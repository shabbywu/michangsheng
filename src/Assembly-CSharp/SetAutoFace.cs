using System;
using System.Collections.Generic;
using UnityEngine;
using YSGame;

// Token: 0x020001B1 RID: 433
public class SetAutoFace : MonoBehaviour
{
	// Token: 0x06001232 RID: 4658 RVA: 0x0006E7CE File Offset: 0x0006C9CE
	private void Awake()
	{
		this.StaticRandomInfo = SetAvatarFaceRandomInfo.inst.StaticRandomInfo;
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x0006E7E0 File Offset: 0x0006C9E0
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

	// Token: 0x06001234 RID: 4660 RVA: 0x0006E898 File Offset: 0x0006CA98
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

	// Token: 0x06001235 RID: 4661 RVA: 0x0006E9D4 File Offset: 0x0006CBD4
	public void DaoChuDate()
	{
		ResManager.inst.SaveStream("Data/StaticRandomInfo.bin", this.StaticRandomInfo);
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x0006E7CE File Offset: 0x0006C9CE
	public void getOldDate()
	{
		this.StaticRandomInfo = SetAvatarFaceRandomInfo.inst.StaticRandomInfo;
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x0006E9EC File Offset: 0x0006CBEC
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

	// Token: 0x04000CDE RID: 3294
	[Header("角色名称")]
	public string AvatarName;

	// Token: 0x04000CDF RID: 3295
	[Header("角色ID")]
	public int AvatarID;

	// Token: 0x04000CE0 RID: 3296
	public List<StaticFaceInfo> StaticRandomInfo = new List<StaticFaceInfo>();

	// Token: 0x04000CE1 RID: 3297
	public List<AvatarFaceInfo> RandomInfo = new List<AvatarFaceInfo>();
}
