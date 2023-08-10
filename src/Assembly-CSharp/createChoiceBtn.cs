using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createChoiceBtn : MonoBehaviour
{
	private void Start()
	{
		((Component)this).GetComponent<UIButton>().onClick.Add(new EventDelegate(resteChoice));
		((Component)this).GetComponent<UIButton>().onClick.Add(new EventDelegate(SetHasColorSelect));
	}

	public void SetHasColorSelect()
	{
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		string text = ((Component)((Component)this).transform.Find("Label")).GetComponent<UILabel>().text;
		Create_face component = ((Component)((Component)this).transform.parent.parent.parent).GetComponent<Create_face>();
		faceInfoDataBaseList baseInfo = component.faceDatabase.getBaseInfo(text);
		string text2 = (component.nowColorstr = jsonData.instance.SuiJiTouXiangGeShuJsonData[baseInfo.JsonInfoName]["colorset"].str);
		if (text2 == "")
		{
			component.hideColorSet();
			return;
		}
		component.ShowColorSet();
		JSONObject colorJson = component.GetColorJson(text2);
		List<int> suijiList = jsonData.instance.getSuijiList(text2, "Sex" + CreateAvatarMag.inst.faceUI.faceDatabase.ListType);
		foreach (Transform item in component.colorsetGrid.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		int num = 0;
		int num2 = (int)jsonData.instance.AvatarRandomJsonData["1"][baseInfo.JsonInfoName].n;
		for (int i = 0; i < suijiList.Count; i++)
		{
			GameObject val2 = Object.Instantiate<GameObject>(component.colorPrefab);
			val2.gameObject.SetActive(true);
			val2.transform.parent = component.colorsetGrid.transform;
			val2.transform.localScale = Vector3.one * 0.6f;
			val2.GetComponent<createImageCell>().SkinIndex = num;
			val2.GetComponent<createImageCell>().SkinType = text2;
			JSONObject jSONObject = colorJson[suijiList[num]];
			((Graphic)((Component)val2.transform.Find("Image1")).GetComponent<Image>()).color = new Color(jSONObject["R"].n / 255f, jSONObject["G"].n / 255f, jSONObject["B"].n / 255f);
			val2.GetComponentInChildren<Toggle>().group = component.colorsetGrid.GetComponent<ToggleGroup>();
			if (num2 == num)
			{
				val2.GetComponentInChildren<Toggle>();
			}
			num++;
		}
	}

	public void resteChoice()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		string text = ((Component)((Component)this).transform.Find("Label")).GetComponent<UILabel>().text;
		Create_face component = ((Component)((Component)this).transform.parent.parent.parent).GetComponent<Create_face>();
		AvatarFaceDatabase faceDatabase = component.faceDatabase;
		foreach (Transform item in component.goodsGrid.transform)
		{
			Object.Destroy((Object)(object)((Component)item).gameObject);
		}
		faceInfoDataBaseList baseInfo = faceDatabase.getBaseInfo(text);
		if (baseInfo != null)
		{
			int num = 0;
			int num2 = (int)jsonData.instance.AvatarRandomJsonData["1"][baseInfo.JsonInfoName].n;
			{
				foreach (faceInfoDataBase face in baseInfo.faceList)
				{
					GameObject val = Object.Instantiate<GameObject>(component.faceItmePrefab);
					val.gameObject.SetActive(true);
					val.transform.parent = component.goodsGrid.transform;
					val.transform.localScale = Vector3.one;
					val.GetComponent<createImageCell>().SkinIndex = num;
					val.GetComponent<createImageCell>().SkinType = baseInfo.JsonInfoName;
					setImage(val, "Image1", face.Image1);
					setImage(val, "Image2", face.Image2);
					setImage(val, "Image3", face.Image3);
					val.GetComponentInChildren<Toggle>().group = component.goodsGrid.GetComponent<ToggleGroup>();
					if (num2 == num)
					{
						val.GetComponentInChildren<Toggle>();
					}
					num++;
				}
				return;
			}
		}
		UIPopTip.Inst.Pop("找不到类型" + text);
	}

	private void setImage(GameObject _temp, string imageName, Sprite img)
	{
		if ((Object)(object)img == (Object)null)
		{
			((Behaviour)((Component)_temp.transform.Find(imageName)).GetComponent<Image>()).enabled = false;
		}
		else
		{
			((Component)_temp.transform.Find(imageName)).GetComponent<Image>().sprite = img;
		}
	}
}
