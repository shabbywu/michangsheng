using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000495 RID: 1173
public class MainUISetFace : MonoBehaviour
{
	// Token: 0x06001F44 RID: 8004 RVA: 0x0010DC18 File Offset: 0x0010BE18
	public void Init()
	{
		if (!this.isInit)
		{
			this.manToggle.valueChange.AddListener(new UnityAction(this.SelectMan));
			this.womenToggle.valueChange.AddListener(new UnityAction(this.SelectWomen));
			this.manToggle.MoNiClick();
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x0010DC7C File Offset: 0x0010BE7C
	private void SelectBigType(faceInfoList face)
	{
		Tools.ClearObj(this.setType.transform);
		int num = 0;
		using (List<faceInfoDataBaseList>.Enumerator enumerator = face.faceList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MainUISetFace.<>c__DisplayClass10_0 CS$<>8__locals1 = new MainUISetFace.<>c__DisplayClass10_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.data = enumerator.Current;
				if (CS$<>8__locals1.data != null)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.setType, this.setTypeList);
					MainUIToggle toggle = gameObject.GetComponent<MainUIToggle>();
					toggle.Text = CS$<>8__locals1.data.Name;
					toggle.valueChange.AddListener(delegate()
					{
						if (toggle.isOn)
						{
							CS$<>8__locals1.<>4__this.SelectSmallType(CS$<>8__locals1.data);
						}
					});
					gameObject.SetActive(true);
					if (num == 0)
					{
						toggle.MoNiClick();
					}
					num++;
				}
			}
		}
	}

	// Token: 0x06001F46 RID: 8006 RVA: 0x0010DD80 File Offset: 0x0010BF80
	private void SelectSmallType(faceInfoDataBaseList face)
	{
		Tools.ClearObj(this.faceImage.transform);
		int num = 0;
		int i = jsonData.instance.AvatarRandomJsonData["1"][face.JsonInfoName].I;
		string str = jsonData.instance.SuiJiTouXiangGeShuJsonData[face.JsonInfoName]["colorset"].str;
		if (str == "")
		{
			this.setColor.HideColorSelect();
		}
		else
		{
			this.setColor.HasColorSelect(str, face);
		}
		foreach (faceInfoDataBase faceInfoDataBase in face.faceList)
		{
			if (faceInfoDataBase != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.faceImage, this.faceImageList);
				createImageCell imageCell = gameObject.GetComponent<createImageCell>();
				imageCell.SkinIndex = num;
				imageCell.SkinType = face.JsonInfoName;
				imageCell.SetImage(faceInfoDataBase.Image1, faceInfoDataBase.Image2, faceInfoDataBase.Image3);
				if (i == num)
				{
					imageCell.toggle.isOn = true;
				}
				imageCell.toggle.onValueChanged.AddListener(delegate(bool b)
				{
					if (b)
					{
						this.setFace(imageCell.SkinType, imageCell.SkinIndex);
					}
				});
				gameObject.SetActive(true);
				num++;
			}
		}
	}

	// Token: 0x06001F47 RID: 8007 RVA: 0x0010DF14 File Offset: 0x0010C114
	public void setFace(string skinType, int skinIndex)
	{
		List<int> suijiList = jsonData.instance.getSuijiList(skinType, "AvatarSex" + AvatarFaceDatabase.inst.ListType);
		jsonData.instance.AvatarRandomJsonData["1"].SetField(skinType, suijiList[skinIndex]);
		MainUIPlayerInfo.inst.playerFace.randomAvatar(1);
	}

	// Token: 0x06001F48 RID: 8008 RVA: 0x0010DF78 File Offset: 0x0010C178
	public void RestInit()
	{
		int num = 0;
		using (List<faceInfoList>.Enumerator enumerator = AvatarFaceDatabase.inst.AllList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MainUISetFace.<>c__DisplayClass13_0 CS$<>8__locals1 = new MainUISetFace.<>c__DisplayClass13_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.temp = enumerator.Current;
				MainUIToggle toggle = this.bigTypeList[num];
				toggle.Text = CS$<>8__locals1.temp.Name;
				toggle.valueChange.RemoveAllListeners();
				toggle.valueChange.AddListener(delegate()
				{
					if (toggle.isOn)
					{
						CS$<>8__locals1.<>4__this.SelectBigType(CS$<>8__locals1.temp);
					}
				});
				num++;
				if (num > 2)
				{
					break;
				}
			}
		}
		this.bigTypeList[0].MoNiClick();
		this.isInit = true;
	}

	// Token: 0x06001F49 RID: 8009 RVA: 0x00019CFA File Offset: 0x00017EFA
	public void ReturnMethod()
	{
		base.gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.facePanel.SetActive(false);
		MainUIMag.inst.createAvatarPanel.setNamePanel.Init();
	}

	// Token: 0x06001F4A RID: 8010 RVA: 0x0010E06C File Offset: 0x0010C26C
	public void RandomFace()
	{
		jsonData.instance.AvatarRandomJsonData = new JSONObject(JSONObject.Type.OBJECT);
		jsonData.instance.AvatarJsonData["1"].SetField("SexType", MainUIPlayerInfo.inst.sex);
		jsonData.instance.refreshMonstar(1);
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Name", MainUIPlayerInfo.inst.playerName);
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", MainUIPlayerInfo.inst.sex);
		MainUIPlayerInfo.inst.playerFace.randomAvatar(1);
		this.RestInit();
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x00019D31 File Offset: 0x00017F31
	public void SelectMan()
	{
		if (this.manToggle.isOn)
		{
			AvatarFaceDatabase.inst.ListType = 1;
			MainUIPlayerInfo.inst.sex = 1;
			this.RandomFace();
		}
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x00019D5C File Offset: 0x00017F5C
	public void SelectWomen()
	{
		if (this.womenToggle.isOn)
		{
			AvatarFaceDatabase.inst.ListType = 2;
			MainUIPlayerInfo.inst.sex = 2;
			this.RandomFace();
		}
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x00019D87 File Offset: 0x00017F87
	public void NextMethod()
	{
		base.gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.setTianFu.Init();
	}

	// Token: 0x04001AC4 RID: 6852
	private bool isInit;

	// Token: 0x04001AC5 RID: 6853
	[SerializeField]
	private Transform setTypeList;

	// Token: 0x04001AC6 RID: 6854
	[SerializeField]
	private GameObject setType;

	// Token: 0x04001AC7 RID: 6855
	[SerializeField]
	private GameObject faceImage;

	// Token: 0x04001AC8 RID: 6856
	[SerializeField]
	private Transform faceImageList;

	// Token: 0x04001AC9 RID: 6857
	[SerializeField]
	private MainUIToggle manToggle;

	// Token: 0x04001ACA RID: 6858
	[SerializeField]
	private MainUIToggle womenToggle;

	// Token: 0x04001ACB RID: 6859
	public List<MainUIToggle> bigTypeList;

	// Token: 0x04001ACC RID: 6860
	public MainUISetColor setColor;
}
