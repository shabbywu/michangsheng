using UnityEngine;
using UnityEngine.UI;

public class updateCD : MonoBehaviour
{
	public float coolingTimer = 2f;

	private float currentTime;

	public Image coolingImage;

	public Text cooltext;

	private void Start()
	{
		currentTime = coolingTimer;
		coolingImage = ((Component)((Component)this).transform.Find("Image")).GetComponent<Image>();
		cooltext = ((Component)((Component)this).transform.Find("cooltime")).GetComponent<Text>();
	}

	private void Update()
	{
		if (currentTime < coolingTimer)
		{
			currentTime += Time.deltaTime;
			coolingImage.fillAmount = 1f - currentTime / coolingTimer;
			if ((double)(coolingTimer - currentTime) < 0.1)
			{
				cooltext.text = "";
			}
			else
			{
				cooltext.text = string.Concat((int)(coolingTimer - currentTime));
			}
		}
	}

	public void OnBtnClickSkill()
	{
		if (currentTime >= coolingTimer)
		{
			currentTime = 0f;
			coolingImage.fillAmount = 1f;
		}
	}
}
