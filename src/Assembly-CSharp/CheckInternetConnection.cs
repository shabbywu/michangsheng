using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004A0 RID: 1184
public class CheckInternetConnection : MonoBehaviour
{
	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x0600255D RID: 9565 RVA: 0x00102F71 File Offset: 0x00101171
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

	// Token: 0x0600255E RID: 9566 RVA: 0x00102FA0 File Offset: 0x001011A0
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

	// Token: 0x0600255F RID: 9567 RVA: 0x0010304B File Offset: 0x0010124B
	private void Start()
	{
		this.refreshText();
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x00103053 File Offset: 0x00101253
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

	// Token: 0x06002561 RID: 9569 RVA: 0x00103062 File Offset: 0x00101262
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

	// Token: 0x06002562 RID: 9570 RVA: 0x00103071 File Offset: 0x00101271
	public void openPopup()
	{
		if (!this.otvorenPopup)
		{
			this.refreshText();
			base.transform.GetChild(0).GetComponent<Animator>().Play("OpenPopup");
			this.otvorenPopup = true;
		}
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x001030A4 File Offset: 0x001012A4
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

	// Token: 0x06002564 RID: 9572 RVA: 0x00103121 File Offset: 0x00101321
	private void OnApplicationPause(bool pauseStatus)
	{
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x00103128 File Offset: 0x00101328
	public void refreshText()
	{
		this.noInternet.text = LanguageManager.NoInternet;
		this.checkConnection.text = LanguageManager.CheckInternet;
		this.checkOK.text = LanguageManager.Ok;
		this.noInternet.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.checkConnection.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.checkOK.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x0010319E File Offset: 0x0010139E
	public void closePopupAndCheck()
	{
		base.StartCoroutine(this.checkInternetConnection());
	}

	// Token: 0x06002567 RID: 9575 RVA: 0x001031AD File Offset: 0x001013AD
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

	// Token: 0x04001E18 RID: 7704
	private static CheckInternetConnection instance;

	// Token: 0x04001E19 RID: 7705
	private TextMesh noInternet;

	// Token: 0x04001E1A RID: 7706
	private TextMesh checkConnection;

	// Token: 0x04001E1B RID: 7707
	private TextMesh checkOK;

	// Token: 0x04001E1C RID: 7708
	private Animator loading;

	// Token: 0x04001E1D RID: 7709
	private Transform loadingHolder;

	// Token: 0x04001E1E RID: 7710
	private Transform pomCollider;

	// Token: 0x04001E1F RID: 7711
	private bool otvorenPopup;

	// Token: 0x04001E20 RID: 7712
	private string url = "https://www.google.com";

	// Token: 0x04001E21 RID: 7713
	[HideInInspector]
	public bool internetOK = true;

	// Token: 0x04001E22 RID: 7714
	[HideInInspector]
	public bool checkDone;
}
