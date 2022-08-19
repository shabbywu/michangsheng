using System;
using UnityEngine;

// Token: 0x020004D3 RID: 1235
public class PlaySounds : MonoBehaviour
{
	// Token: 0x060027EF RID: 10223 RVA: 0x0012F490 File Offset: 0x0012D690
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		PlaySounds.Button_MusicOn = base.transform.Find("Button_MusicOn").GetComponent<AudioSource>();
		PlaySounds.Button_SoundOn = base.transform.Find("Button_SoundOn").GetComponent<AudioSource>();
		PlaySounds.Button_Play = base.transform.Find("Button_Play").GetComponent<AudioSource>();
		PlaySounds.Button_GoBack = base.transform.Find("Button_GoBack").GetComponent<AudioSource>();
		PlaySounds.Button_OpenWorld = base.transform.Find("Button_OpenWorld").GetComponent<AudioSource>();
		PlaySounds.Button_OpenLevel = base.transform.Find("Button_OpenLevel").GetComponent<AudioSource>();
		PlaySounds.Button_LockedLevel_Click = base.transform.Find("Button_LockedLevel_Click").GetComponent<AudioSource>();
		PlaySounds.Button_Pause = base.transform.Find("Button_Pause").GetComponent<AudioSource>();
		PlaySounds.Button_NextLevel = base.transform.Find("Button_NextLevel").GetComponent<AudioSource>();
		PlaySounds.Button_RestartLevel = base.transform.Find("Button_RestartLevel").GetComponent<AudioSource>();
		PlaySounds.Run = base.transform.Find("Run").GetComponent<AudioSource>();
		PlaySounds.Jump1 = base.transform.Find("Jump1").GetComponent<AudioSource>();
		PlaySounds.Jump2 = base.transform.Find("Jump2").GetComponent<AudioSource>();
		PlaySounds.Jump3 = base.transform.Find("Jump3").GetComponent<AudioSource>();
		PlaySounds.VoiceJump1 = base.transform.Find("VoiceJump1").GetComponent<AudioSource>();
		PlaySounds.VoiceJump2 = base.transform.Find("VoiceJump2").GetComponent<AudioSource>();
		PlaySounds.VoiceJump3 = base.transform.Find("VoiceJump3").GetComponent<AudioSource>();
		PlaySounds.VoiceJump4 = base.transform.Find("VoiceJump4").GetComponent<AudioSource>();
		PlaySounds.VoiceJump5 = base.transform.Find("VoiceJump5").GetComponent<AudioSource>();
		PlaySounds.VoiceJump6 = base.transform.Find("VoiceJump6").GetComponent<AudioSource>();
		PlaySounds.VoiceJump7 = base.transform.Find("VoiceJump7").GetComponent<AudioSource>();
		PlaySounds.VoiceJump8 = base.transform.Find("VoiceJump8").GetComponent<AudioSource>();
		PlaySounds.Landing1 = base.transform.Find("Landing1").GetComponent<AudioSource>();
		PlaySounds.Landing2 = base.transform.Find("Landing2").GetComponent<AudioSource>();
		PlaySounds.Landing3 = base.transform.Find("Landing3").GetComponent<AudioSource>();
		PlaySounds.Landing_Strong = base.transform.Find("Landing_Strong").GetComponent<AudioSource>();
		PlaySounds.SmashBaboon = base.transform.Find("SmashBaboon").GetComponent<AudioSource>();
		PlaySounds.Level_Failed_Popup = base.transform.Find("Level_Failed_Popup").GetComponent<AudioSource>();
		PlaySounds.Level_Completed_Popup = base.transform.Find("Level_Completed_Popup").GetComponent<AudioSource>();
		PlaySounds.CollectCoin = base.transform.Find("CollectCoin").GetComponent<AudioSource>();
		PlaySounds.CollectCoin_2nd = base.transform.Find("CollectCoin_2nd").GetComponent<AudioSource>();
		PlaySounds.CollectCoin_3rd = base.transform.Find("CollectCoin_3rd").GetComponent<AudioSource>();
		PlaySounds.GetStar = base.transform.Find("GetStar").GetComponent<AudioSource>();
		PlaySounds.GetStar2 = base.transform.Find("GetStar2").GetComponent<AudioSource>();
		PlaySounds.GetStar3 = base.transform.Find("GetStar3").GetComponent<AudioSource>();
		PlaySounds.BackgroundMusic_Gameplay = base.transform.Find("BackgroundMusic_Gameplay").GetComponent<AudioSource>();
		PlaySounds.BackgroundMusic_Menu = base.transform.Find("BackgroundMusic_Menu").GetComponent<AudioSource>();
		PlaySounds.CoinsSpent = base.transform.Find("CoinsSpent").GetComponent<AudioSource>();
		PlaySounds.NoMoreCoins = base.transform.Find("NoMoreCoins").GetComponent<AudioSource>();
		PlaySounds.Biljka_Ugriz_NEW = base.transform.Find("PiranhaPlantBite").GetComponent<AudioSource>();
		PlaySounds.Collect_Banana_NEW = base.transform.Find("Collect_Banana").GetComponent<AudioSource>();
		PlaySounds.Collect_Diamond_NEW = base.transform.Find("Collect_Diamond").GetComponent<AudioSource>();
		PlaySounds.Collect_PowerUp_NEW = base.transform.Find("Collect_PowerUp").GetComponent<AudioSource>();
		PlaySounds.Glide_NEW = base.transform.Find("Glide").GetComponent<AudioSource>();
		PlaySounds.OtkljucavanjeNivoa_NEW = base.transform.Find("LevelUnlock").GetComponent<AudioSource>();
		PlaySounds.SmashGorilla_NEW = base.transform.Find("SmashGorilla").GetComponent<AudioSource>();
		PlaySounds.Otvaranje_Kovcega_NEW = base.transform.Find("UnlockChest").GetComponent<AudioSource>();
		PlaySounds.Bure_Eksplozija_NEW = base.transform.Find("BarrelExplode").GetComponent<AudioSource>();
		PlaySounds.MushroomBounce = base.transform.Find("MushroomBounce").GetComponent<AudioSource>();
		PlaySounds.Siljci = base.transform.Find("SpikesHit").GetComponent<AudioSource>();
		PlaySounds.TNTBure_Eksplozija = base.transform.Find("TNTBarrelExplode").GetComponent<AudioSource>();
		PlaySounds.LooseShield = base.transform.Find("LooseShield").GetComponent<AudioSource>();
		PlaySounds.MajmunUtepan = base.transform.Find("MonkeyKilled").GetComponent<AudioSource>();
		if (PlayerPrefs.HasKey("soundOn"))
		{
			PlaySounds.soundOn = (PlayerPrefs.GetInt("soundOn") == 1);
			PlaySounds.musicOn = (PlayerPrefs.GetInt("musicOn") == 1);
			return;
		}
		PlaySounds.soundOn = (PlaySounds.musicOn = true);
		PlayerPrefs.SetInt("soundOn", 1);
		PlayerPrefs.SetInt("musicOn", 1);
		PlayerPrefs.Save();
	}

	// Token: 0x060027F0 RID: 10224 RVA: 0x0012FA6A File Offset: 0x0012DC6A
	public static void Play_Button_MusicOn()
	{
		if (PlaySounds.Button_MusicOn.clip != null)
		{
			PlaySounds.Button_MusicOn.Play();
		}
	}

	// Token: 0x060027F1 RID: 10225 RVA: 0x0012FA88 File Offset: 0x0012DC88
	public static void Play_Button_SoundOn()
	{
		if (PlaySounds.Button_SoundOn.clip != null)
		{
			PlaySounds.Button_SoundOn.Play();
		}
	}

	// Token: 0x060027F2 RID: 10226 RVA: 0x0012FAA6 File Offset: 0x0012DCA6
	public static void Play_Button_Play()
	{
		if (PlaySounds.Button_Play.clip != null)
		{
			PlaySounds.Button_Play.Play();
		}
	}

	// Token: 0x060027F3 RID: 10227 RVA: 0x0012FAC4 File Offset: 0x0012DCC4
	public static void Play_Button_GoBack()
	{
		if (PlaySounds.Button_GoBack.clip != null)
		{
			PlaySounds.Button_GoBack.Play();
		}
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x0012FAE2 File Offset: 0x0012DCE2
	public static void Play_Button_OpenWorld()
	{
		if (PlaySounds.Button_OpenWorld.clip != null)
		{
			PlaySounds.Button_OpenWorld.Play();
		}
	}

	// Token: 0x060027F5 RID: 10229 RVA: 0x0012FB00 File Offset: 0x0012DD00
	public static void Play_Button_OpenLevel()
	{
		if (PlaySounds.Button_OpenLevel.clip != null)
		{
			PlaySounds.Button_OpenLevel.Play();
		}
	}

	// Token: 0x060027F6 RID: 10230 RVA: 0x0012FB00 File Offset: 0x0012DD00
	public static void Play_Button_Pause()
	{
		if (PlaySounds.Button_OpenLevel.clip != null)
		{
			PlaySounds.Button_OpenLevel.Play();
		}
	}

	// Token: 0x060027F7 RID: 10231 RVA: 0x0012FB1E File Offset: 0x0012DD1E
	public static void Play_Button_NextLevel()
	{
		if (PlaySounds.Button_NextLevel.clip != null)
		{
			PlaySounds.Button_NextLevel.Play();
		}
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x0012FB3C File Offset: 0x0012DD3C
	public static void Play_Button_RestartLevel()
	{
		if (PlaySounds.Button_RestartLevel.clip != null)
		{
			PlaySounds.Button_RestartLevel.Play();
		}
	}

	// Token: 0x060027F9 RID: 10233 RVA: 0x0012FB5A File Offset: 0x0012DD5A
	public static void Play_Button_LockedLevel_Click()
	{
		if (PlaySounds.Button_LockedLevel_Click.clip != null)
		{
			PlaySounds.Button_LockedLevel_Click.Play();
		}
	}

	// Token: 0x060027FA RID: 10234 RVA: 0x0012FB78 File Offset: 0x0012DD78
	public static void Play_Run()
	{
		if (PlaySounds.Run.clip != null)
		{
			PlaySounds.Run.pitch = Random.Range(0.9f, 1.8f);
			PlaySounds.Run.Play();
		}
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x0012FBAF File Offset: 0x0012DDAF
	public static void Stop_Run()
	{
		if (PlaySounds.Run.clip != null)
		{
			PlaySounds.Run.Stop();
		}
	}

	// Token: 0x060027FC RID: 10236 RVA: 0x0012FBD0 File Offset: 0x0012DDD0
	public static void Play_Jump()
	{
		switch (Random.Range(1, 4))
		{
		case 1:
			if (PlaySounds.Jump1.clip != null)
			{
				PlaySounds.Jump1.Play();
				return;
			}
			break;
		case 2:
			if (PlaySounds.Jump2.clip != null)
			{
				PlaySounds.Jump2.Play();
				return;
			}
			break;
		case 3:
			if (PlaySounds.Jump3.clip != null)
			{
				PlaySounds.Jump3.Play();
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060027FD RID: 10237 RVA: 0x0012FC50 File Offset: 0x0012DE50
	public static void Play_VoiceJump()
	{
		switch (Random.Range(1, 56))
		{
		case 1:
			if (PlaySounds.VoiceJump1.clip != null)
			{
				PlaySounds.VoiceJump1.Play();
				return;
			}
			break;
		case 2:
			if (PlaySounds.VoiceJump2.clip != null)
			{
				PlaySounds.VoiceJump2.Play();
				return;
			}
			break;
		case 3:
			if (PlaySounds.VoiceJump3.clip != null)
			{
				PlaySounds.VoiceJump3.Play();
				return;
			}
			break;
		case 4:
			if (PlaySounds.VoiceJump4.clip != null)
			{
				PlaySounds.VoiceJump4.Play();
				return;
			}
			break;
		case 5:
			if (PlaySounds.VoiceJump5.clip != null)
			{
				PlaySounds.VoiceJump5.Play();
				return;
			}
			break;
		case 6:
			if (PlaySounds.VoiceJump6.clip != null)
			{
				PlaySounds.VoiceJump6.Play();
				return;
			}
			break;
		case 7:
			if (PlaySounds.VoiceJump7.clip != null)
			{
				PlaySounds.VoiceJump7.Play();
				return;
			}
			break;
		case 8:
			if (PlaySounds.VoiceJump8.clip != null)
			{
				PlaySounds.VoiceJump8.Play();
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060027FE RID: 10238 RVA: 0x0012FD80 File Offset: 0x0012DF80
	public static void Play_Landing()
	{
		switch (Random.Range(1, 4))
		{
		case 1:
			if (PlaySounds.Landing1.clip != null)
			{
				PlaySounds.Landing1.Play();
				return;
			}
			break;
		case 2:
			if (PlaySounds.Landing2.clip != null)
			{
				PlaySounds.Landing2.Play();
				return;
			}
			break;
		case 3:
			if (PlaySounds.Landing3.clip != null)
			{
				PlaySounds.Landing3.Play();
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060027FF RID: 10239 RVA: 0x0012FE00 File Offset: 0x0012E000
	public static void Play_Landing_Strong()
	{
		if (PlaySounds.Landing_Strong.clip != null)
		{
			PlaySounds.Landing_Strong.Play();
		}
	}

	// Token: 0x06002800 RID: 10240 RVA: 0x0012FE20 File Offset: 0x0012E020
	public static void Play_SmashBaboon()
	{
		PlaySounds.zvukZaBabuna++;
		if (PlaySounds.zvukZaBabuna % 2 == 0)
		{
			if (PlaySounds.SmashGorilla_NEW.clip != null)
			{
				PlaySounds.SmashGorilla_NEW.Play();
				return;
			}
		}
		else if (PlaySounds.SmashBaboon.clip != null)
		{
			PlaySounds.SmashBaboon.Play();
		}
	}

	// Token: 0x06002801 RID: 10241 RVA: 0x0012FE7B File Offset: 0x0012E07B
	public static void Play_Level_Failed_Popup()
	{
		if (PlaySounds.Level_Failed_Popup.clip != null)
		{
			PlaySounds.Level_Failed_Popup.Play();
		}
	}

	// Token: 0x06002802 RID: 10242 RVA: 0x0012FE99 File Offset: 0x0012E099
	public static void Play_Level_Completed_Popup()
	{
		if (PlaySounds.Level_Completed_Popup.clip != null)
		{
			PlaySounds.Level_Completed_Popup.Play();
		}
	}

	// Token: 0x06002803 RID: 10243 RVA: 0x0012FEB7 File Offset: 0x0012E0B7
	public static void Play_CollectCoin()
	{
		if (PlaySounds.CollectCoin.clip != null)
		{
			PlaySounds.CollectCoin.Play();
		}
	}

	// Token: 0x06002804 RID: 10244 RVA: 0x0012FED5 File Offset: 0x0012E0D5
	public static void Play_CollectCoin_2nd()
	{
		if (PlaySounds.CollectCoin_2nd.clip != null)
		{
			PlaySounds.CollectCoin_2nd.Play();
		}
	}

	// Token: 0x06002805 RID: 10245 RVA: 0x0012FEF3 File Offset: 0x0012E0F3
	public static void Play_CollectCoin_3rd()
	{
		if (PlaySounds.CollectCoin_3rd.clip != null)
		{
			PlaySounds.CollectCoin_3rd.Play();
		}
	}

	// Token: 0x06002806 RID: 10246 RVA: 0x0012FF11 File Offset: 0x0012E111
	public static void Play_GetStar()
	{
		if (PlaySounds.GetStar.clip != null)
		{
			PlaySounds.GetStar.Play();
		}
	}

	// Token: 0x06002807 RID: 10247 RVA: 0x0012FF2F File Offset: 0x0012E12F
	public static void Play_GetStar2()
	{
		if (PlaySounds.GetStar2.clip != null)
		{
			PlaySounds.GetStar2.Play();
		}
	}

	// Token: 0x06002808 RID: 10248 RVA: 0x0012FF4D File Offset: 0x0012E14D
	public static void Play_GetStar3()
	{
		if (PlaySounds.GetStar3.clip != null)
		{
			PlaySounds.GetStar3.Play();
		}
	}

	// Token: 0x06002809 RID: 10249 RVA: 0x0012FF6B File Offset: 0x0012E16B
	public static void Play_CoinsSpent()
	{
		if (PlaySounds.CoinsSpent.clip != null)
		{
			PlaySounds.CoinsSpent.Play();
		}
	}

	// Token: 0x0600280A RID: 10250 RVA: 0x0012FF89 File Offset: 0x0012E189
	public static void Play_NoMoreCoins()
	{
		if (PlaySounds.NoMoreCoins.clip != null)
		{
			PlaySounds.NoMoreCoins.Play();
		}
	}

	// Token: 0x0600280B RID: 10251 RVA: 0x0012FFA7 File Offset: 0x0012E1A7
	public static void Play_BiljkaUgriz()
	{
		if (PlaySounds.Biljka_Ugriz_NEW.clip != null)
		{
			PlaySounds.Biljka_Ugriz_NEW.Play();
		}
	}

	// Token: 0x0600280C RID: 10252 RVA: 0x0012FFC5 File Offset: 0x0012E1C5
	public static void Play_CollectBanana()
	{
		if (PlaySounds.Collect_Banana_NEW.clip != null)
		{
			PlaySounds.Collect_Banana_NEW.Play();
		}
	}

	// Token: 0x0600280D RID: 10253 RVA: 0x0012FFE3 File Offset: 0x0012E1E3
	public static void Play_CollectDiamond()
	{
		if (PlaySounds.Collect_Diamond_NEW.clip != null)
		{
			PlaySounds.Collect_Diamond_NEW.Play();
		}
	}

	// Token: 0x0600280E RID: 10254 RVA: 0x00130001 File Offset: 0x0012E201
	public static void Play_CollectPowerUp()
	{
		if (PlaySounds.Collect_PowerUp_NEW.clip != null)
		{
			PlaySounds.Collect_PowerUp_NEW.Play();
		}
	}

	// Token: 0x0600280F RID: 10255 RVA: 0x0013001F File Offset: 0x0012E21F
	public static void Play_Glide()
	{
		if (PlaySounds.Glide_NEW.clip != null)
		{
			PlaySounds.Glide_NEW.Play();
		}
	}

	// Token: 0x06002810 RID: 10256 RVA: 0x0013003D File Offset: 0x0012E23D
	public static void Stop_Glide()
	{
		if (PlaySounds.Glide_NEW.clip != null)
		{
			PlaySounds.Glide_NEW.Stop();
		}
	}

	// Token: 0x06002811 RID: 10257 RVA: 0x0013005B File Offset: 0x0012E25B
	public static void Play_OtkljucavanjeNivoa()
	{
		if (PlaySounds.OtkljucavanjeNivoa_NEW.clip != null)
		{
			PlaySounds.OtkljucavanjeNivoa_NEW.Play();
		}
	}

	// Token: 0x06002812 RID: 10258 RVA: 0x00130079 File Offset: 0x0012E279
	public static void Play_SmashGorilla()
	{
		if (PlaySounds.SmashGorilla_NEW.clip != null)
		{
			PlaySounds.SmashGorilla_NEW.Play();
		}
	}

	// Token: 0x06002813 RID: 10259 RVA: 0x00130097 File Offset: 0x0012E297
	public static void Play_Otvaranje_Kovcega()
	{
		if (PlaySounds.Otvaranje_Kovcega_NEW.clip != null)
		{
			PlaySounds.Otvaranje_Kovcega_NEW.Play();
		}
	}

	// Token: 0x06002814 RID: 10260 RVA: 0x001300B5 File Offset: 0x0012E2B5
	public static void Play_Bure_Eksplozija()
	{
		if (PlaySounds.Bure_Eksplozija_NEW.clip != null)
		{
			PlaySounds.Bure_Eksplozija_NEW.Play();
		}
	}

	// Token: 0x06002815 RID: 10261 RVA: 0x001300D3 File Offset: 0x0012E2D3
	public static void Play_MushroomBounce()
	{
		if (PlaySounds.MushroomBounce.clip != null)
		{
			PlaySounds.MushroomBounce.Play();
		}
	}

	// Token: 0x06002816 RID: 10262 RVA: 0x001300F1 File Offset: 0x0012E2F1
	public static void Play_BackgroundMusic_Gameplay()
	{
		if (PlaySounds.BackgroundMusic_Gameplay.clip != null)
		{
			PlaySounds.BackgroundMusic_Gameplay.Play();
		}
	}

	// Token: 0x06002817 RID: 10263 RVA: 0x0013010F File Offset: 0x0012E30F
	public static void Play_BackgroundMusic_Menu()
	{
		if (PlaySounds.BackgroundMusic_Menu.clip != null)
		{
			PlaySounds.BackgroundMusic_Menu.Play();
		}
	}

	// Token: 0x06002818 RID: 10264 RVA: 0x0013012D File Offset: 0x0012E32D
	public static void Stop_BackgroundMusic_Gameplay()
	{
		if (PlaySounds.BackgroundMusic_Gameplay.clip != null)
		{
			PlaySounds.BackgroundMusic_Gameplay.Stop();
		}
	}

	// Token: 0x06002819 RID: 10265 RVA: 0x0013014B File Offset: 0x0012E34B
	public static void Stop_BackgroundMusic_Menu()
	{
		if (PlaySounds.BackgroundMusic_Menu.clip != null)
		{
			PlaySounds.BackgroundMusic_Menu.Stop();
		}
	}

	// Token: 0x0600281A RID: 10266 RVA: 0x00130169 File Offset: 0x0012E369
	public static void Stop_Level_Failed_Popup()
	{
		if (PlaySounds.Level_Failed_Popup.clip != null)
		{
			PlaySounds.Level_Failed_Popup.Stop();
		}
	}

	// Token: 0x0600281B RID: 10267 RVA: 0x00130187 File Offset: 0x0012E387
	public static void Play_Siljci()
	{
		if (PlaySounds.Siljci.clip != null)
		{
			PlaySounds.Siljci.Play();
		}
	}

	// Token: 0x0600281C RID: 10268 RVA: 0x001301A5 File Offset: 0x0012E3A5
	public static void Play_TNTBure_Eksplozija()
	{
		if (PlaySounds.TNTBure_Eksplozija.clip != null)
		{
			PlaySounds.TNTBure_Eksplozija.Play();
		}
	}

	// Token: 0x0600281D RID: 10269 RVA: 0x001301C3 File Offset: 0x0012E3C3
	public static void Play_LooseShield()
	{
		if (PlaySounds.LooseShield.clip != null)
		{
			PlaySounds.LooseShield.Play();
		}
	}

	// Token: 0x0600281E RID: 10270 RVA: 0x001301E1 File Offset: 0x0012E3E1
	public static void Play_MajmunUtepan()
	{
		if (PlaySounds.MajmunUtepan.clip != null)
		{
			PlaySounds.MajmunUtepan.Play();
		}
	}

	// Token: 0x040022F7 RID: 8951
	private static AudioSource Button_MusicOn;

	// Token: 0x040022F8 RID: 8952
	private static AudioSource Button_SoundOn;

	// Token: 0x040022F9 RID: 8953
	private static AudioSource Button_Play;

	// Token: 0x040022FA RID: 8954
	private static AudioSource Button_GoBack;

	// Token: 0x040022FB RID: 8955
	private static AudioSource Button_OpenWorld;

	// Token: 0x040022FC RID: 8956
	private static AudioSource Button_OpenLevel;

	// Token: 0x040022FD RID: 8957
	private static AudioSource Button_LockedLevel_Click;

	// Token: 0x040022FE RID: 8958
	private static AudioSource Button_Pause;

	// Token: 0x040022FF RID: 8959
	private static AudioSource Button_NextLevel;

	// Token: 0x04002300 RID: 8960
	private static AudioSource Button_RestartLevel;

	// Token: 0x04002301 RID: 8961
	[HideInInspector]
	public static AudioSource Run;

	// Token: 0x04002302 RID: 8962
	private static AudioSource Jump1;

	// Token: 0x04002303 RID: 8963
	private static AudioSource Jump2;

	// Token: 0x04002304 RID: 8964
	private static AudioSource Jump3;

	// Token: 0x04002305 RID: 8965
	private static AudioSource VoiceJump1;

	// Token: 0x04002306 RID: 8966
	private static AudioSource VoiceJump2;

	// Token: 0x04002307 RID: 8967
	private static AudioSource VoiceJump3;

	// Token: 0x04002308 RID: 8968
	private static AudioSource VoiceJump4;

	// Token: 0x04002309 RID: 8969
	private static AudioSource VoiceJump5;

	// Token: 0x0400230A RID: 8970
	private static AudioSource VoiceJump6;

	// Token: 0x0400230B RID: 8971
	private static AudioSource VoiceJump7;

	// Token: 0x0400230C RID: 8972
	private static AudioSource VoiceJump8;

	// Token: 0x0400230D RID: 8973
	private static AudioSource Landing1;

	// Token: 0x0400230E RID: 8974
	private static AudioSource Landing2;

	// Token: 0x0400230F RID: 8975
	private static AudioSource Landing3;

	// Token: 0x04002310 RID: 8976
	private static AudioSource Landing_Strong;

	// Token: 0x04002311 RID: 8977
	private static AudioSource SmashBaboon;

	// Token: 0x04002312 RID: 8978
	[HideInInspector]
	public static AudioSource Level_Failed_Popup;

	// Token: 0x04002313 RID: 8979
	private static AudioSource Level_Completed_Popup;

	// Token: 0x04002314 RID: 8980
	private static AudioSource CollectCoin;

	// Token: 0x04002315 RID: 8981
	private static AudioSource CollectCoin_2nd;

	// Token: 0x04002316 RID: 8982
	private static AudioSource CollectCoin_3rd;

	// Token: 0x04002317 RID: 8983
	private static AudioSource GetStar;

	// Token: 0x04002318 RID: 8984
	private static AudioSource GetStar2;

	// Token: 0x04002319 RID: 8985
	private static AudioSource GetStar3;

	// Token: 0x0400231A RID: 8986
	private static AudioSource CoinsSpent;

	// Token: 0x0400231B RID: 8987
	private static AudioSource NoMoreCoins;

	// Token: 0x0400231C RID: 8988
	private static AudioSource Biljka_Ugriz_NEW;

	// Token: 0x0400231D RID: 8989
	private static AudioSource Collect_Banana_NEW;

	// Token: 0x0400231E RID: 8990
	private static AudioSource Collect_Diamond_NEW;

	// Token: 0x0400231F RID: 8991
	private static AudioSource Collect_PowerUp_NEW;

	// Token: 0x04002320 RID: 8992
	[HideInInspector]
	public static AudioSource Glide_NEW;

	// Token: 0x04002321 RID: 8993
	private static AudioSource OtkljucavanjeNivoa_NEW;

	// Token: 0x04002322 RID: 8994
	private static AudioSource SmashGorilla_NEW;

	// Token: 0x04002323 RID: 8995
	private static AudioSource Otvaranje_Kovcega_NEW;

	// Token: 0x04002324 RID: 8996
	private static AudioSource Bure_Eksplozija_NEW;

	// Token: 0x04002325 RID: 8997
	private static AudioSource MushroomBounce;

	// Token: 0x04002326 RID: 8998
	private static AudioSource Siljci;

	// Token: 0x04002327 RID: 8999
	private static AudioSource TNTBure_Eksplozija;

	// Token: 0x04002328 RID: 9000
	private static AudioSource LooseShield;

	// Token: 0x04002329 RID: 9001
	private static AudioSource MajmunUtepan;

	// Token: 0x0400232A RID: 9002
	[HideInInspector]
	public static AudioSource BackgroundMusic_Gameplay;

	// Token: 0x0400232B RID: 9003
	[HideInInspector]
	public static AudioSource BackgroundMusic_Menu;

	// Token: 0x0400232C RID: 9004
	[HideInInspector]
	public static bool soundOn;

	// Token: 0x0400232D RID: 9005
	[HideInInspector]
	public static bool musicOn;

	// Token: 0x0400232E RID: 9006
	private static int zvukZaBabuna;
}
