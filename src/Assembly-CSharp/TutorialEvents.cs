using System;
using UnityEngine;

// Token: 0x02000790 RID: 1936
public class TutorialEvents : MonoBehaviour
{
	// Token: 0x06003168 RID: 12648 RVA: 0x0018A138 File Offset: 0x00188338
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

	// Token: 0x06003169 RID: 12649 RVA: 0x0018A2A4 File Offset: 0x001884A4
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

	// Token: 0x04002D8A RID: 11658
	public static bool postavljenCollider;

	// Token: 0x04002D8B RID: 11659
	private bool helpBool;

	// Token: 0x04002D8C RID: 11660
	private DateTime timeToShowNextElement;
}
