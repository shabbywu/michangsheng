using System;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
	public static bool postavljenCollider;

	private bool helpBool;

	private DateTime timeToShowNextElement;

	private void Start()
	{
		if (((Object)((Component)this).gameObject).name.Contains("1"))
		{
			((Component)((Component)this).transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial1 Popup/Tutorial Text")).GetComponent<TextMesh>().text = LanguageManager.TutorialTapJump;
			((Component)((Component)this).transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial1 Popup/Tutorial Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true, increaseFont: false);
		}
		else if (((Object)((Component)this).gameObject).name.Contains("2"))
		{
			((Component)((Component)this).transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial2 Popup/Tutorial Text")).GetComponent<TextMesh>().text = LanguageManager.TutorialGlide;
			((Component)((Component)this).transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial2 Popup/Tutorial Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true, increaseFont: false);
		}
		else if (((Object)((Component)this).gameObject).name.Contains("3"))
		{
			((Component)((Component)this).transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial3 Popup/Tutorial Text")).GetComponent<TextMesh>().text = LanguageManager.TutorialSwipe;
			((Component)((Component)this).transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial3 Popup/Tutorial Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true, increaseFont: false);
		}
		((Component)((Component)this).transform.GetChild(0)).gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)col).tag == "Monkey")
		{
			postavljenCollider = false;
			Manage.pauseEnabled = false;
			if (((Object)((Component)this).gameObject).name.Contains("1"))
			{
				GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().KontrolisaniSkok = true;
			}
			else if (((Object)((Component)this).gameObject).name.Contains("2"))
			{
				GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().Glide = true;
			}
			else if (((Object)((Component)this).gameObject).name.Contains("3"))
			{
				GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().SlideNaDole = true;
			}
			Time.timeScale = 0f;
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
			((Component)this).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 10f;
			((Component)((Component)this).transform.GetChild(0)).gameObject.SetActive(true);
			((Component)((Component)this).transform.GetChild(0).GetChild(0)).GetComponent<Animator>().Play("OpenPopup");
		}
	}
}
