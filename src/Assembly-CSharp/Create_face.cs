using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003E8 RID: 1000
public class Create_face : MonoBehaviour
{
	// Token: 0x0600203E RID: 8254 RVA: 0x000E3010 File Offset: 0x000E1210
	private void Start()
	{
		jsonData.instance.AvatarRandomJsonData = new JSONObject(JSONObject.Type.OBJECT);
		jsonData.instance.refreshMonstar(1);
		base.transform.parent.parent.GetComponent<CreateAvatarMag>().player.randomAvatar(1);
		this.resetList();
	}

	// Token: 0x0600203F RID: 8255 RVA: 0x000E3060 File Offset: 0x000E1260
	public JSONObject GetColorJson(string color_s)
	{
		JSONObject result = null;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(color_s);
		if (num <= 1128880211U)
		{
			if (num <= 527219179U)
			{
				if (num != 153553283U)
				{
					if (num == 527219179U)
					{
						if (color_s == "yanzhuColor")
						{
							result = jsonData.instance.YanZhuYanSeRandomColorJsonData;
						}
					}
				}
				else if (color_s == "tezhengColor")
				{
					result = jsonData.instance.MianWenYanSeRandomColorJsonData;
				}
			}
			else if (num != 596278282U)
			{
				if (num == 1128880211U)
				{
					if (color_s == "tattooColor")
					{
						result = jsonData.instance.WenShenRandomColorJsonData;
					}
				}
			}
			else if (color_s == "hairColorR")
			{
				result = jsonData.instance.HairRandomColorJsonData;
			}
		}
		else if (num <= 2189298307U)
		{
			if (num != 1180500499U)
			{
				if (num == 2189298307U)
				{
					if (color_s == "mouthColor")
					{
						result = jsonData.instance.MouthRandomColorJsonData;
					}
				}
			}
			else if (color_s == "eyebrowColor")
			{
				result = jsonData.instance.MeiMaoYanSeRandomColorJsonData;
			}
		}
		else if (num != 2546894512U)
		{
			if (num == 4294355740U)
			{
				if (color_s == "blushColor")
				{
					result = jsonData.instance.SaiHonRandomColorJsonData;
				}
			}
		}
		else if (color_s == "featureColor")
		{
			result = jsonData.instance.MeiMaoYanSeRandomColorJsonData;
		}
		return result;
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x000E31E0 File Offset: 0x000E13E0
	public void ResteColorSetUIColor(string c)
	{
		if (c == "")
		{
			this.NowSelectColor.gameObject.SetActive(false);
			return;
		}
		this.NowSelectColor.gameObject.SetActive(true);
		int num = jsonData.instance.AvatarRandomJsonData["1"][c].I + 1;
		JSONObject jsonobject = this.GetColorJson(c)[string.Concat(num)];
		float n = jsonobject["R"].n;
		float n2 = jsonobject["G"].n;
		float n3 = jsonobject["B"].n;
		this.NowSelectColor.color = new Color(n / 255f, n2 / 255f, n3 / 255f);
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x000E32AD File Offset: 0x000E14AD
	public void ShowColorSet()
	{
		this.colorset.SetActive(true);
		this.colorset.transform.GetComponent<UIToggle>().value = false;
		this.colorsetScroll.SetActive(false);
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x000E32DD File Offset: 0x000E14DD
	public void hideColorSet()
	{
		this.colorset.SetActive(false);
		this.colorsetScroll.SetActive(false);
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x000E32F8 File Offset: 0x000E14F8
	public void resetList()
	{
		foreach (object obj in this.AllListGrid.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		int num = 0;
		foreach (faceInfoList faceInfoList in this.faceDatabase.AllList)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.AllItmePrefab, this.AllListGrid.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.Find("Label").GetComponent<UILabel>().text = faceInfoList.Name;
			gameObject.transform.Find("LabelLight").GetComponent<UILabel>().text = faceInfoList.Name;
			if (num == 0)
			{
				gameObject.transform.GetComponent<UIToggle>().value = true;
				gameObject.transform.GetComponent<CreateFaceButton>().resteChoice();
			}
			num++;
		}
		base.Invoke("resetAllListGrid", 0.1f);
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x000E346C File Offset: 0x000E166C
	public void resetAllListGrid()
	{
		this.AllListGrid.repositionNow = true;
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x000E347A File Offset: 0x000E167A
	private void Update()
	{
		this.ResteColorSetUIColor(this.nowColorstr);
	}

	// Token: 0x04001A2E RID: 6702
	public GameObject faceChoicePrefab;

	// Token: 0x04001A2F RID: 6703
	public GameObject faceItmePrefab;

	// Token: 0x04001A30 RID: 6704
	public GameObject colorPrefab;

	// Token: 0x04001A31 RID: 6705
	public GameObject AllItmePrefab;

	// Token: 0x04001A32 RID: 6706
	public AvatarFaceDatabase faceDatabase;

	// Token: 0x04001A33 RID: 6707
	public UIGrid AllListGrid;

	// Token: 0x04001A34 RID: 6708
	public UIGrid ItemGrid;

	// Token: 0x04001A35 RID: 6709
	public GameObject goodsGrid;

	// Token: 0x04001A36 RID: 6710
	public GameObject colorset;

	// Token: 0x04001A37 RID: 6711
	public GameObject colorsetScroll;

	// Token: 0x04001A38 RID: 6712
	public GameObject colorsetGrid;

	// Token: 0x04001A39 RID: 6713
	public Image NowSelectColor;

	// Token: 0x04001A3A RID: 6714
	public string nowColorstr = "";
}
