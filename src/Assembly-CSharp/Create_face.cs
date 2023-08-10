using UnityEngine;
using UnityEngine.UI;

public class Create_face : MonoBehaviour
{
	public GameObject faceChoicePrefab;

	public GameObject faceItmePrefab;

	public GameObject colorPrefab;

	public GameObject AllItmePrefab;

	public AvatarFaceDatabase faceDatabase;

	public UIGrid AllListGrid;

	public UIGrid ItemGrid;

	public GameObject goodsGrid;

	public GameObject colorset;

	public GameObject colorsetScroll;

	public GameObject colorsetGrid;

	public Image NowSelectColor;

	public string nowColorstr = "";

	private void Start()
	{
		jsonData.instance.AvatarRandomJsonData = new JSONObject(JSONObject.Type.OBJECT);
		jsonData.instance.refreshMonstar(1);
		((Component)((Component)this).transform.parent.parent).GetComponent<CreateAvatarMag>().player.randomAvatar(1);
		resetList();
	}

	public JSONObject GetColorJson(string color_s)
	{
		JSONObject result = null;
		switch (color_s)
		{
		case "hairColorR":
			result = jsonData.instance.HairRandomColorJsonData;
			break;
		case "mouthColor":
			result = jsonData.instance.MouthRandomColorJsonData;
			break;
		case "tattooColor":
			result = jsonData.instance.WenShenRandomColorJsonData;
			break;
		case "blushColor":
			result = jsonData.instance.SaiHonRandomColorJsonData;
			break;
		case "yanzhuColor":
			result = jsonData.instance.YanZhuYanSeRandomColorJsonData;
			break;
		case "tezhengColor":
			result = jsonData.instance.MianWenYanSeRandomColorJsonData;
			break;
		case "eyebrowColor":
			result = jsonData.instance.MeiMaoYanSeRandomColorJsonData;
			break;
		case "featureColor":
			result = jsonData.instance.MeiMaoYanSeRandomColorJsonData;
			break;
		}
		return result;
	}

	public void ResteColorSetUIColor(string c)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (c == "")
		{
			((Component)NowSelectColor).gameObject.SetActive(false);
			return;
		}
		((Component)NowSelectColor).gameObject.SetActive(true);
		int num = jsonData.instance.AvatarRandomJsonData["1"][c].I + 1;
		JSONObject jSONObject = GetColorJson(c)[string.Concat(num)];
		float n = jSONObject["R"].n;
		float n2 = jSONObject["G"].n;
		float n3 = jSONObject["B"].n;
		((Graphic)NowSelectColor).color = new Color(n / 255f, n2 / 255f, n3 / 255f);
	}

	public void ShowColorSet()
	{
		colorset.SetActive(true);
		((Component)colorset.transform).GetComponent<UIToggle>().value = false;
		colorsetScroll.SetActive(false);
	}

	public void hideColorSet()
	{
		colorset.SetActive(false);
		colorsetScroll.SetActive(false);
	}

	public void resetList()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		foreach (Transform item in ((Component)AllListGrid).transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		int num = 0;
		foreach (faceInfoList all in faceDatabase.AllList)
		{
			GameObject val2 = Object.Instantiate<GameObject>(AllItmePrefab, ((Component)AllListGrid).transform);
			val2.transform.localPosition = Vector3.zero;
			val2.transform.localScale = Vector3.one;
			((Component)val2.transform.Find("Label")).GetComponent<UILabel>().text = all.Name;
			((Component)val2.transform.Find("LabelLight")).GetComponent<UILabel>().text = all.Name;
			if (num == 0)
			{
				((Component)val2.transform).GetComponent<UIToggle>().value = true;
				((Component)val2.transform).GetComponent<CreateFaceButton>().resteChoice();
			}
			num++;
		}
		((MonoBehaviour)this).Invoke("resetAllListGrid", 0.1f);
	}

	public void resetAllListGrid()
	{
		AllListGrid.repositionNow = true;
	}

	private void Update()
	{
		ResteColorSetUIColor(nowColorstr);
	}
}
