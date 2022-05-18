using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000584 RID: 1412
public class Create_face : MonoBehaviour
{
	// Token: 0x060023C3 RID: 9155 RVA: 0x001255B8 File Offset: 0x001237B8
	private void Start()
	{
		jsonData.instance.AvatarRandomJsonData = new JSONObject(JSONObject.Type.OBJECT);
		jsonData.instance.refreshMonstar(1);
		base.transform.parent.parent.GetComponent<CreateAvatarMag>().player.randomAvatar(1);
		this.resetList();
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x0010DA30 File Offset: 0x0010BC30
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

	// Token: 0x060023C5 RID: 9157 RVA: 0x00125608 File Offset: 0x00123808
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

	// Token: 0x060023C6 RID: 9158 RVA: 0x0001CD87 File Offset: 0x0001AF87
	public void ShowColorSet()
	{
		this.colorset.SetActive(true);
		this.colorset.transform.GetComponent<UIToggle>().value = false;
		this.colorsetScroll.SetActive(false);
	}

	// Token: 0x060023C7 RID: 9159 RVA: 0x0001CDB7 File Offset: 0x0001AFB7
	public void hideColorSet()
	{
		this.colorset.SetActive(false);
		this.colorsetScroll.SetActive(false);
	}

	// Token: 0x060023C8 RID: 9160 RVA: 0x001256D8 File Offset: 0x001238D8
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

	// Token: 0x060023C9 RID: 9161 RVA: 0x0001CDD1 File Offset: 0x0001AFD1
	public void resetAllListGrid()
	{
		this.AllListGrid.repositionNow = true;
	}

	// Token: 0x060023CA RID: 9162 RVA: 0x0001CDDF File Offset: 0x0001AFDF
	private void Update()
	{
		this.ResteColorSetUIColor(this.nowColorstr);
	}

	// Token: 0x04001EC3 RID: 7875
	public GameObject faceChoicePrefab;

	// Token: 0x04001EC4 RID: 7876
	public GameObject faceItmePrefab;

	// Token: 0x04001EC5 RID: 7877
	public GameObject colorPrefab;

	// Token: 0x04001EC6 RID: 7878
	public GameObject AllItmePrefab;

	// Token: 0x04001EC7 RID: 7879
	public AvatarFaceDatabase faceDatabase;

	// Token: 0x04001EC8 RID: 7880
	public UIGrid AllListGrid;

	// Token: 0x04001EC9 RID: 7881
	public UIGrid ItemGrid;

	// Token: 0x04001ECA RID: 7882
	public GameObject goodsGrid;

	// Token: 0x04001ECB RID: 7883
	public GameObject colorset;

	// Token: 0x04001ECC RID: 7884
	public GameObject colorsetScroll;

	// Token: 0x04001ECD RID: 7885
	public GameObject colorsetGrid;

	// Token: 0x04001ECE RID: 7886
	public Image NowSelectColor;

	// Token: 0x04001ECF RID: 7887
	public string nowColorstr = "";
}
