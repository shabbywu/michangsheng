using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200058D RID: 1421
public class createChoiceBtn : MonoBehaviour
{
	// Token: 0x06002409 RID: 9225 RVA: 0x00126A8C File Offset: 0x00124C8C
	private void Start()
	{
		base.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.resteChoice)));
		base.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.SetHasColorSelect)));
	}

	// Token: 0x0600240A RID: 9226 RVA: 0x00126ADC File Offset: 0x00124CDC
	public void SetHasColorSelect()
	{
		string text = base.transform.Find("Label").GetComponent<UILabel>().text;
		Create_face component = base.transform.parent.parent.parent.GetComponent<Create_face>();
		faceInfoDataBaseList baseInfo = component.faceDatabase.getBaseInfo(text);
		string str = jsonData.instance.SuiJiTouXiangGeShuJsonData[baseInfo.JsonInfoName]["colorset"].str;
		component.nowColorstr = str;
		if (str == "")
		{
			component.hideColorSet();
			return;
		}
		component.ShowColorSet();
		JSONObject colorJson = component.GetColorJson(str);
		List<int> suijiList = jsonData.instance.getSuijiList(str, "Sex" + CreateAvatarMag.inst.faceUI.faceDatabase.ListType);
		foreach (object obj in component.colorsetGrid.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		int num = 0;
		int num2 = (int)jsonData.instance.AvatarRandomJsonData["1"][baseInfo.JsonInfoName].n;
		for (int i = 0; i < suijiList.Count; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(component.colorPrefab);
			gameObject.gameObject.SetActive(true);
			gameObject.transform.parent = component.colorsetGrid.transform;
			gameObject.transform.localScale = Vector3.one * 0.6f;
			gameObject.GetComponent<createImageCell>().SkinIndex = num;
			gameObject.GetComponent<createImageCell>().SkinType = str;
			JSONObject jsonobject = colorJson[suijiList[num]];
			gameObject.transform.Find("Image1").GetComponent<Image>().color = new Color(jsonobject["R"].n / 255f, jsonobject["G"].n / 255f, jsonobject["B"].n / 255f);
			gameObject.GetComponentInChildren<Toggle>().group = component.colorsetGrid.GetComponent<ToggleGroup>();
			if (num2 == num)
			{
				gameObject.GetComponentInChildren<Toggle>();
			}
			num++;
		}
	}

	// Token: 0x0600240B RID: 9227 RVA: 0x00126D68 File Offset: 0x00124F68
	public void resteChoice()
	{
		string text = base.transform.Find("Label").GetComponent<UILabel>().text;
		Create_face component = base.transform.parent.parent.parent.GetComponent<Create_face>();
		AvatarFaceDatabase faceDatabase = component.faceDatabase;
		foreach (object obj in component.goodsGrid.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		faceInfoDataBaseList baseInfo = faceDatabase.getBaseInfo(text);
		if (baseInfo != null)
		{
			int num = 0;
			int num2 = (int)jsonData.instance.AvatarRandomJsonData["1"][baseInfo.JsonInfoName].n;
			using (List<faceInfoDataBase>.Enumerator enumerator2 = baseInfo.faceList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					faceInfoDataBase faceInfoDataBase = enumerator2.Current;
					GameObject gameObject = Object.Instantiate<GameObject>(component.faceItmePrefab);
					gameObject.gameObject.SetActive(true);
					gameObject.transform.parent = component.goodsGrid.transform;
					gameObject.transform.localScale = Vector3.one;
					gameObject.GetComponent<createImageCell>().SkinIndex = num;
					gameObject.GetComponent<createImageCell>().SkinType = baseInfo.JsonInfoName;
					this.setImage(gameObject, "Image1", faceInfoDataBase.Image1);
					this.setImage(gameObject, "Image2", faceInfoDataBase.Image2);
					this.setImage(gameObject, "Image3", faceInfoDataBase.Image3);
					gameObject.GetComponentInChildren<Toggle>().group = component.goodsGrid.GetComponent<ToggleGroup>();
					if (num2 == num)
					{
						gameObject.GetComponentInChildren<Toggle>();
					}
					num++;
				}
				return;
			}
		}
		UIPopTip.Inst.Pop("找不到类型" + text, PopTipIconType.叹号);
	}

	// Token: 0x0600240C RID: 9228 RVA: 0x0001D154 File Offset: 0x0001B354
	private void setImage(GameObject _temp, string imageName, Sprite img)
	{
		if (img == null)
		{
			_temp.transform.Find(imageName).GetComponent<Image>().enabled = false;
			return;
		}
		_temp.transform.Find(imageName).GetComponent<Image>().sprite = img;
	}
}
