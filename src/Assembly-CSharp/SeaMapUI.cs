using UnityEngine;
using UnityEngine.UI;

public class SeaMapUI : MonoBehaviour
{
	public Image image;

	private void Start()
	{
	}

	public void CloseSeaMapUI()
	{
		((Component)this).gameObject.SetActive(false);
		Tools.canClickFlag = true;
	}

	private void Update()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			RectTransform rectTransform = ((Graphic)image).rectTransform;
			rectTransform.sizeDelta *= 1.03f;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			RectTransform rectTransform2 = ((Graphic)image).rectTransform;
			rectTransform2.sizeDelta /= 1.03f;
		}
		if (((Graphic)image).rectTransform.sizeDelta.x < 1920f)
		{
			((Graphic)image).rectTransform.sizeDelta = new Vector2(1920f, 1080f);
		}
		else if (((Graphic)image).rectTransform.sizeDelta.x > 3840f)
		{
			((Graphic)image).rectTransform.sizeDelta = new Vector2(3840f, 2160f);
		}
	}
}
