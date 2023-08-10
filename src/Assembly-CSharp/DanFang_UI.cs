using UnityEngine;
using UnityEngine.UI;

public class DanFang_UI : MonoBehaviour
{
	public int ItemID = -1;

	public GameObject content;

	public Toggle toggle;

	public LianDanDanFang lianDanDanFang;

	public Text text;

	public float ASize = 60f;

	private bool isShow;

	private float IsShowPastTime;

	private float ShowHait;

	public bool ChildWeigh;

	private void Start()
	{
	}

	public void showContent()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		content.SetActive(true);
		if (ChildWeigh)
		{
			float num = ASize;
			foreach (Transform item in content.transform)
			{
				Transform val = item;
				if (((Component)val).gameObject.activeSelf)
				{
					num += ((Component)val).GetComponent<RectTransform>().sizeDelta.y + 10f;
				}
			}
			float num2 = 0.2f;
			if (ShowHait > 0.05f)
			{
				ShowHait = 0f;
				num2 = 0f;
			}
			((Component)this).GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 20f + num + num2);
			ShowHait += Time.deltaTime;
		}
		else
		{
			((Component)this).GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 10f + (float)content.transform.childCount * ASize);
		}
	}

	public void closeContent()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		content.SetActive(false);
		((Component)this).GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 76.5f);
	}

	private void Update()
	{
		if (toggle.isOn)
		{
			showContent();
		}
		else
		{
			closeContent();
		}
	}
}
