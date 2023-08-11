using UnityEngine;

public class PlaySounds : MonoBehaviour
{
	private static AudioSource Button_MusicOn;

	private static AudioSource Button_SoundOn;

	private static AudioSource Button_Play;

	private static AudioSource Button_GoBack;

	private static AudioSource Button_OpenWorld;

	private static AudioSource Button_OpenLevel;

	private static AudioSource Button_LockedLevel_Click;

	private static AudioSource Button_Pause;

	private static AudioSource Button_NextLevel;

	private static AudioSource Button_RestartLevel;

	[HideInInspector]
	public static AudioSource Run;

	private static AudioSource Jump1;

	private static AudioSource Jump2;

	private static AudioSource Jump3;

	private static AudioSource VoiceJump1;

	private static AudioSource VoiceJump2;

	private static AudioSource VoiceJump3;

	private static AudioSource VoiceJump4;

	private static AudioSource VoiceJump5;

	private static AudioSource VoiceJump6;

	private static AudioSource VoiceJump7;

	private static AudioSource VoiceJump8;

	private static AudioSource Landing1;

	private static AudioSource Landing2;

	private static AudioSource Landing3;

	private static AudioSource Landing_Strong;

	private static AudioSource SmashBaboon;

	[HideInInspector]
	public static AudioSource Level_Failed_Popup;

	private static AudioSource Level_Completed_Popup;

	private static AudioSource CollectCoin;

	private static AudioSource CollectCoin_2nd;

	private static AudioSource CollectCoin_3rd;

	private static AudioSource GetStar;

	private static AudioSource GetStar2;

	private static AudioSource GetStar3;

	private static AudioSource CoinsSpent;

	private static AudioSource NoMoreCoins;

	private static AudioSource Biljka_Ugriz_NEW;

	private static AudioSource Collect_Banana_NEW;

	private static AudioSource Collect_Diamond_NEW;

	private static AudioSource Collect_PowerUp_NEW;

	[HideInInspector]
	public static AudioSource Glide_NEW;

	private static AudioSource OtkljucavanjeNivoa_NEW;

	private static AudioSource SmashGorilla_NEW;

	private static AudioSource Otvaranje_Kovcega_NEW;

	private static AudioSource Bure_Eksplozija_NEW;

	private static AudioSource MushroomBounce;

	private static AudioSource Siljci;

	private static AudioSource TNTBure_Eksplozija;

	private static AudioSource LooseShield;

	private static AudioSource MajmunUtepan;

	[HideInInspector]
	public static AudioSource BackgroundMusic_Gameplay;

	[HideInInspector]
	public static AudioSource BackgroundMusic_Menu;

	[HideInInspector]
	public static bool soundOn;

	[HideInInspector]
	public static bool musicOn;

	private static int zvukZaBabuna;

	private void Awake()
	{
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		Button_MusicOn = ((Component)((Component)this).transform.Find("Button_MusicOn")).GetComponent<AudioSource>();
		Button_SoundOn = ((Component)((Component)this).transform.Find("Button_SoundOn")).GetComponent<AudioSource>();
		Button_Play = ((Component)((Component)this).transform.Find("Button_Play")).GetComponent<AudioSource>();
		Button_GoBack = ((Component)((Component)this).transform.Find("Button_GoBack")).GetComponent<AudioSource>();
		Button_OpenWorld = ((Component)((Component)this).transform.Find("Button_OpenWorld")).GetComponent<AudioSource>();
		Button_OpenLevel = ((Component)((Component)this).transform.Find("Button_OpenLevel")).GetComponent<AudioSource>();
		Button_LockedLevel_Click = ((Component)((Component)this).transform.Find("Button_LockedLevel_Click")).GetComponent<AudioSource>();
		Button_Pause = ((Component)((Component)this).transform.Find("Button_Pause")).GetComponent<AudioSource>();
		Button_NextLevel = ((Component)((Component)this).transform.Find("Button_NextLevel")).GetComponent<AudioSource>();
		Button_RestartLevel = ((Component)((Component)this).transform.Find("Button_RestartLevel")).GetComponent<AudioSource>();
		Run = ((Component)((Component)this).transform.Find("Run")).GetComponent<AudioSource>();
		Jump1 = ((Component)((Component)this).transform.Find("Jump1")).GetComponent<AudioSource>();
		Jump2 = ((Component)((Component)this).transform.Find("Jump2")).GetComponent<AudioSource>();
		Jump3 = ((Component)((Component)this).transform.Find("Jump3")).GetComponent<AudioSource>();
		VoiceJump1 = ((Component)((Component)this).transform.Find("VoiceJump1")).GetComponent<AudioSource>();
		VoiceJump2 = ((Component)((Component)this).transform.Find("VoiceJump2")).GetComponent<AudioSource>();
		VoiceJump3 = ((Component)((Component)this).transform.Find("VoiceJump3")).GetComponent<AudioSource>();
		VoiceJump4 = ((Component)((Component)this).transform.Find("VoiceJump4")).GetComponent<AudioSource>();
		VoiceJump5 = ((Component)((Component)this).transform.Find("VoiceJump5")).GetComponent<AudioSource>();
		VoiceJump6 = ((Component)((Component)this).transform.Find("VoiceJump6")).GetComponent<AudioSource>();
		VoiceJump7 = ((Component)((Component)this).transform.Find("VoiceJump7")).GetComponent<AudioSource>();
		VoiceJump8 = ((Component)((Component)this).transform.Find("VoiceJump8")).GetComponent<AudioSource>();
		Landing1 = ((Component)((Component)this).transform.Find("Landing1")).GetComponent<AudioSource>();
		Landing2 = ((Component)((Component)this).transform.Find("Landing2")).GetComponent<AudioSource>();
		Landing3 = ((Component)((Component)this).transform.Find("Landing3")).GetComponent<AudioSource>();
		Landing_Strong = ((Component)((Component)this).transform.Find("Landing_Strong")).GetComponent<AudioSource>();
		SmashBaboon = ((Component)((Component)this).transform.Find("SmashBaboon")).GetComponent<AudioSource>();
		Level_Failed_Popup = ((Component)((Component)this).transform.Find("Level_Failed_Popup")).GetComponent<AudioSource>();
		Level_Completed_Popup = ((Component)((Component)this).transform.Find("Level_Completed_Popup")).GetComponent<AudioSource>();
		CollectCoin = ((Component)((Component)this).transform.Find("CollectCoin")).GetComponent<AudioSource>();
		CollectCoin_2nd = ((Component)((Component)this).transform.Find("CollectCoin_2nd")).GetComponent<AudioSource>();
		CollectCoin_3rd = ((Component)((Component)this).transform.Find("CollectCoin_3rd")).GetComponent<AudioSource>();
		GetStar = ((Component)((Component)this).transform.Find("GetStar")).GetComponent<AudioSource>();
		GetStar2 = ((Component)((Component)this).transform.Find("GetStar2")).GetComponent<AudioSource>();
		GetStar3 = ((Component)((Component)this).transform.Find("GetStar3")).GetComponent<AudioSource>();
		BackgroundMusic_Gameplay = ((Component)((Component)this).transform.Find("BackgroundMusic_Gameplay")).GetComponent<AudioSource>();
		BackgroundMusic_Menu = ((Component)((Component)this).transform.Find("BackgroundMusic_Menu")).GetComponent<AudioSource>();
		CoinsSpent = ((Component)((Component)this).transform.Find("CoinsSpent")).GetComponent<AudioSource>();
		NoMoreCoins = ((Component)((Component)this).transform.Find("NoMoreCoins")).GetComponent<AudioSource>();
		Biljka_Ugriz_NEW = ((Component)((Component)this).transform.Find("PiranhaPlantBite")).GetComponent<AudioSource>();
		Collect_Banana_NEW = ((Component)((Component)this).transform.Find("Collect_Banana")).GetComponent<AudioSource>();
		Collect_Diamond_NEW = ((Component)((Component)this).transform.Find("Collect_Diamond")).GetComponent<AudioSource>();
		Collect_PowerUp_NEW = ((Component)((Component)this).transform.Find("Collect_PowerUp")).GetComponent<AudioSource>();
		Glide_NEW = ((Component)((Component)this).transform.Find("Glide")).GetComponent<AudioSource>();
		OtkljucavanjeNivoa_NEW = ((Component)((Component)this).transform.Find("LevelUnlock")).GetComponent<AudioSource>();
		SmashGorilla_NEW = ((Component)((Component)this).transform.Find("SmashGorilla")).GetComponent<AudioSource>();
		Otvaranje_Kovcega_NEW = ((Component)((Component)this).transform.Find("UnlockChest")).GetComponent<AudioSource>();
		Bure_Eksplozija_NEW = ((Component)((Component)this).transform.Find("BarrelExplode")).GetComponent<AudioSource>();
		MushroomBounce = ((Component)((Component)this).transform.Find("MushroomBounce")).GetComponent<AudioSource>();
		Siljci = ((Component)((Component)this).transform.Find("SpikesHit")).GetComponent<AudioSource>();
		TNTBure_Eksplozija = ((Component)((Component)this).transform.Find("TNTBarrelExplode")).GetComponent<AudioSource>();
		LooseShield = ((Component)((Component)this).transform.Find("LooseShield")).GetComponent<AudioSource>();
		MajmunUtepan = ((Component)((Component)this).transform.Find("MonkeyKilled")).GetComponent<AudioSource>();
		if (PlayerPrefs.HasKey("soundOn"))
		{
			soundOn = PlayerPrefs.GetInt("soundOn") == 1;
			musicOn = PlayerPrefs.GetInt("musicOn") == 1;
			return;
		}
		soundOn = (musicOn = true);
		PlayerPrefs.SetInt("soundOn", 1);
		PlayerPrefs.SetInt("musicOn", 1);
		PlayerPrefs.Save();
	}

	public static void Play_Button_MusicOn()
	{
		if ((Object)(object)Button_MusicOn.clip != (Object)null)
		{
			Button_MusicOn.Play();
		}
	}

	public static void Play_Button_SoundOn()
	{
		if ((Object)(object)Button_SoundOn.clip != (Object)null)
		{
			Button_SoundOn.Play();
		}
	}

	public static void Play_Button_Play()
	{
		if ((Object)(object)Button_Play.clip != (Object)null)
		{
			Button_Play.Play();
		}
	}

	public static void Play_Button_GoBack()
	{
		if ((Object)(object)Button_GoBack.clip != (Object)null)
		{
			Button_GoBack.Play();
		}
	}

	public static void Play_Button_OpenWorld()
	{
		if ((Object)(object)Button_OpenWorld.clip != (Object)null)
		{
			Button_OpenWorld.Play();
		}
	}

	public static void Play_Button_OpenLevel()
	{
		if ((Object)(object)Button_OpenLevel.clip != (Object)null)
		{
			Button_OpenLevel.Play();
		}
	}

	public static void Play_Button_Pause()
	{
		if ((Object)(object)Button_OpenLevel.clip != (Object)null)
		{
			Button_OpenLevel.Play();
		}
	}

	public static void Play_Button_NextLevel()
	{
		if ((Object)(object)Button_NextLevel.clip != (Object)null)
		{
			Button_NextLevel.Play();
		}
	}

	public static void Play_Button_RestartLevel()
	{
		if ((Object)(object)Button_RestartLevel.clip != (Object)null)
		{
			Button_RestartLevel.Play();
		}
	}

	public static void Play_Button_LockedLevel_Click()
	{
		if ((Object)(object)Button_LockedLevel_Click.clip != (Object)null)
		{
			Button_LockedLevel_Click.Play();
		}
	}

	public static void Play_Run()
	{
		if ((Object)(object)Run.clip != (Object)null)
		{
			Run.pitch = Random.Range(0.9f, 1.8f);
			Run.Play();
		}
	}

	public static void Stop_Run()
	{
		if ((Object)(object)Run.clip != (Object)null)
		{
			Run.Stop();
		}
	}

	public static void Play_Jump()
	{
		switch (Random.Range(1, 4))
		{
		case 1:
			if ((Object)(object)Jump1.clip != (Object)null)
			{
				Jump1.Play();
			}
			break;
		case 2:
			if ((Object)(object)Jump2.clip != (Object)null)
			{
				Jump2.Play();
			}
			break;
		case 3:
			if ((Object)(object)Jump3.clip != (Object)null)
			{
				Jump3.Play();
			}
			break;
		}
	}

	public static void Play_VoiceJump()
	{
		switch (Random.Range(1, 56))
		{
		case 1:
			if ((Object)(object)VoiceJump1.clip != (Object)null)
			{
				VoiceJump1.Play();
			}
			break;
		case 2:
			if ((Object)(object)VoiceJump2.clip != (Object)null)
			{
				VoiceJump2.Play();
			}
			break;
		case 3:
			if ((Object)(object)VoiceJump3.clip != (Object)null)
			{
				VoiceJump3.Play();
			}
			break;
		case 4:
			if ((Object)(object)VoiceJump4.clip != (Object)null)
			{
				VoiceJump4.Play();
			}
			break;
		case 5:
			if ((Object)(object)VoiceJump5.clip != (Object)null)
			{
				VoiceJump5.Play();
			}
			break;
		case 6:
			if ((Object)(object)VoiceJump6.clip != (Object)null)
			{
				VoiceJump6.Play();
			}
			break;
		case 7:
			if ((Object)(object)VoiceJump7.clip != (Object)null)
			{
				VoiceJump7.Play();
			}
			break;
		case 8:
			if ((Object)(object)VoiceJump8.clip != (Object)null)
			{
				VoiceJump8.Play();
			}
			break;
		}
	}

	public static void Play_Landing()
	{
		switch (Random.Range(1, 4))
		{
		case 1:
			if ((Object)(object)Landing1.clip != (Object)null)
			{
				Landing1.Play();
			}
			break;
		case 2:
			if ((Object)(object)Landing2.clip != (Object)null)
			{
				Landing2.Play();
			}
			break;
		case 3:
			if ((Object)(object)Landing3.clip != (Object)null)
			{
				Landing3.Play();
			}
			break;
		}
	}

	public static void Play_Landing_Strong()
	{
		if ((Object)(object)Landing_Strong.clip != (Object)null)
		{
			Landing_Strong.Play();
		}
	}

	public static void Play_SmashBaboon()
	{
		zvukZaBabuna++;
		if (zvukZaBabuna % 2 == 0)
		{
			if ((Object)(object)SmashGorilla_NEW.clip != (Object)null)
			{
				SmashGorilla_NEW.Play();
			}
		}
		else if ((Object)(object)SmashBaboon.clip != (Object)null)
		{
			SmashBaboon.Play();
		}
	}

	public static void Play_Level_Failed_Popup()
	{
		if ((Object)(object)Level_Failed_Popup.clip != (Object)null)
		{
			Level_Failed_Popup.Play();
		}
	}

	public static void Play_Level_Completed_Popup()
	{
		if ((Object)(object)Level_Completed_Popup.clip != (Object)null)
		{
			Level_Completed_Popup.Play();
		}
	}

	public static void Play_CollectCoin()
	{
		if ((Object)(object)CollectCoin.clip != (Object)null)
		{
			CollectCoin.Play();
		}
	}

	public static void Play_CollectCoin_2nd()
	{
		if ((Object)(object)CollectCoin_2nd.clip != (Object)null)
		{
			CollectCoin_2nd.Play();
		}
	}

	public static void Play_CollectCoin_3rd()
	{
		if ((Object)(object)CollectCoin_3rd.clip != (Object)null)
		{
			CollectCoin_3rd.Play();
		}
	}

	public static void Play_GetStar()
	{
		if ((Object)(object)GetStar.clip != (Object)null)
		{
			GetStar.Play();
		}
	}

	public static void Play_GetStar2()
	{
		if ((Object)(object)GetStar2.clip != (Object)null)
		{
			GetStar2.Play();
		}
	}

	public static void Play_GetStar3()
	{
		if ((Object)(object)GetStar3.clip != (Object)null)
		{
			GetStar3.Play();
		}
	}

	public static void Play_CoinsSpent()
	{
		if ((Object)(object)CoinsSpent.clip != (Object)null)
		{
			CoinsSpent.Play();
		}
	}

	public static void Play_NoMoreCoins()
	{
		if ((Object)(object)NoMoreCoins.clip != (Object)null)
		{
			NoMoreCoins.Play();
		}
	}

	public static void Play_BiljkaUgriz()
	{
		if ((Object)(object)Biljka_Ugriz_NEW.clip != (Object)null)
		{
			Biljka_Ugriz_NEW.Play();
		}
	}

	public static void Play_CollectBanana()
	{
		if ((Object)(object)Collect_Banana_NEW.clip != (Object)null)
		{
			Collect_Banana_NEW.Play();
		}
	}

	public static void Play_CollectDiamond()
	{
		if ((Object)(object)Collect_Diamond_NEW.clip != (Object)null)
		{
			Collect_Diamond_NEW.Play();
		}
	}

	public static void Play_CollectPowerUp()
	{
		if ((Object)(object)Collect_PowerUp_NEW.clip != (Object)null)
		{
			Collect_PowerUp_NEW.Play();
		}
	}

	public static void Play_Glide()
	{
		if ((Object)(object)Glide_NEW.clip != (Object)null)
		{
			Glide_NEW.Play();
		}
	}

	public static void Stop_Glide()
	{
		if ((Object)(object)Glide_NEW.clip != (Object)null)
		{
			Glide_NEW.Stop();
		}
	}

	public static void Play_OtkljucavanjeNivoa()
	{
		if ((Object)(object)OtkljucavanjeNivoa_NEW.clip != (Object)null)
		{
			OtkljucavanjeNivoa_NEW.Play();
		}
	}

	public static void Play_SmashGorilla()
	{
		if ((Object)(object)SmashGorilla_NEW.clip != (Object)null)
		{
			SmashGorilla_NEW.Play();
		}
	}

	public static void Play_Otvaranje_Kovcega()
	{
		if ((Object)(object)Otvaranje_Kovcega_NEW.clip != (Object)null)
		{
			Otvaranje_Kovcega_NEW.Play();
		}
	}

	public static void Play_Bure_Eksplozija()
	{
		if ((Object)(object)Bure_Eksplozija_NEW.clip != (Object)null)
		{
			Bure_Eksplozija_NEW.Play();
		}
	}

	public static void Play_MushroomBounce()
	{
		if ((Object)(object)MushroomBounce.clip != (Object)null)
		{
			MushroomBounce.Play();
		}
	}

	public static void Play_BackgroundMusic_Gameplay()
	{
		if ((Object)(object)BackgroundMusic_Gameplay.clip != (Object)null)
		{
			BackgroundMusic_Gameplay.Play();
		}
	}

	public static void Play_BackgroundMusic_Menu()
	{
		if ((Object)(object)BackgroundMusic_Menu.clip != (Object)null)
		{
			BackgroundMusic_Menu.Play();
		}
	}

	public static void Stop_BackgroundMusic_Gameplay()
	{
		if ((Object)(object)BackgroundMusic_Gameplay.clip != (Object)null)
		{
			BackgroundMusic_Gameplay.Stop();
		}
	}

	public static void Stop_BackgroundMusic_Menu()
	{
		if ((Object)(object)BackgroundMusic_Menu.clip != (Object)null)
		{
			BackgroundMusic_Menu.Stop();
		}
	}

	public static void Stop_Level_Failed_Popup()
	{
		if ((Object)(object)Level_Failed_Popup.clip != (Object)null)
		{
			Level_Failed_Popup.Stop();
		}
	}

	public static void Play_Siljci()
	{
		if ((Object)(object)Siljci.clip != (Object)null)
		{
			Siljci.Play();
		}
	}

	public static void Play_TNTBure_Eksplozija()
	{
		if ((Object)(object)TNTBure_Eksplozija.clip != (Object)null)
		{
			TNTBure_Eksplozija.Play();
		}
	}

	public static void Play_LooseShield()
	{
		if ((Object)(object)LooseShield.clip != (Object)null)
		{
			LooseShield.Play();
		}
	}

	public static void Play_MajmunUtepan()
	{
		if ((Object)(object)MajmunUtepan.clip != (Object)null)
		{
			MajmunUtepan.Play();
		}
	}
}
