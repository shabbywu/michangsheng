using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006B7 RID: 1719
public class Loading : MonoBehaviour
{
	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000213E0 File Offset: 0x0001F5E0
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

	// Token: 0x06002AFD RID: 11005 RVA: 0x0002140D File Offset: 0x0001F60D
	private void Awake()
	{
		Loading.instance = this;
	}

	// Token: 0x06002AFE RID: 11006 RVA: 0x0014E068 File Offset: 0x0014C268
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

	// Token: 0x06002AFF RID: 11007 RVA: 0x00021415 File Offset: 0x0001F615
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

	// Token: 0x06002B00 RID: 11008 RVA: 0x0002141D File Offset: 0x0001F61D
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

	// Token: 0x06002B01 RID: 11009 RVA: 0x00021441 File Offset: 0x0001F641
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

	// Token: 0x04002533 RID: 9523
	private GameObject Tips;

	// Token: 0x04002534 RID: 9524
	private GameObject Vrata;

	// Token: 0x04002535 RID: 9525
	private TextMesh PozadinaText;

	// Token: 0x04002536 RID: 9526
	private int RandomBroj;

	// Token: 0x04002537 RID: 9527
	private string LoadingText;

	// Token: 0x04002538 RID: 9528
	private static Loading instance;
}
