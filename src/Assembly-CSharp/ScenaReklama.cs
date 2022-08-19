using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x020004DA RID: 1242
public class ScenaReklama : MonoBehaviour
{
	// Token: 0x0600283B RID: 10299 RVA: 0x00130989 File Offset: 0x0012EB89
	private void Start()
	{
		this.invalidMail = GameObject.Find("IncorrectMail");
		this.invalidMail.SetActive(false);
	}

	// Token: 0x0600283C RID: 10300 RVA: 0x001309A8 File Offset: 0x0012EBA8
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

	// Token: 0x0600283D RID: 10301 RVA: 0x00130B50 File Offset: 0x0012ED50
	private byte[] GetBytes(string str)
	{
		byte[] array = new byte[str.Length * 2];
		Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
		return array;
	}

	// Token: 0x0600283E RID: 10302 RVA: 0x00130B7D File Offset: 0x0012ED7D
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

	// Token: 0x0600283F RID: 10303 RVA: 0x00130B8C File Offset: 0x0012ED8C
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x0400234D RID: 9037
	private Vector3 originalScale;

	// Token: 0x0400234E RID: 9038
	private string clickedItem;

	// Token: 0x0400234F RID: 9039
	private string releasedItem;

	// Token: 0x04002350 RID: 9040
	private TouchScreenKeyboard keyboard;

	// Token: 0x04002351 RID: 9041
	private string mail;

	// Token: 0x04002352 RID: 9042
	private GameObject invalidMail;
}
