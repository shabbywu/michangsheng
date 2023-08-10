using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class LastScene : MonoBehaviour
{
	private GameObject majmunLogo;

	private Vector3 originalScale;

	private string clickedItem;

	private string releasedItem;

	private TouchScreenKeyboard keyboard;

	private string mail;

	private GameObject invalidMail;

	private TextMesh boardText;

	private TextMesh boardText_black;

	private GameObject partikli;

	private GameObject temp;

	private void Start()
	{
		((MonoBehaviour)this).StartCoroutine(Loading.Instance.UcitanaScena(Camera.main, 5, 0f));
		partikli = GameObject.Find("Partikli Finalna scena");
		partikli.SetActive(false);
		boardText = GameObject.Find("BoardText_Congratulations").GetComponent<TextMesh>();
		boardText_black = GameObject.Find("BoardText_NewLevelsComingSoon").GetComponent<TextMesh>();
		boardText.text = LanguageManager.Congratulations;
		boardText_black.text = LanguageManager.NewLevelsComingSoon;
		((Component)boardText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)boardText_black).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((MonoBehaviour)this).Invoke("UkljuciPartikle", 0.75f);
		if (PlaySounds.musicOn)
		{
			PlaySounds.Play_BackgroundMusic_Gameplay();
			if (PlaySounds.Level_Failed_Popup.isPlaying)
			{
				PlaySounds.Stop_Level_Failed_Popup();
			}
		}
	}

	private void UkljuciPartikle()
	{
		partikli.SetActive(true);
	}

	private void Update()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyUp((KeyCode)27))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			Application.LoadLevel(1);
		}
		else if (Input.GetMouseButtonDown(0))
		{
			clickedItem = RaycastFunction(Input.mousePosition);
			if (clickedItem.Equals("Button_Back") || clickedItem.Equals("Button_Subscribe"))
			{
				temp = GameObject.Find(clickedItem);
				originalScale = temp.transform.localScale;
				temp.transform.localScale = originalScale * 0.8f;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			releasedItem = RaycastFunction(Input.mousePosition);
			if (!clickedItem.Equals(string.Empty))
			{
				if ((Object)(object)temp != (Object)null)
				{
					temp.transform.localScale = originalScale;
				}
				if (releasedItem.Equals("Button_Back"))
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
				else if (clickedItem.Equals("Button_Subscribe"))
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
		if (keyboard == null || !keyboard.done)
		{
			return;
		}
		mail = keyboard.text;
		keyboard = null;
		if (!mail.Equals(string.Empty) && mail.Contains("@"))
		{
			Debug.Log((object)("poruka: " + mail));
			if (invalidMail.activeSelf)
			{
				invalidMail.SetActive(false);
			}
			GameObject.Find("Button_Subscribe").SetActive(false);
		}
		else
		{
			Debug.Log((object)("treba da je prazno: " + mail));
			invalidMail.SetActive(true);
		}
	}

	private byte[] GetBytes(string str)
	{
		byte[] array = new byte[str.Length * 2];
		Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
		return array;
	}

	private IEnumerator postToServer()
	{
		yield return (object)new WaitForEndOfFrame();
		WWWForm val = new WWWForm();
		byte[] bytes = GetBytes(mail);
		File.WriteAllBytes(Application.persistentDataPath + "/mailList.txt", bytes);
		val.AddBinaryData("NekiNaziv", bytes, "index.php");
		WWW www = new WWW("http://www.tapsong.net/content/MonkeyRush/", val);
		yield return www;
		if (string.IsNullOrEmpty(www.error))
		{
			GameObject.Find("MAIL").GetComponent<TextMesh>().text = "SUCCESSFULLY UPLOAD:\n" + www.text;
		}
		else
		{
			GameObject.Find("MAIL").GetComponent<TextMesh>().text = "ERROR UPLOADING:\n" + www.error;
		}
	}

	private string RaycastFunction(Vector3 vector)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		return "";
	}
}
