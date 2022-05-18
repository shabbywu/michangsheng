using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200067C RID: 1660
public class CheckInternetConnection : MonoBehaviour
{
	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06002981 RID: 10625 RVA: 0x000203DE File Offset: 0x0001E5DE
	public static CheckInternetConnection Instance
	{
		get
		{
			if (CheckInternetConnection.instance == null)
			{
				CheckInternetConnection.instance = (Object.FindObjectOfType(typeof(CheckInternetConnection)) as CheckInternetConnection);
			}
			return CheckInternetConnection.instance;
		}
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x001429BC File Offset: 0x00140BBC
	private void Awake()
	{
		CheckInternetConnection.instance = this;
		this.noInternet = base.transform.Find("AnimationHolderGlavni/AnimationHolder/NoInternetPopup/Text/NoInternet").GetComponent<TextMesh>();
		this.checkConnection = base.transform.Find("AnimationHolderGlavni/AnimationHolder/NoInternetPopup/Text/CheckConnection").GetComponent<TextMesh>();
		this.checkOK = base.transform.Find("AnimationHolderGlavni/AnimationHolder/NoInternetPopup/Button_CheckOK/Text").GetComponent<TextMesh>();
		this.loading = base.transform.Find("AnimationHolderGlavni/Loading Buffer HOLDER/Loading").GetComponent<Animator>();
		this.pomCollider = base.transform.Find("AnimationHolderGlavni/PomocniColliderKodProveravanjaKonekcije");
		this.loadingHolder = base.transform.Find("AnimationHolderGlavni/Loading Buffer HOLDER");
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x0002040B File Offset: 0x0001E60B
	private void Start()
	{
		this.refreshText();
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x00020413 File Offset: 0x0001E613
	public IEnumerator checkInternetConnectionAndOpenPopup()
	{
		WWW www = new WWW(this.url);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			Debug.Log("internet error: " + www.error);
			this.internetOK = false;
			this.checkDone = true;
			this.loadingHolder.gameObject.SetActive(false);
			if (!this.otvorenPopup)
			{
				base.transform.GetChild(0).GetComponent<Animator>().Play("OpenPopup");
				this.otvorenPopup = true;
			}
			yield return new WaitForSeconds(0.25f);
			this.pomCollider.gameObject.SetActive(false);
		}
		else
		{
			Debug.Log("Sve je OK");
			this.internetOK = true;
			this.checkDone = true;
			this.loadingHolder.gameObject.SetActive(false);
			if (this.otvorenPopup)
			{
				base.transform.GetChild(0).GetComponent<Animator>().Play("ClosePopup");
				this.otvorenPopup = false;
			}
			yield return new WaitForSeconds(0.25f);
			this.pomCollider.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x00020422 File Offset: 0x0001E622
	public IEnumerator checkInternetConnection()
	{
		WWW www = new WWW(this.url);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			this.internetOK = false;
			this.checkDone = true;
		}
		else
		{
			this.internetOK = true;
			this.checkDone = true;
		}
		yield break;
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x00020431 File Offset: 0x0001E631
	public void openPopup()
	{
		if (!this.otvorenPopup)
		{
			this.refreshText();
			base.transform.GetChild(0).GetComponent<Animator>().Play("OpenPopup");
			this.otvorenPopup = true;
		}
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x00142A68 File Offset: 0x00140C68
	public void NoVideosAvailable_OpenPopup()
	{
		if (!this.otvorenPopup)
		{
			this.checkConnection.text = LanguageManager.NoVideo;
			this.noInternet.text = string.Empty;
			this.checkConnection.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			this.noInternet.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			base.transform.GetChild(0).GetComponent<Animator>().Play("OpenPopup");
			this.otvorenPopup = true;
		}
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x00020463 File Offset: 0x0001E663
	private void OnApplicationPause(bool pauseStatus)
	{
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x00142AE8 File Offset: 0x00140CE8
	public void refreshText()
	{
		this.noInternet.text = LanguageManager.NoInternet;
		this.checkConnection.text = LanguageManager.CheckInternet;
		this.checkOK.text = LanguageManager.Ok;
		this.noInternet.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.checkConnection.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.checkOK.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
	}

	// Token: 0x0600298A RID: 10634 RVA: 0x00020467 File Offset: 0x0001E667
	public void closePopupAndCheck()
	{
		base.StartCoroutine(this.checkInternetConnection());
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x00020476 File Offset: 0x0001E676
	public IEnumerator ClosePopup()
	{
		this.loadingHolder.gameObject.SetActive(false);
		if (this.otvorenPopup)
		{
			base.transform.GetChild(0).GetComponent<Animator>().Play("ClosePopup");
			this.otvorenPopup = false;
		}
		yield return new WaitForSeconds(0.25f);
		this.pomCollider.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0400232F RID: 9007
	private static CheckInternetConnection instance;

	// Token: 0x04002330 RID: 9008
	private TextMesh noInternet;

	// Token: 0x04002331 RID: 9009
	private TextMesh checkConnection;

	// Token: 0x04002332 RID: 9010
	private TextMesh checkOK;

	// Token: 0x04002333 RID: 9011
	private Animator loading;

	// Token: 0x04002334 RID: 9012
	private Transform loadingHolder;

	// Token: 0x04002335 RID: 9013
	private Transform pomCollider;

	// Token: 0x04002336 RID: 9014
	private bool otvorenPopup;

	// Token: 0x04002337 RID: 9015
	private string url = "https://www.google.com";

	// Token: 0x04002338 RID: 9016
	[HideInInspector]
	public bool internetOK = true;

	// Token: 0x04002339 RID: 9017
	[HideInInspector]
	public bool checkDone;
}
