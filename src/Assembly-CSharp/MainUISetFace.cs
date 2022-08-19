using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200032C RID: 812
public class MainUISetFace : MonoBehaviour
{
	// Token: 0x06001BFA RID: 7162 RVA: 0x000C8620 File Offset: 0x000C6820
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

	// Token: 0x06001BFB RID: 7163 RVA: 0x000C8684 File Offset: 0x000C6884
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

	// Token: 0x06001BFC RID: 7164 RVA: 0x000C8788 File Offset: 0x000C6988
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

	// Token: 0x06001BFD RID: 7165 RVA: 0x000C891C File Offset: 0x000C6B1C
	public void setFace(string skinType, int skinIndex)
	{
		List<int> suijiList = jsonData.instance.getSuijiList(skinType, "AvatarSex" + AvatarFaceDatabase.inst.ListType);
		jsonData.instance.AvatarRandomJsonData["1"].SetField(skinType, suijiList[skinIndex]);
		MainUIPlayerInfo.inst.playerFace.randomAvatar(1);
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x000C8980 File Offset: 0x000C6B80
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

	// Token: 0x06001BFF RID: 7167 RVA: 0x000C8A74 File Offset: 0x000C6C74
	public void ReturnMethod()
	{
		base.gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.facePanel.SetActive(false);
		MainUIMag.inst.createAvatarPanel.setNamePanel.Init();
	}

	// Token: 0x06001C00 RID: 7168 RVA: 0x000C8AAC File Offset: 0x000C6CAC
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

	// Token: 0x06001C01 RID: 7169 RVA: 0x000C8B62 File Offset: 0x000C6D62
	public void SelectMan()
	{
		if (this.manToggle.isOn)
		{
			AvatarFaceDatabase.inst.ListType = 1;
			MainUIPlayerInfo.inst.sex = 1;
			this.RandomFace();
		}
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x000C8B8D File Offset: 0x000C6D8D
	public void SelectWomen()
	{
		if (this.womenToggle.isOn)
		{
			AvatarFaceDatabase.inst.ListType = 2;
			MainUIPlayerInfo.inst.sex = 2;
			this.RandomFace();
		}
	}

	// Token: 0x06001C03 RID: 7171 RVA: 0x000C8BB8 File Offset: 0x000C6DB8
	public void NextMethod()
	{
		base.gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.setTianFu.Init();
	}

	// Token: 0x04001698 RID: 5784
	private bool isInit;

	// Token: 0x04001699 RID: 5785
	[SerializeField]
	private Transform setTypeList;

	// Token: 0x0400169A RID: 5786
	[SerializeField]
	private GameObject setType;

	// Token: 0x0400169B RID: 5787
	[SerializeField]
	private GameObject faceImage;

	// Token: 0x0400169C RID: 5788
	[SerializeField]
	private Transform faceImageList;

	// Token: 0x0400169D RID: 5789
	[SerializeField]
	private MainUIToggle manToggle;

	// Token: 0x0400169E RID: 5790
	[SerializeField]
	private MainUIToggle womenToggle;

	// Token: 0x0400169F RID: 5791
	public List<MainUIToggle> bigTypeList;

	// Token: 0x040016A0 RID: 5792
	public MainUISetColor setColor;
}
