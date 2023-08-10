using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class ScenaReklama : MonoBehaviour
{
	private Vector3 originalScale;

	private string clickedItem;

	private string releasedItem;

	private TouchScreenKeyboard keyboard;

	private string mail;

	private GameObject invalidMail;

	private void Start()
	{
		invalidMail = GameObject.Find("IncorrectMail");
		invalidMail.SetActive(false);
	}

	private void Update()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyUp((KeyCode)27))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			GameObject.Find(releasedItem).GetComponent<Collider>().enabled = false;
			GC.Collect();
			Resources.UnloadUnusedAssets();
			Application.LoadLevelAsync(StagesParser.currSetIndex * 10 + StagesParser.currStageIndex + 5);
		}
		else if (Input.GetMouseButtonDown(0))
		{
			clickedItem = RaycastFunction(Input.mousePosition);
			if (clickedItem.Equals("Button_Continue") || clickedItem.Equals("Button_Subscribe"))
			{
				GameObject val = GameObject.Find(clickedItem);
				originalScale = val.transform.localScale;
				val.transform.localScale = originalScale * 0.8f;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			releasedItem = RaycastFunction(Input.mousePosition);
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
