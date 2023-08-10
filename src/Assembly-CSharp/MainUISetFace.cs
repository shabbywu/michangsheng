using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainUISetFace : MonoBehaviour
{
	private bool isInit;

	[SerializeField]
	private Transform setTypeList;

	[SerializeField]
	private GameObject setType;

	[SerializeField]
	private GameObject faceImage;

	[SerializeField]
	private Transform faceImageList;

	[SerializeField]
	private MainUIToggle manToggle;

	[SerializeField]
	private MainUIToggle womenToggle;

	public List<MainUIToggle> bigTypeList;

	public MainUISetColor setColor;

	public void Init()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		if (!isInit)
		{
			manToggle.valueChange.AddListener(new UnityAction(SelectMan));
			womenToggle.valueChange.AddListener(new UnityAction(SelectWomen));
			manToggle.MoNiClick();
		}
		((Component)this).gameObject.SetActive(true);
	}

	private void SelectBigType(faceInfoList face)
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		Tools.ClearObj(setType.transform);
		int num = 0;
		foreach (faceInfoDataBaseList data in face.faceList)
		{
			if (data == null)
			{
				continue;
			}
			GameObject val = Object.Instantiate<GameObject>(setType, setTypeList);
			MainUIToggle toggle = val.GetComponent<MainUIToggle>();
			toggle.Text = data.Name;
			toggle.valueChange.AddListener((UnityAction)delegate
			{
				if (toggle.isOn)
				{
					SelectSmallType(data);
				}
			});
			val.SetActive(true);
			if (num == 0)
			{
				toggle.MoNiClick();
			}
			num++;
		}
	}

	private void SelectSmallType(faceInfoDataBaseList face)
	{
		Tools.ClearObj(faceImage.transform);
		int num = 0;
		int i = jsonData.instance.AvatarRandomJsonData["1"][face.JsonInfoName].I;
		string str = jsonData.instance.SuiJiTouXiangGeShuJsonData[face.JsonInfoName]["colorset"].str;
		if (str == "")
		{
			setColor.HideColorSelect();
		}
		else
		{
			setColor.HasColorSelect(str, face);
		}
		foreach (faceInfoDataBase face2 in face.faceList)
		{
			if (face2 == null)
			{
				continue;
			}
			GameObject val = Object.Instantiate<GameObject>(faceImage, faceImageList);
			createImageCell imageCell = val.GetComponent<createImageCell>();
			imageCell.SkinIndex = num;
			imageCell.SkinType = face.JsonInfoName;
			imageCell.SetImage(face2.Image1, face2.Image2, face2.Image3);
			if (i == num)
			{
				imageCell.toggle.isOn = true;
			}
			((UnityEvent<bool>)(object)imageCell.toggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool b)
			{
				if (b)
				{
					setFace(imageCell.SkinType, imageCell.SkinIndex);
				}
			});
			val.SetActive(true);
			num++;
		}
	}

	public void setFace(string skinType, int skinIndex)
	{
		List<int> suijiList = jsonData.instance.getSuijiList(skinType, "AvatarSex" + AvatarFaceDatabase.inst.ListType);
		jsonData.instance.AvatarRandomJsonData["1"].SetField(skinType, suijiList[skinIndex]);
		MainUIPlayerInfo.inst.playerFace.randomAvatar(1);
	}

	public void RestInit()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		int num = 0;
		foreach (faceInfoList temp in AvatarFaceDatabase.inst.AllList)
		{
			MainUIToggle toggle = bigTypeList[num];
			toggle.Text = temp.Name;
			((UnityEventBase)toggle.valueChange).RemoveAllListeners();
			toggle.valueChange.AddListener((UnityAction)delegate
			{
				if (toggle.isOn)
				{
					SelectBigType(temp);
				}
			});
			num++;
			if (num > 2)
			{
				break;
			}
		}
		bigTypeList[0].MoNiClick();
		isInit = true;
	}

	public void ReturnMethod()
	{
		((Component)this).gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.facePanel.SetActive(false);
		MainUIMag.inst.createAvatarPanel.setNamePanel.Init();
	}

	public void RandomFace()
	{
		jsonData.instance.AvatarRandomJsonData = new JSONObject(JSONObject.Type.OBJECT);
		jsonData.instance.AvatarJsonData["1"].SetField("SexType", MainUIPlayerInfo.inst.sex);
		jsonData.instance.refreshMonstar(1);
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Name", MainUIPlayerInfo.inst.playerName);
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", MainUIPlayerInfo.inst.sex);
		MainUIPlayerInfo.inst.playerFace.randomAvatar(1);
		RestInit();
	}

	public void SelectMan()
	{
		if (manToggle.isOn)
		{
			AvatarFaceDatabase.inst.ListType = 1;
			MainUIPlayerInfo.inst.sex = 1;
			RandomFace();
		}
	}

	public void SelectWomen()
	{
		if (womenToggle.isOn)
		{
			AvatarFaceDatabase.inst.ListType = 2;
			MainUIPlayerInfo.inst.sex = 2;
			RandomFace();
		}
	}

	public void NextMethod()
	{
		((Component)this).gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.setTianFu.Init();
	}
}
