using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x0200074F RID: 1871
public class ScenaReklama : MonoBehaviour
{
	// Token: 0x06002F9E RID: 12190 RVA: 0x000234B4 File Offset: 0x000216B4
	private void Start()
	{
		this.invalidMail = GameObject.Find("IncorrectMail");
		this.invalidMail.SetActive(false);
	}

	// Token: 0x06002F9F RID: 12191 RVA: 0x0017D224 File Offset: 0x0017B424
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			GameObject.Find(this.releasedItem).GetComponent<Collider>().enabled = false;
			GC.Collect();
			Resources.UnloadUnusedAssets();
			Application.LoadLevelAsync(StagesParser.currSetIndex * 10 + StagesParser.currStageIndex + 5);
		}
		else if (Input.GetMouseButtonDown(0))
		{
			this.clickedItem = this.RaycastFunction(Input.mousePosition);
			if (this.clickedItem.Equals("Button_Continue") || this.clickedItem.Equals("Button_Subscribe"))
			{
				GameObject gameObject = GameObject.Find(this.clickedItem);
				this.originalScale = gameObject.transform.localScale;
				gameObject.transform.localScale = this.originalScale * 0.8f;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
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

	// Token: 0x06002FA0 RID: 12192 RVA: 0x001499E4 File Offset: 0x00147BE4
	private byte[] GetBytes(string str)
	{
		byte[] array = new byte[str.Length * 2];
		Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
		return array;
	}

	// Token: 0x06002FA1 RID: 12193 RVA: 0x000234D2 File Offset: 0x000216D2
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

	// Token: 0x06002FA2 RID: 12194 RVA: 0x00149A14 File Offset: 0x00147C14
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x04002AD7 RID: 10967
	private Vector3 originalScale;

	// Token: 0x04002AD8 RID: 10968
	private string clickedItem;

	// Token: 0x04002AD9 RID: 10969
	private string releasedItem;

	// Token: 0x04002ADA RID: 10970
	private TouchScreenKeyboard keyboard;

	// Token: 0x04002ADB RID: 10971
	private string mail;

	// Token: 0x04002ADC RID: 10972
	private GameObject invalidMail;
}
