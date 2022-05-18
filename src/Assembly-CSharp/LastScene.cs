using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x020006AB RID: 1707
public class LastScene : MonoBehaviour
{
	// Token: 0x06002AAC RID: 10924 RVA: 0x001496C0 File Offset: 0x001478C0
	private void Start()
	{
		base.StartCoroutine(Loading.Instance.UcitanaScena(Camera.main, 5, 0f));
		this.partikli = GameObject.Find("Partikli Finalna scena");
		this.partikli.SetActive(false);
		this.boardText = GameObject.Find("BoardText_Congratulations").GetComponent<TextMesh>();
		this.boardText_black = GameObject.Find("BoardText_NewLevelsComingSoon").GetComponent<TextMesh>();
		this.boardText.text = LanguageManager.Congratulations;
		this.boardText_black.text = LanguageManager.NewLevelsComingSoon;
		this.boardText.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.boardText_black.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.Invoke("UkljuciPartikle", 0.75f);
		if (PlaySounds.musicOn)
		{
			PlaySounds.Play_BackgroundMusic_Gameplay();
			if (PlaySounds.Level_Failed_Popup.isPlaying)
			{
				PlaySounds.Stop_Level_Failed_Popup();
			}
		}
	}

	// Token: 0x06002AAD RID: 10925 RVA: 0x00021196 File Offset: 0x0001F396
	private void UkljuciPartikle()
	{
		this.partikli.SetActive(true);
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x001497A4 File Offset: 0x001479A4
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			Application.LoadLevel(1);
		}
		else if (Input.GetMouseButtonDown(0))
		{
			this.clickedItem = this.RaycastFunction(Input.mousePosition);
			if (this.clickedItem.Equals("Button_Back") || this.clickedItem.Equals("Button_Subscribe"))
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
				this.temp.transform.localScale = this.originalScale * 0.8f;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			if (!this.clickedItem.Equals(string.Empty))
			{
				if (this.temp != null)
				{
					this.temp.transform.localScale = this.originalScale;
				}
				if (this.releasedItem.Equals("Button_Back"))
				{
					if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
					{
						PlaySounds.Stop_BackgroundMusic_Gameplay();
					}
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_OpenLevel();
					}
					Application.LoadLevel(1);
				}
				else if (this.clickedItem.Equals("Button_Subscribe"))
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_OpenLevel();
					}
					PlayerPrefs.SetInt("MailSent", 1);
					PlayerPrefs.Save();
					GameObject.Find("Button_Subscribe").SetActive(false);
				}
			}
		}
		if (this.keyboard != null && this.keyboard.done)
		{
			this.mail = this.keyboard.text;
			this.keyboard = null;
			if (!this.mail.Equals(string.Empty) && this.mail.Contains("@"))
			{
				Debug.Log("poruka: " + this.mail);
				if (this.invalidMail.activeSelf)
				{
					this.invalidMail.SetActive(false);
				}
				GameObject.Find("Button_Subscribe").SetActive(false);
				return;
			}
			Debug.Log("treba da je prazno: " + this.mail);
			this.invalidMail.SetActive(true);
		}
	}

	// Token: 0x06002AAF RID: 10927 RVA: 0x001499E4 File Offset: 0x00147BE4
	private byte[] GetBytes(string str)
	{
		byte[] array = new byte[str.Length * 2];
		Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
		return array;
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x000211A4 File Offset: 0x0001F3A4
	private IEnumerator postToServer()
	{
		yield return new WaitForEndOfFrame();
		WWWForm wwwform = new WWWForm();
		byte[] bytes = this.GetBytes(this.mail);
		File.WriteAllBytes(Application.persistentDataPath + "/mailList.txt", bytes);
		wwwform.AddBinaryData("NekiNaziv", bytes, "index.php");
		WWW www = new WWW("http://www.tapsong.net/content/MonkeyRush/", wwwform);
		yield return www;
		if (string.IsNullOrEmpty(www.error))
		{
			GameObject.Find("MAIL").GetComponent<TextMesh>().text = "SUCCESSFULLY UPLOAD:\n" + www.text;
		}
		else
		{
			GameObject.Find("MAIL").GetComponent<TextMesh>().text = "ERROR UPLOADING:\n" + www.error;
		}
		yield break;
	}

	// Token: 0x06002AB1 RID: 10929 RVA: 0x00149A14 File Offset: 0x00147C14
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x040024A7 RID: 9383
	private GameObject majmunLogo;

	// Token: 0x040024A8 RID: 9384
	private Vector3 originalScale;

	// Token: 0x040024A9 RID: 9385
	private string clickedItem;

	// Token: 0x040024AA RID: 9386
	private string releasedItem;

	// Token: 0x040024AB RID: 9387
	private TouchScreenKeyboard keyboard;

	// Token: 0x040024AC RID: 9388
	private string mail;

	// Token: 0x040024AD RID: 9389
	private GameObject invalidMail;

	// Token: 0x040024AE RID: 9390
	private TextMesh boardText;

	// Token: 0x040024AF RID: 9391
	private TextMesh boardText_black;

	// Token: 0x040024B0 RID: 9392
	private GameObject partikli;

	// Token: 0x040024B1 RID: 9393
	private GameObject temp;
}
