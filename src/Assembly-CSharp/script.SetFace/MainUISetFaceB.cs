using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace script.SetFace;

public class MainUISetFaceB : MonoBehaviour
{
	[SerializeField]
	private Transform setTypeList;

	[SerializeField]
	private GameObject setType;

	[SerializeField]
	private GameObject faceImage;

	[SerializeField]
	private Transform faceImageList;

	public List<MainUIToggle> bigTypeList;

	public SetColor SetColor;

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
			SetColor.HideColorSelect();
		}
		else
		{
			SetColor.HasColorSelect(str, face);
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
		SetFaceUI.Inst.Face.randomAvatar(1);
	}

	public void Init()
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		int num = 0;
		foreach (faceInfoList temp in AvatarFaceDatabase.inst.AllList)
		{
			MainUIToggle toggle = bigTypeList[num];
			toggle.Text = temp.Name;
			((UnityEventBase)toggle.valueChange).RemoveAllListeners();
			if (temp.Name == "脸部" && SetFaceUI.Inst.Lock.activeSelf)
			{
				toggle.Text = "<color=#a6a697>" + temp.Name + "</color>";
				toggle.isDisable = true;
			}
			else
			{
				toggle.isDisable = false;
				toggle.valueChange.AddListener((UnityAction)delegate
				{
					if (toggle.isOn)
					{
						SelectBigType(temp);
					}
				});
			}
			num++;
			if (num > 2)
			{
				break;
			}
		}
		if (SetFaceUI.Inst.Lock.activeSelf)
		{
			bigTypeList[1].MoNiClick();
		}
		else
		{
			bigTypeList[0].MoNiClick();
		}
	}
}
