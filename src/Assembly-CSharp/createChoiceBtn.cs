using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003EC RID: 1004
public class createChoiceBtn : MonoBehaviour
{
	// Token: 0x06002076 RID: 8310 RVA: 0x000E4720 File Offset: 0x000E2920
	private void Start()
	{
		base.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.resteChoice)));
		base.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.SetHasColorSelect)));
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x000E4770 File Offset: 0x000E2970
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

	// Token: 0x06002078 RID: 8312 RVA: 0x000E49FC File Offset: 0x000E2BFC
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

	// Token: 0x06002079 RID: 8313 RVA: 0x000E4BFC File Offset: 0x000E2DFC
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
