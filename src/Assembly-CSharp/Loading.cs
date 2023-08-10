using System;
using System.Collections;
using UnityEngine;

public class Loading : MonoBehaviour
{
	private GameObject Tips;

	private GameObject Vrata;

	private TextMesh PozadinaText;

	private int RandomBroj;

	private string LoadingText;

	private static Loading instance;

	public static Loading Instance
	{
		get
		{
			if ((Object)(object)instance == (Object)null)
			{
				instance = Object.FindObjectOfType(typeof(Loading)) as Loading;
			}
			return instance;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		((Object)((Component)this).gameObject).name = "LOADING ZA BRISANJE";
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		if (StagesParser.LoadingPoruke.Count == 0)
		{
			StagesParser.LoadingPoruke.Clear();
			StagesParser.RedniBrojSlike.Clear();
			StagesParser.Instance.UcitajLoadingPoruke();
		}
		((Component)((Component)this).transform.Find("Loading Animation Tip-s/Loading sa Tip-om/Loading Text")).GetComponent<TextMesh>().text = LanguageManager.Loading;
		((Component)((Component)this).transform.Find("Loading Animation Tip-s/Loading sa Tip-om/Loading Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		RandomBroj = Random.Range(1, StagesParser.LoadingPoruke.Count);
		PozadinaText = GameObject.Find("Tip Text").GetComponent<TextMesh>();
		if (StagesParser.loadingTip == 1)
		{
			PozadinaText.text = LanguageManager.LoadingTip1;
			((Component)PozadinaText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true, increaseFont: false);
			Object obj = Object.Instantiate(Resources.Load("LoadingBackground/BgP8"));
			((GameObject)((obj is GameObject) ? obj : null)).transform.parent = GameObject.Find("Loading Tip BG").transform;
			RandomBroj = 8;
		}
		else if (StagesParser.loadingTip == 2)
		{
			PozadinaText.text = LanguageManager.LoadingTip2;
			((Component)PozadinaText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true, increaseFont: false);
			Object obj2 = Object.Instantiate(Resources.Load("LoadingBackground/BgP5"));
			((GameObject)((obj2 is GameObject) ? obj2 : null)).transform.parent = GameObject.Find("Loading Tip BG").transform;
			RandomBroj = 5;
		}
		else if (StagesParser.loadingTip == 3)
		{
			PozadinaText.text = LanguageManager.LoadingTip3;
			((Component)PozadinaText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true, increaseFont: false);
			Object obj3 = Object.Instantiate(Resources.Load("LoadingBackground/BgP3"));
			((GameObject)((obj3 is GameObject) ? obj3 : null)).transform.parent = GameObject.Find("Loading Tip BG").transform;
			RandomBroj = 3;
		}
		else
		{
			PozadinaText.text = StagesParser.LoadingPoruke[RandomBroj - 1];
			((Component)PozadinaText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true, increaseFont: false);
			StagesParser.LoadingPoruke.RemoveAt(RandomBroj - 1);
			Object obj4 = Object.Instantiate(Resources.Load("LoadingBackground/BgP" + StagesParser.RedniBrojSlike[RandomBroj - 1]));
			Object obj5 = ((obj4 is GameObject) ? obj4 : null);
			StagesParser.RedniBrojSlike.RemoveAt(RandomBroj - 1);
			((GameObject)obj5).transform.parent = GameObject.Find("Loading Tip BG").transform;
		}
		if (PlaySounds.BackgroundMusic_Menu.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Menu();
		}
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		((MonoBehaviour)this).StartCoroutine(LoadNextLevel());
	}

	private IEnumerator LoadNextLevel()
	{
		GC.Collect();
		Resources.UnloadUnusedAssets();
		yield return (object)new WaitForSeconds(3f);
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
	}

	public IEnumerator UcitanaScena(Camera camera, int skaliraj, float delay)
	{
		StagesParser.loadingTip = -1;
		float time = 0.45f;
		switch (skaliraj)
		{
		case 2:
			((Component)this).transform.localScale = new Vector3(0.334f, 0.334f, 0.334f);
			((Component)this).transform.position = new Vector3(9f, -44.061f, -25.05859f);
			time = 0.65f;
			break;
		case 3:
			((Component)this).transform.localScale = new Vector3(0.334f, 0.334f, 0.334f);
			((Component)this).transform.position = new Vector3(82.20029f, -40.65633f, -25.05859f);
			time = 0.65f;
			break;
		case 5:
			((Component)this).transform.position = new Vector3(0f, 0f, -5f);
			((Component)((Component)this).transform.Find("Loading Animation Vrata")).GetComponent<Animator>().speed = 2f;
			time = 0f;
			break;
		default:
			((Component)this).transform.position = new Vector3(((Component)camera).transform.position.x, ((Component)camera).transform.position.y, ((Component)camera).transform.position.z + 5f);
			break;
		}
		yield return (object)new WaitForSeconds(delay);
		((Component)((Component)this).transform.Find("Loading Animation Tip-s")).GetComponent<Animator>().Play("Loading Tip Odlazak");
		((MonoBehaviour)this).StartCoroutine(unistiObjekat(time, skaliraj));
	}

	private IEnumerator unistiObjekat(float time, int kojaScena)
	{
		yield return (object)new WaitForSeconds(time);
		((Component)((Component)this).transform.Find("Loading Animation Vrata")).GetComponent<Animator>().Play("Loading Zidovi Odlazak");
		yield return (object)new WaitForSeconds(1f);
		Object.Destroy((Object)(object)((Component)this).gameObject);
		if (kojaScena == 1)
		{
			GameObject.Find("GO screen").GetComponent<Collider>().enabled = true;
		}
	}
}
