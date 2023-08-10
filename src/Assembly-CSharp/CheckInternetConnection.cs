using System.Collections;
using UnityEngine;

public class CheckInternetConnection : MonoBehaviour
{
	private static CheckInternetConnection instance;

	private TextMesh noInternet;

	private TextMesh checkConnection;

	private TextMesh checkOK;

	private Animator loading;

	private Transform loadingHolder;

	private Transform pomCollider;

	private bool otvorenPopup;

	private string url = "https://www.google.com";

	[HideInInspector]
	public bool internetOK = true;

	[HideInInspector]
	public bool checkDone;

	public static CheckInternetConnection Instance
	{
		get
		{
			if ((Object)(object)instance == (Object)null)
			{
				instance = Object.FindObjectOfType(typeof(CheckInternetConnection)) as CheckInternetConnection;
			}
			return instance;
		}
	}

	private void Awake()
	{
		instance = this;
		noInternet = ((Component)((Component)this).transform.Find("AnimationHolderGlavni/AnimationHolder/NoInternetPopup/Text/NoInternet")).GetComponent<TextMesh>();
		checkConnection = ((Component)((Component)this).transform.Find("AnimationHolderGlavni/AnimationHolder/NoInternetPopup/Text/CheckConnection")).GetComponent<TextMesh>();
		checkOK = ((Component)((Component)this).transform.Find("AnimationHolderGlavni/AnimationHolder/NoInternetPopup/Button_CheckOK/Text")).GetComponent<TextMesh>();
		loading = ((Component)((Component)this).transform.Find("AnimationHolderGlavni/Loading Buffer HOLDER/Loading")).GetComponent<Animator>();
		pomCollider = ((Component)this).transform.Find("AnimationHolderGlavni/PomocniColliderKodProveravanjaKonekcije");
		loadingHolder = ((Component)this).transform.Find("AnimationHolderGlavni/Loading Buffer HOLDER");
	}

	private void Start()
	{
		refreshText();
	}

	public IEnumerator checkInternetConnectionAndOpenPopup()
	{
		WWW www = new WWW(url);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			Debug.Log((object)("internet error: " + www.error));
			internetOK = false;
			checkDone = true;
			((Component)loadingHolder).gameObject.SetActive(false);
			if (!otvorenPopup)
			{
				((Component)((Component)this).transform.GetChild(0)).GetComponent<Animator>().Play("OpenPopup");
				otvorenPopup = true;
			}
			yield return (object)new WaitForSeconds(0.25f);
			((Component)pomCollider).gameObject.SetActive(false);
		}
		else
		{
			Debug.Log((object)"Sve je OK");
			internetOK = true;
			checkDone = true;
			((Component)loadingHolder).gameObject.SetActive(false);
			if (otvorenPopup)
			{
				((Component)((Component)this).transform.GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
				otvorenPopup = false;
			}
			yield return (object)new WaitForSeconds(0.25f);
			((Component)pomCollider).gameObject.SetActive(false);
		}
	}

	public IEnumerator checkInternetConnection()
	{
		WWW www = new WWW(url);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			internetOK = false;
			checkDone = true;
		}
		else
		{
			internetOK = true;
			checkDone = true;
		}
	}

	public void openPopup()
	{
		if (!otvorenPopup)
		{
			refreshText();
			((Component)((Component)this).transform.GetChild(0)).GetComponent<Animator>().Play("OpenPopup");
			otvorenPopup = true;
		}
	}

	public void NoVideosAvailable_OpenPopup()
	{
		if (!otvorenPopup)
		{
			checkConnection.text = LanguageManager.NoVideo;
			noInternet.text = string.Empty;
			((Component)checkConnection).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			((Component)noInternet).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			((Component)((Component)this).transform.GetChild(0)).GetComponent<Animator>().Play("OpenPopup");
			otvorenPopup = true;
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
	}

	public void refreshText()
	{
		noInternet.text = LanguageManager.NoInternet;
		checkConnection.text = LanguageManager.CheckInternet;
		checkOK.text = LanguageManager.Ok;
		((Component)noInternet).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)checkConnection).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)checkOK).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
	}

	public void closePopupAndCheck()
	{
		((MonoBehaviour)this).StartCoroutine(checkInternetConnection());
	}

	public IEnumerator ClosePopup()
	{
		((Component)loadingHolder).gameObject.SetActive(false);
		if (otvorenPopup)
		{
			((Component)((Component)this).transform.GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
			otvorenPopup = false;
		}
		yield return (object)new WaitForSeconds(0.25f);
		((Component)pomCollider).gameObject.SetActive(false);
	}
}
