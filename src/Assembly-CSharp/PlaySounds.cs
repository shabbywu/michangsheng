using System;
using UnityEngine;

// Token: 0x02000746 RID: 1862
public class PlaySounds : MonoBehaviour
{
	// Token: 0x06002F46 RID: 12102 RVA: 0x0017C210 File Offset: 0x0017A410
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

	// Token: 0x06002F47 RID: 12103 RVA: 0x00022DE6 File Offset: 0x00020FE6
	public static void Play_Button_MusicOn()
	{
		if (PlaySounds.Button_MusicOn.clip != null)
		{
			PlaySounds.Button_MusicOn.Play();
		}
	}

	// Token: 0x06002F48 RID: 12104 RVA: 0x00022E04 File Offset: 0x00021004
	public static void Play_Button_SoundOn()
	{
		if (PlaySounds.Button_SoundOn.clip != null)
		{
			PlaySounds.Button_SoundOn.Play();
		}
	}

	// Token: 0x06002F49 RID: 12105 RVA: 0x00022E22 File Offset: 0x00021022
	public static void Play_Button_Play()
	{
		if (PlaySounds.Button_Play.clip != null)
		{
			PlaySounds.Button_Play.Play();
		}
	}

	// Token: 0x06002F4A RID: 12106 RVA: 0x00022E40 File Offset: 0x00021040
	public static void Play_Button_GoBack()
	{
		if (PlaySounds.Button_GoBack.clip != null)
		{
			PlaySounds.Button_GoBack.Play();
		}
	}

	// Token: 0x06002F4B RID: 12107 RVA: 0x00022E5E File Offset: 0x0002105E
	public static void Play_Button_OpenWorld()
	{
		if (PlaySounds.Button_OpenWorld.clip != null)
		{
			PlaySounds.Button_OpenWorld.Play();
		}
	}

	// Token: 0x06002F4C RID: 12108 RVA: 0x00022E7C File Offset: 0x0002107C
	public static void Play_Button_OpenLevel()
	{
		if (PlaySounds.Button_OpenLevel.clip != null)
		{
			PlaySounds.Button_OpenLevel.Play();
		}
	}

	// Token: 0x06002F4D RID: 12109 RVA: 0x00022E7C File Offset: 0x0002107C
	public static void Play_Button_Pause()
	{
		if (PlaySounds.Button_OpenLevel.clip != null)
		{
			PlaySounds.Button_OpenLevel.Play();
		}
	}

	// Token: 0x06002F4E RID: 12110 RVA: 0x00022E9A File Offset: 0x0002109A
	public static void Play_Button_NextLevel()
	{
		if (PlaySounds.Button_NextLevel.clip != null)
		{
			PlaySounds.Button_NextLevel.Play();
		}
	}

	// Token: 0x06002F4F RID: 12111 RVA: 0x00022EB8 File Offset: 0x000210B8
	public static void Play_Button_RestartLevel()
	{
		if (PlaySounds.Button_RestartLevel.clip != null)
		{
			PlaySounds.Button_RestartLevel.Play();
		}
	}

	// Token: 0x06002F50 RID: 12112 RVA: 0x00022ED6 File Offset: 0x000210D6
	public static void Play_Button_LockedLevel_Click()
	{
		if (PlaySounds.Button_LockedLevel_Click.clip != null)
		{
			PlaySounds.Button_LockedLevel_Click.Play();
		}
	}

	// Token: 0x06002F51 RID: 12113 RVA: 0x00022EF4 File Offset: 0x000210F4
	public static void Play_Run()
	{
		if (PlaySounds.Run.clip != null)
		{
			PlaySounds.Run.pitch = Random.Range(0.9f, 1.8f);
			PlaySounds.Run.Play();
		}
	}

	// Token: 0x06002F52 RID: 12114 RVA: 0x00022F2B File Offset: 0x0002112B
	public static void Stop_Run()
	{
		if (PlaySounds.Run.clip != null)
		{
			PlaySounds.Run.Stop();
		}
	}

	// Token: 0x06002F53 RID: 12115 RVA: 0x0017C7EC File Offset: 0x0017A9EC
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

	// Token: 0x06002F54 RID: 12116 RVA: 0x0017C86C File Offset: 0x0017AA6C
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

	// Token: 0x06002F55 RID: 12117 RVA: 0x0017C99C File Offset: 0x0017AB9C
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

	// Token: 0x06002F56 RID: 12118 RVA: 0x00022F49 File Offset: 0x00021149
	public static void Play_Landing_Strong()
	{
		if (PlaySounds.Landing_Strong.clip != null)
		{
			PlaySounds.Landing_Strong.Play();
		}
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x0017CA1C File Offset: 0x0017AC1C
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

	// Token: 0x06002F58 RID: 12120 RVA: 0x00022F67 File Offset: 0x00021167
	public static void Play_Level_Failed_Popup()
	{
		if (PlaySounds.Level_Failed_Popup.clip != null)
		{
			PlaySounds.Level_Failed_Popup.Play();
		}
	}

	// Token: 0x06002F59 RID: 12121 RVA: 0x00022F85 File Offset: 0x00021185
	public static void Play_Level_Completed_Popup()
	{
		if (PlaySounds.Level_Completed_Popup.clip != null)
		{
			PlaySounds.Level_Completed_Popup.Play();
		}
	}

	// Token: 0x06002F5A RID: 12122 RVA: 0x00022FA3 File Offset: 0x000211A3
	public static void Play_CollectCoin()
	{
		if (PlaySounds.CollectCoin.clip != null)
		{
			PlaySounds.CollectCoin.Play();
		}
	}

	// Token: 0x06002F5B RID: 12123 RVA: 0x00022FC1 File Offset: 0x000211C1
	public static void Play_CollectCoin_2nd()
	{
		if (PlaySounds.CollectCoin_2nd.clip != null)
		{
			PlaySounds.CollectCoin_2nd.Play();
		}
	}

	// Token: 0x06002F5C RID: 12124 RVA: 0x00022FDF File Offset: 0x000211DF
	public static void Play_CollectCoin_3rd()
	{
		if (PlaySounds.CollectCoin_3rd.clip != null)
		{
			PlaySounds.CollectCoin_3rd.Play();
		}
	}

	// Token: 0x06002F5D RID: 12125 RVA: 0x00022FFD File Offset: 0x000211FD
	public static void Play_GetStar()
	{
		if (PlaySounds.GetStar.clip != null)
		{
			PlaySounds.GetStar.Play();
		}
	}

	// Token: 0x06002F5E RID: 12126 RVA: 0x0002301B File Offset: 0x0002121B
	public static void Play_GetStar2()
	{
		if (PlaySounds.GetStar2.clip != null)
		{
			PlaySounds.GetStar2.Play();
		}
	}

	// Token: 0x06002F5F RID: 12127 RVA: 0x00023039 File Offset: 0x00021239
	public static void Play_GetStar3()
	{
		if (PlaySounds.GetStar3.clip != null)
		{
			PlaySounds.GetStar3.Play();
		}
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x00023057 File Offset: 0x00021257
	public static void Play_CoinsSpent()
	{
		if (PlaySounds.CoinsSpent.clip != null)
		{
			PlaySounds.CoinsSpent.Play();
		}
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x00023075 File Offset: 0x00021275
	public static void Play_NoMoreCoins()
	{
		if (PlaySounds.NoMoreCoins.clip != null)
		{
			PlaySounds.NoMoreCoins.Play();
		}
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x00023093 File Offset: 0x00021293
	public static void Play_BiljkaUgriz()
	{
		if (PlaySounds.Biljka_Ugriz_NEW.clip != null)
		{
			PlaySounds.Biljka_Ugriz_NEW.Play();
		}
	}

	// Token: 0x06002F63 RID: 12131 RVA: 0x000230B1 File Offset: 0x000212B1
	public static void Play_CollectBanana()
	{
		if (PlaySounds.Collect_Banana_NEW.clip != null)
		{
			PlaySounds.Collect_Banana_NEW.Play();
		}
	}

	// Token: 0x06002F64 RID: 12132 RVA: 0x000230CF File Offset: 0x000212CF
	public static void Play_CollectDiamond()
	{
		if (PlaySounds.Collect_Diamond_NEW.clip != null)
		{
			PlaySounds.Collect_Diamond_NEW.Play();
		}
	}

	// Token: 0x06002F65 RID: 12133 RVA: 0x000230ED File Offset: 0x000212ED
	public static void Play_CollectPowerUp()
	{
		if (PlaySounds.Collect_PowerUp_NEW.clip != null)
		{
			PlaySounds.Collect_PowerUp_NEW.Play();
		}
	}

	// Token: 0x06002F66 RID: 12134 RVA: 0x0002310B File Offset: 0x0002130B
	public static void Play_Glide()
	{
		if (PlaySounds.Glide_NEW.clip != null)
		{
			PlaySounds.Glide_NEW.Play();
		}
	}

	// Token: 0x06002F67 RID: 12135 RVA: 0x00023129 File Offset: 0x00021329
	public static void Stop_Glide()
	{
		if (PlaySounds.Glide_NEW.clip != null)
		{
			PlaySounds.Glide_NEW.Stop();
		}
	}

	// Token: 0x06002F68 RID: 12136 RVA: 0x00023147 File Offset: 0x00021347
	public static void Play_OtkljucavanjeNivoa()
	{
		if (PlaySounds.OtkljucavanjeNivoa_NEW.clip != null)
		{
			PlaySounds.OtkljucavanjeNivoa_NEW.Play();
		}
	}

	// Token: 0x06002F69 RID: 12137 RVA: 0x00023165 File Offset: 0x00021365
	public static void Play_SmashGorilla()
	{
		if (PlaySounds.SmashGorilla_NEW.clip != null)
		{
			PlaySounds.SmashGorilla_NEW.Play();
		}
	}

	// Token: 0x06002F6A RID: 12138 RVA: 0x00023183 File Offset: 0x00021383
	public static void Play_Otvaranje_Kovcega()
	{
		if (PlaySounds.Otvaranje_Kovcega_NEW.clip != null)
		{
			PlaySounds.Otvaranje_Kovcega_NEW.Play();
		}
	}

	// Token: 0x06002F6B RID: 12139 RVA: 0x000231A1 File Offset: 0x000213A1
	public static void Play_Bure_Eksplozija()
	{
		if (PlaySounds.Bure_Eksplozija_NEW.clip != null)
		{
			PlaySounds.Bure_Eksplozija_NEW.Play();
		}
	}

	// Token: 0x06002F6C RID: 12140 RVA: 0x000231BF File Offset: 0x000213BF
	public static void Play_MushroomBounce()
	{
		if (PlaySounds.MushroomBounce.clip != null)
		{
			PlaySounds.MushroomBounce.Play();
		}
	}

	// Token: 0x06002F6D RID: 12141 RVA: 0x000231DD File Offset: 0x000213DD
	public static void Play_BackgroundMusic_Gameplay()
	{
		if (PlaySounds.BackgroundMusic_Gameplay.clip != null)
		{
			PlaySounds.BackgroundMusic_Gameplay.Play();
		}
	}

	// Token: 0x06002F6E RID: 12142 RVA: 0x000231FB File Offset: 0x000213FB
	public static void Play_BackgroundMusic_Menu()
	{
		if (PlaySounds.BackgroundMusic_Menu.clip != null)
		{
			PlaySounds.BackgroundMusic_Menu.Play();
		}
	}

	// Token: 0x06002F6F RID: 12143 RVA: 0x00023219 File Offset: 0x00021419
	public static void Stop_BackgroundMusic_Gameplay()
	{
		if (PlaySounds.BackgroundMusic_Gameplay.clip != null)
		{
			PlaySounds.BackgroundMusic_Gameplay.Stop();
		}
	}

	// Token: 0x06002F70 RID: 12144 RVA: 0x00023237 File Offset: 0x00021437
	public static void Stop_BackgroundMusic_Menu()
	{
		if (PlaySounds.BackgroundMusic_Menu.clip != null)
		{
			PlaySounds.BackgroundMusic_Menu.Stop();
		}
	}

	// Token: 0x06002F71 RID: 12145 RVA: 0x00023255 File Offset: 0x00021455
	public static void Stop_Level_Failed_Popup()
	{
		if (PlaySounds.Level_Failed_Popup.clip != null)
		{
			PlaySounds.Level_Failed_Popup.Stop();
		}
	}

	// Token: 0x06002F72 RID: 12146 RVA: 0x00023273 File Offset: 0x00021473
	public static void Play_Siljci()
	{
		if (PlaySounds.Siljci.clip != null)
		{
			PlaySounds.Siljci.Play();
		}
	}

	// Token: 0x06002F73 RID: 12147 RVA: 0x00023291 File Offset: 0x00021491
	public static void Play_TNTBure_Eksplozija()
	{
		if (PlaySounds.TNTBure_Eksplozija.clip != null)
		{
			PlaySounds.TNTBure_Eksplozija.Play();
		}
	}

	// Token: 0x06002F74 RID: 12148 RVA: 0x000232AF File Offset: 0x000214AF
	public static void Play_LooseShield()
	{
		if (PlaySounds.LooseShield.clip != null)
		{
			PlaySounds.LooseShield.Play();
		}
	}

	// Token: 0x06002F75 RID: 12149 RVA: 0x000232CD File Offset: 0x000214CD
	public static void Play_MajmunUtepan()
	{
		if (PlaySounds.MajmunUtepan.clip != null)
		{
			PlaySounds.MajmunUtepan.Play();
		}
	}

	// Token: 0x04002A77 RID: 10871
	private static AudioSource Button_MusicOn;

	// Token: 0x04002A78 RID: 10872
	private static AudioSource Button_SoundOn;

	// Token: 0x04002A79 RID: 10873
	private static AudioSource Button_Play;

	// Token: 0x04002A7A RID: 10874
	private static AudioSource Button_GoBack;

	// Token: 0x04002A7B RID: 10875
	private static AudioSource Button_OpenWorld;

	// Token: 0x04002A7C RID: 10876
	private static AudioSource Button_OpenLevel;

	// Token: 0x04002A7D RID: 10877
	private static AudioSource Button_LockedLevel_Click;

	// Token: 0x04002A7E RID: 10878
	private static AudioSource Button_Pause;

	// Token: 0x04002A7F RID: 10879
	private static AudioSource Button_NextLevel;

	// Token: 0x04002A80 RID: 10880
	private static AudioSource Button_RestartLevel;

	// Token: 0x04002A81 RID: 10881
	[HideInInspector]
	public static AudioSource Run;

	// Token: 0x04002A82 RID: 10882
	private static AudioSource Jump1;

	// Token: 0x04002A83 RID: 10883
	private static AudioSource Jump2;

	// Token: 0x04002A84 RID: 10884
	private static AudioSource Jump3;

	// Token: 0x04002A85 RID: 10885
	private static AudioSource VoiceJump1;

	// Token: 0x04002A86 RID: 10886
	private static AudioSource VoiceJump2;

	// Token: 0x04002A87 RID: 10887
	private static AudioSource VoiceJump3;

	// Token: 0x04002A88 RID: 10888
	private static AudioSource VoiceJump4;

	// Token: 0x04002A89 RID: 10889
	private static AudioSource VoiceJump5;

	// Token: 0x04002A8A RID: 10890
	private static AudioSource VoiceJump6;

	// Token: 0x04002A8B RID: 10891
	private static AudioSource VoiceJump7;

	// Token: 0x04002A8C RID: 10892
	private static AudioSource VoiceJump8;

	// Token: 0x04002A8D RID: 10893
	private static AudioSource Landing1;

	// Token: 0x04002A8E RID: 10894
	private static AudioSource Landing2;

	// Token: 0x04002A8F RID: 10895
	private static AudioSource Landing3;

	// Token: 0x04002A90 RID: 10896
	private static AudioSource Landing_Strong;

	// Token: 0x04002A91 RID: 10897
	private static AudioSource SmashBaboon;

	// Token: 0x04002A92 RID: 10898
	[HideInInspector]
	public static AudioSource Level_Failed_Popup;

	// Token: 0x04002A93 RID: 10899
	private static AudioSource Level_Completed_Popup;

	// Token: 0x04002A94 RID: 10900
	private static AudioSource CollectCoin;

	// Token: 0x04002A95 RID: 10901
	private static AudioSource CollectCoin_2nd;

	// Token: 0x04002A96 RID: 10902
	private static AudioSource CollectCoin_3rd;

	// Token: 0x04002A97 RID: 10903
	private static AudioSource GetStar;

	// Token: 0x04002A98 RID: 10904
	private static AudioSource GetStar2;

	// Token: 0x04002A99 RID: 10905
	private static AudioSource GetStar3;

	// Token: 0x04002A9A RID: 10906
	private static AudioSource CoinsSpent;

	// Token: 0x04002A9B RID: 10907
	private static AudioSource NoMoreCoins;

	// Token: 0x04002A9C RID: 10908
	private static AudioSource Biljka_Ugriz_NEW;

	// Token: 0x04002A9D RID: 10909
	private static AudioSource Collect_Banana_NEW;

	// Token: 0x04002A9E RID: 10910
	private static AudioSource Collect_Diamond_NEW;

	// Token: 0x04002A9F RID: 10911
	private static AudioSource Collect_PowerUp_NEW;

	// Token: 0x04002AA0 RID: 10912
	[HideInInspector]
	public static AudioSource Glide_NEW;

	// Token: 0x04002AA1 RID: 10913
	private static AudioSource OtkljucavanjeNivoa_NEW;

	// Token: 0x04002AA2 RID: 10914
	private static AudioSource SmashGorilla_NEW;

	// Token: 0x04002AA3 RID: 10915
	private static AudioSource Otvaranje_Kovcega_NEW;

	// Token: 0x04002AA4 RID: 10916
	private static AudioSource Bure_Eksplozija_NEW;

	// Token: 0x04002AA5 RID: 10917
	private static AudioSource MushroomBounce;

	// Token: 0x04002AA6 RID: 10918
	private static AudioSource Siljci;

	// Token: 0x04002AA7 RID: 10919
	private static AudioSource TNTBure_Eksplozija;

	// Token: 0x04002AA8 RID: 10920
	private static AudioSource LooseShield;

	// Token: 0x04002AA9 RID: 10921
	private static AudioSource MajmunUtepan;

	// Token: 0x04002AAA RID: 10922
	[HideInInspector]
	public static AudioSource BackgroundMusic_Gameplay;

	// Token: 0x04002AAB RID: 10923
	[HideInInspector]
	public static AudioSource BackgroundMusic_Menu;

	// Token: 0x04002AAC RID: 10924
	[HideInInspector]
	public static bool soundOn;

	// Token: 0x04002AAD RID: 10925
	[HideInInspector]
	public static bool musicOn;

	// Token: 0x04002AAE RID: 10926
	private static int zvukZaBabuna;
}
