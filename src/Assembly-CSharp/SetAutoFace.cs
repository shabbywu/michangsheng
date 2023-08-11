using System;
using System.Collections.Generic;
using UnityEngine;
using YSGame;

public class SetAutoFace : MonoBehaviour
{
	[Header("角色名称")]
	public string AvatarName;

	[Header("角色ID")]
	public int AvatarID;

	public List<StaticFaceInfo> StaticRandomInfo = new List<StaticFaceInfo>();

	public List<AvatarFaceInfo> RandomInfo = new List<AvatarFaceInfo>();

	private void Awake()
	{
		StaticRandomInfo = SetAvatarFaceRandomInfo.inst.StaticRandomInfo;
	}

	public void SetStaticRandomInfo()
	{
		StaticFaceInfo staticFaceInfo = new StaticFaceInfo();
		for (int i = 0; i < StaticRandomInfo.Count; i++)
		{
			if (StaticRandomInfo[i].AvatarScope == AvatarID)
			{
				StaticRandomInfo[i].name = AvatarName;
				StaticRandomInfo[i].AvatarScope = AvatarID;
				StaticRandomInfo[i].faceinfoList = getCurDate();
				return;
			}
		}
		staticFaceInfo.name = AvatarName;
		staticFaceInfo.AvatarScope = AvatarID;
		staticFaceInfo.faceinfoList = getCurDate();
		StaticRandomInfo.Add(staticFaceInfo);
	}

	public void showCurAvatarByAvatarID()
	{
		for (int i = 0; i < StaticRandomInfo.Count; i++)
		{
			if (StaticRandomInfo[i].AvatarScope == AvatarID)
			{
				for (int j = 0; j < StaticRandomInfo[i].faceinfoList.Count; j++)
				{
					jsonData.instance.AvatarRandomJsonData["1"].SetField(StaticRandomInfo[i].faceinfoList[j].SkinTypeName.ToString(), StaticRandomInfo[i].faceinfoList[j].SkinTypeScope);
				}
				jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", jsonData.instance.AvatarJsonData[StaticRandomInfo[i].AvatarScope.ToString()]["SexType"].I);
				AvatarName = StaticRandomInfo[i].name;
				MainUIPlayerInfo.inst.playerFace.randomAvatar(1);
				break;
			}
		}
	}

	public void DaoChuDate()
	{
		ResManager.inst.SaveStream("Data/StaticRandomInfo.bin", StaticRandomInfo);
	}

	public void getOldDate()
	{
		StaticRandomInfo = SetAvatarFaceRandomInfo.inst.StaticRandomInfo;
	}

	private List<StaticFaceRandomInfo> getCurDate()
	{
		List<StaticFaceRandomInfo> list = new List<StaticFaceRandomInfo>();
		JSONObject jSONObject = jsonData.instance.AvatarRandomJsonData["1"];
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2 = ((jSONObject["Sex"].I != 1) ? jsonData.instance.NvZuJianBiao : jsonData.instance.NanZuJianBiao);
		for (int i = 0; i < jSONObject2.Count; i++)
		{
			StaticFaceRandomInfo staticFaceRandomInfo = new StaticFaceRandomInfo();
			Enum.TryParse<SetAvatarFaceRandomInfo.InfoName>(jSONObject2[i]["StrID"].str, ignoreCase: true, out staticFaceRandomInfo.SkinTypeName);
			staticFaceRandomInfo.SkinTypeScope = jSONObject[jSONObject2[i]["StrID"].str].I;
			list.Add(staticFaceRandomInfo);
		}
		return list;
	}
}
