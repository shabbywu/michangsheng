using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004BA RID: 1210
public class Loading : MonoBehaviour
{
	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06002648 RID: 9800 RVA: 0x0010A009 File Offset: 0x00108209
	public static Loading Instance
	{
		get
		{
			if (Loading.instance == null)
			{
				Loading.instance = (Object.FindObjectOfType(typeof(Loading)) as Loading);
			}
			return Loading.instance;
		}
	}

	// Token: 0x06002649 RID: 9801 RVA: 0x0010A036 File Offset: 0x00108236
	private void Awake()
	{
		Loading.instance = this;
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x0010A040 File Offset: 0x00108240
	private void Start()
	{
		base.gameObject.name = "LOADING ZA BRISANJE";
		Object.DontDestroyOnLoad(base.gameObject);
		if (StagesParser.LoadingPoruke.Count == 0)
		{
			StagesParser.LoadingPoruke.Clear();
			StagesParser.RedniBrojSlike.Clear();
			StagesParser.Instance.UcitajLoadingPoruke();
		}
		base.transform.Find("Loading Animation Tip-s/Loading sa Tip-om/Loading Text").GetComponent<TextMesh>().text = LanguageManager.Loading;
		base.transform.Find("Loading Animation Tip-s/Loading sa Tip-om/Loading Text").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this.RandomBroj = Random.Range(1, StagesParser.LoadingPoruke.Count);
		this.PozadinaText = GameObject.Find("Tip Text").GetComponent<TextMesh>();
		if (StagesParser.loadingTip == 1)
		{
			this.PozadinaText.text = LanguageManager.LoadingTip1;
			this.PozadinaText.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, false);
			(Object.Instantiate(Resources.Load("LoadingBackground/BgP8")) as GameObject).transform.parent = GameObject.Find("Loading Tip BG").transform;
			this.RandomBroj = 8;
		}
		else if (StagesParser.loadingTip == 2)
		{
			this.PozadinaText.text = LanguageManager.LoadingTip2;
			this.PozadinaText.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, false);
			(Object.Instantiate(Resources.Load("LoadingBackground/BgP5")) as GameObject).transform.parent = GameObject.Find("Loading Tip BG").transform;
			this.RandomBroj = 5;
		}
		else if (StagesParser.loadingTip == 3)
		{
			this.PozadinaText.text = LanguageManager.LoadingTip3;
			this.PozadinaText.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, false);
			(Object.Instantiate(Resources.Load("LoadingBackground/BgP3")) as GameObject).transform.parent = GameObject.Find("Loading Tip BG").transform;
			this.RandomBroj = 3;
		}
		else
		{
			this.PozadinaText.text = StagesParser.LoadingPoruke[this.RandomBroj - 1];
			this.PozadinaText.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, false);
			StagesParser.LoadingPoruke.RemoveAt(this.RandomBroj - 1);
			GameObject gameObject = Object.Instantiate(Resources.Load("LoadingBackground/BgP" + StagesParser.RedniBrojSlike[this.RandomBroj - 1].ToString())) as GameObject;
			StagesParser.RedniBrojSlike.RemoveAt(this.RandomBroj - 1);
			gameObject.transform.parent = GameObject.Find("Loading Tip BG").transform;
		}
		if (PlaySounds.BackgroundMusic_Menu.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Menu();
		}
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		base.StartCoroutine(this.LoadNextLevel());
	}

	// Token: 0x0600264B RID: 9803 RVA: 0x0010A2F4 File Offset: 0x001084F4
	private IEnumerator LoadNextLevel()
	{
		GC.Collect();
		Resources.UnloadUnusedAssets();
		yield return new WaitForSeconds(3f);
		if (StagesParser.odgledaoTutorial == 0)
		{
			if (StagesParser.nivoZaUcitavanje == 1)
			{
				Application.LoadLevel(StagesParser.nivoZaUcitavanje);
				StagesParser.nivoZaUcitavanje = 0;
			}
			else
			{
				Application.LoadLevel("_Tutorial Level");
			}
		}
		else if (StagesParser.bonusLevel)
		{
			Application.LoadLevelAsync("_Bonus Level Clouds");
		}
		else
		{
			Application.LoadLevelAsync(StagesParser.nivoZaUcitavanje);
		}
		yield break;
	}

	// Token: 0x0600264C RID: 9804 RVA: 0x0010A2FC File Offset: 0x001084FC
	public IEnumerator UcitanaScena(Camera camera, int skaliraj, float delay)
	{
		StagesParser.loadingTip = -1;
		float time = 0.45f;
		if (skaliraj == 2)
		{
			base.transform.localScale = new Vector3(0.334f, 0.334f, 0.334f);
			base.transform.position = new Vector3(9f, -44.061f, -25.05859f);
			time = 0.65f;
		}
		else if (skaliraj == 3)
		{
			base.transform.localScale = new Vector3(0.334f, 0.334f, 0.334f);
			base.transform.position = new Vector3(82.20029f, -40.65633f, -25.05859f);
			time = 0.65f;
		}
		else if (skaliraj == 5)
		{
			base.transform.position = new Vector3(0f, 0f, -5f);
			base.transform.Find("Loading Animation Vrata").GetComponent<Animator>().speed = 2f;
			time = 0f;
		}
		else
		{
			base.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 5f);
		}
		yield return new WaitForSeconds(delay);
		base.transform.Find("Loading Animation Tip-s").GetComponent<Animator>().Play("Loading Tip Odlazak");
		base.StartCoroutine(this.unistiObjekat(time, skaliraj));
		yield break;
	}

	// Token: 0x0600264D RID: 9805 RVA: 0x0010A320 File Offset: 0x00108520
	private IEnumerator unistiObjekat(float time, int kojaScena)
	{
		yield return new WaitForSeconds(time);
		base.transform.Find("Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Odlazak");
		yield return new WaitForSeconds(1f);
		Object.Destroy(base.gameObject);
		if (kojaScena == 1)
		{
			GameObject.Find("GO screen").GetComponent<Collider>().enabled = true;
		}
		yield break;
	}

	// Token: 0x04001F8E RID: 8078
	private GameObject Tips;

	// Token: 0x04001F8F RID: 8079
	private GameObject Vrata;

	// Token: 0x04001F90 RID: 8080
	private TextMesh PozadinaText;

	// Token: 0x04001F91 RID: 8081
	private int RandomBroj;

	// Token: 0x04001F92 RID: 8082
	private string LoadingText;

	// Token: 0x04001F93 RID: 8083
	private static Loading instance;
}
