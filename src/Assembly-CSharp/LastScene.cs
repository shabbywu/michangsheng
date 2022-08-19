using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x020004B6 RID: 1206
public class LastScene : MonoBehaviour
{
	// Token: 0x06002628 RID: 9768 RVA: 0x00108EA4 File Offset: 0x001070A4
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

	// Token: 0x06002629 RID: 9769 RVA: 0x00108F86 File Offset: 0x00107186
	private void UkljuciPartikle()
	{
		this.partikli.SetActive(true);
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x00108F94 File Offset: 0x00107194
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

	// Token: 0x0600262B RID: 9771 RVA: 0x001091D4 File Offset: 0x001073D4
	private byte[] GetBytes(string str)
	{
		byte[] array = new byte[str.Length * 2];
		Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
		return array;
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x00109201 File Offset: 0x00107401
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

	// Token: 0x0600262D RID: 9773 RVA: 0x00109210 File Offset: 0x00107410
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x04001F2A RID: 7978
	private GameObject majmunLogo;

	// Token: 0x04001F2B RID: 7979
	private Vector3 originalScale;

	// Token: 0x04001F2C RID: 7980
	private string clickedItem;

	// Token: 0x04001F2D RID: 7981
	private string releasedItem;

	// Token: 0x04001F2E RID: 7982
	private TouchScreenKeyboard keyboard;

	// Token: 0x04001F2F RID: 7983
	private string mail;

	// Token: 0x04001F30 RID: 7984
	private GameObject invalidMail;

	// Token: 0x04001F31 RID: 7985
	private TextMesh boardText;

	// Token: 0x04001F32 RID: 7986
	private TextMesh boardText_black;

	// Token: 0x04001F33 RID: 7987
	private GameObject partikli;

	// Token: 0x04001F34 RID: 7988
	private GameObject temp;
}
