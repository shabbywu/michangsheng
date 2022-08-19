using System;
using UnityEngine;

// Token: 0x020004FF RID: 1279
public class TutorialEvents : MonoBehaviour
{
	// Token: 0x06002963 RID: 10595 RVA: 0x0013CC58 File Offset: 0x0013AE58
	private void Start()
	{
		if (base.gameObject.name.Contains("1"))
		{
			base.transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial1 Popup/Tutorial Text").GetComponent<TextMesh>().text = LanguageManager.TutorialTapJump;
			base.transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial1 Popup/Tutorial Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, false);
		}
		else if (base.gameObject.name.Contains("2"))
		{
			base.transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial2 Popup/Tutorial Text").GetComponent<TextMesh>().text = LanguageManager.TutorialGlide;
			base.transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial2 Popup/Tutorial Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, false);
		}
		else if (base.gameObject.name.Contains("3"))
		{
			base.transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial3 Popup/Tutorial Text").GetComponent<TextMesh>().text = LanguageManager.TutorialSwipe;
			base.transform.GetChild(0).GetChild(0).Find("AnimationHolder/Tutorial3 Popup/Tutorial Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, false);
		}
		base.transform.GetChild(0).gameObject.SetActive(false);
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x0013CDC4 File Offset: 0x0013AFC4
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Monkey")
		{
			TutorialEvents.postavljenCollider = false;
			Manage.pauseEnabled = false;
			if (base.gameObject.name.Contains("1"))
			{
				GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().KontrolisaniSkok = true;
			}
			else if (base.gameObject.name.Contains("2"))
			{
				GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().Glide = true;
			}
			else if (base.gameObject.name.Contains("3"))
			{
				GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().SlideNaDole = true;
			}
			Time.timeScale = 0f;
			base.GetComponent<Collider2D>().enabled = false;
			base.transform.position = Camera.main.transform.position + Vector3.forward * 10f;
			base.transform.GetChild(0).gameObject.SetActive(true);
			base.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("OpenPopup");
		}
	}

	// Token: 0x040025A2 RID: 9634
	public static bool postavljenCollider;

	// Token: 0x040025A3 RID: 9635
	private bool helpBool;

	// Token: 0x040025A4 RID: 9636
	private DateTime timeToShowNextElement;
}
