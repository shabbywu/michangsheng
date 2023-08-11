using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
	public static string chosenLanguage = "_en";

	public static string Collect = "Collect";

	public static string LogIn = "Log In";

	public static string Free = "Free";

	public static string Coins = "Coins";

	public static string Music = "Music";

	public static string Sound = "Sound";

	public static string Language = "Language";

	public static string Settings = "Settings";

	public static string ResetProgress = "Reset Progress";

	public static string ResetTutorials = "Reset Tutorials";

	public static string LogOut = "Log Out";

	public static string Leaderboard = "Leaderboard";

	public static string InviteAndEarn = "Invite and earn";

	public static string InviteFriendsAndEarn = "Invite friends and earn";

	public static string FreeCoins = "Free Coins";

	public static string Shop = "Shop";

	public static string Customize = "Customize";

	public static string PowerUps = "Power-Ups";

	public static string WatchVideo = "Watch Video";

	public static string Banana = "Banana";

	public static string DoubleCoins = "Double Coins";

	public static string CoinsMagnet = "Coins Magnet";

	public static string Shield = "Shield";

	public static string New = "New";

	public static string Preview = "Preview";

	public static string Buy = "Buy";

	public static string Equip = "Equip";

	public static string Unequip = "Unequip";

	public static string ShareAndEarn = "Share and earn";

	public static string PostAndEarn = "Post and earn";

	public static string TweetAndEarn = "Tweet and earn";

	public static string LogInAndEarn = "Log In and earn";

	public static string Level = "Level";

	public static string Mission = "Mission";

	public static string Invite = "Invite";

	public static string BonusLevel = "Bonus Level";

	public static string Unlock = "Unlock";

	public static string No = "No";

	public static string Yes = "Yes";

	public static string NoVideo = "No videos available, please try again later";

	public static string NotEnoughBananas = "Not enough bananas";

	public static string Loading = "Loading";

	public static string Tip = "Tip";

	public static string Tips = "Tips";

	public static string ComingSoon = "Coming Soon";

	public static string TapScreenToStart = "Tap Screen To Start!";

	public static string Pause = "Pause";

	public static string KeepPlaying = "Keep Playing";

	public static string LevelFailed = "Level Failed";

	public static string LevelCompleted = "Level Completed";

	public static string RateThisGame = "Rate this game";

	public static string DoYouLikeOurGame = "Do you like our game?";

	public static string Cancel = "Cancel";

	public static string Rate = "Rate";

	public static string Congratulations = "Congratulations!";

	public static string NewLevelsComingSoon = "New levels coming soon!";

	public static string TutorialTapJump = "1-TAP anywhere to JUMP\n2-TAP and HOLD to JUMP HIGHER";

	public static string TutorialGlide = "When in the AIR you can TAP and HOLD to GLIDE slowly to the ground";

	public static string TutorialSwipe = "SWIPE DOWN at great heights to perform a deadly super drop!";

	public static string NoInternet = "No internet";

	public static string CheckInternet = "Check your internet connection";

	public static string HowWouldYouRate = "How would you rate our game?";

	public static string Downloading = "Downloading...";

	public static string Maintenance = "Maintenance";

	public static string BeBackSoon = "We'll be back soon";

	public static string Ok = "OK";

	public static string Reward = "Reward";

	public static string DailyReward = "Daily Reward";

	public static string Day = "Day";

	public static string Play = "Play";

	public static string BananaIsland = "Banana Island";

	public static string SavannaIsland = "Savanna Island";

	public static string JungleIsland = "Jungle Island";

	public static string TempleIsland = "Temple Island";

	public static string VolcanoIsland = "Volcano Island";

	public static string Completed = "Completed";

	public static string Share = "Share";

	public static string FollowUsOnFacebook = "Follow us on Facebook";

	public static string LoadingTip1 = "Don't miss Tips and Tricks in the Loading Screen, they can be useful.";

	public static string LoadingTip2 = "Want to keep playing? Spend one of your bananas to continue running.";

	public static string LoadingTip3 = "Jump on enemies' heads to knock'em out.";

	public static string FrozenIsland = "Frozen Island";

	public static bool hasWhiteSpaces = true;

	private void Awake()
	{
		((Object)((Component)this).transform).name = "LanguageManager";
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		RefreshTexts();
		if (PlayerPrefs.HasKey("choosenLanguage"))
		{
			chosenLanguage = PlayerPrefs.GetString("choosenLanguage");
		}
		Debug.Log((object)("Chosen Languafe iz Language Mangaer: " + chosenLanguage));
	}

	public static void RefreshTexts()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		IEnumerable<XElement> source = ((XContainer)XElement.Parse(((object)(TextAsset)Resources.Load("xmls/inGameText/Language" + chosenLanguage)).ToString())).Elements();
		int num = source.Count();
		Debug.Log((object)("Ukupno ima " + num + " xml elemenata"));
		Collect = source.ElementAt(0).Value;
		LogIn = source.ElementAt(1).Value;
		Free = source.ElementAt(2).Value;
		Coins = source.ElementAt(3).Value;
		Music = source.ElementAt(4).Value;
		Sound = source.ElementAt(5).Value;
		Language = source.ElementAt(6).Value;
		Settings = source.ElementAt(7).Value;
		ResetProgress = source.ElementAt(8).Value;
		ResetTutorials = source.ElementAt(9).Value;
		LogOut = source.ElementAt(10).Value;
		Leaderboard = source.ElementAt(11).Value;
		InviteAndEarn = source.ElementAt(12).Value;
		InviteFriendsAndEarn = source.ElementAt(13).Value;
		FreeCoins = source.ElementAt(14).Value;
		Shop = source.ElementAt(15).Value;
		Customize = source.ElementAt(16).Value;
		PowerUps = source.ElementAt(17).Value;
		WatchVideo = source.ElementAt(18).Value;
		Banana = source.ElementAt(19).Value;
		DoubleCoins = source.ElementAt(20).Value;
		CoinsMagnet = source.ElementAt(21).Value;
		Shield = source.ElementAt(22).Value;
		New = source.ElementAt(23).Value;
		Preview = source.ElementAt(24).Value;
		Buy = source.ElementAt(25).Value;
		Equip = source.ElementAt(26).Value;
		Unequip = source.ElementAt(27).Value;
		ShareAndEarn = source.ElementAt(28).Value;
		PostAndEarn = source.ElementAt(29).Value;
		TweetAndEarn = source.ElementAt(30).Value;
		LogInAndEarn = source.ElementAt(31).Value;
		Level = source.ElementAt(32).Value;
		Mission = source.ElementAt(33).Value;
		Invite = source.ElementAt(34).Value;
		BonusLevel = source.ElementAt(35).Value;
		Unlock = source.ElementAt(36).Value;
		No = source.ElementAt(37).Value;
		Yes = source.ElementAt(38).Value;
		NoVideo = source.ElementAt(39).Value;
		NotEnoughBananas = source.ElementAt(40).Value;
		Loading = source.ElementAt(41).Value;
		Tip = source.ElementAt(42).Value;
		Tips = source.ElementAt(43).Value;
		ComingSoon = source.ElementAt(44).Value;
		TapScreenToStart = source.ElementAt(45).Value;
		Pause = source.ElementAt(46).Value;
		KeepPlaying = source.ElementAt(47).Value;
		LevelFailed = source.ElementAt(48).Value;
		LevelCompleted = source.ElementAt(49).Value;
		RateThisGame = source.ElementAt(50).Value;
		DoYouLikeOurGame = source.ElementAt(51).Value;
		Cancel = source.ElementAt(52).Value;
		Rate = source.ElementAt(53).Value;
		Congratulations = source.ElementAt(54).Value;
		NewLevelsComingSoon = source.ElementAt(55).Value;
		TutorialTapJump = source.ElementAt(56).Value;
		TutorialGlide = source.ElementAt(57).Value;
		TutorialSwipe = source.ElementAt(58).Value;
		NoInternet = source.ElementAt(59).Value;
		CheckInternet = source.ElementAt(60).Value;
		HowWouldYouRate = source.ElementAt(61).Value;
		Downloading = source.ElementAt(62).Value;
		Maintenance = source.ElementAt(63).Value;
		BeBackSoon = source.ElementAt(64).Value;
		Ok = source.ElementAt(65).Value;
		Reward = source.ElementAt(66).Value;
		DailyReward = source.ElementAt(67).Value;
		Day = source.ElementAt(68).Value;
		Play = source.ElementAt(69).Value;
		BananaIsland = source.ElementAt(70).Value;
		SavannaIsland = source.ElementAt(71).Value;
		JungleIsland = source.ElementAt(72).Value;
		TempleIsland = source.ElementAt(73).Value;
		VolcanoIsland = source.ElementAt(74).Value;
		Completed = source.ElementAt(75).Value;
		Share = source.ElementAt(76).Value;
		FollowUsOnFacebook = source.ElementAt(77).Value;
		LoadingTip1 = source.ElementAt(78).Value;
		LoadingTip2 = source.ElementAt(79).Value;
		LoadingTip3 = source.ElementAt(80).Value;
		FrozenIsland = source.ElementAt(83).Value;
	}

	public string SplitTextIntoRows(string originalText, int rowLimit)
	{
		string text;
		string text2 = (text = "");
		originalText = originalText.Replace("\n", " ");
		if (originalText.Length < rowLimit)
		{
			return originalText;
		}
		bool flag = true;
		string[] array = originalText.Split(new char[1] { ' ' });
		if (chosenLanguage == "_jp" || chosenLanguage == "_ch")
		{
			array = new string[originalText.Length];
			for (int i = 0; i < originalText.Length; i++)
			{
				array[i] = originalText[i].ToString();
			}
		}
		int num = 0;
		while (num < array.Length)
		{
			if (array.Length <= num)
			{
				continue;
			}
			if (array[num].Length >= rowLimit)
			{
				text2 = text2 + array[num++] + ((chosenLanguage != "_jp" && chosenLanguage != "_ch") ? " " : "");
			}
			else
			{
				while (text2.Length + array[num].Length < rowLimit && num < array.Length)
				{
					text2 = text2 + array[num++] + ((chosenLanguage != "_jp" && chosenLanguage != "_ch") ? " " : "");
					if (num >= array.Length)
					{
						break;
					}
				}
			}
			if (flag)
			{
				text = text2;
				flag = false;
			}
			else
			{
				text = text + "\n" + text2;
			}
			text2 = "";
		}
		return text;
	}

	private string SplitInHalf(string toSplit)
	{
		return toSplit;
	}

	public IEnumerator helpfunk(TextMesh toAdjust)
	{
		string text = toAdjust.text;
		Bounds bounds;
		while (true)
		{
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float num = ((Bounds)(ref bounds)).size.x * 1.1f;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (!(num < ((Bounds)(ref bounds)).size.x))
			{
				break;
			}
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float num2 = ((Bounds)(ref bounds)).size.y * 1.1f;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (num2 < ((Bounds)(ref bounds)).size.y)
			{
				toAdjust.characterSize *= 1.1f;
				continue;
			}
			break;
		}
		while (true)
		{
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float num3 = ((Bounds)(ref bounds)).size.x * 0.9f;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (!(num3 > ((Bounds)(ref bounds)).size.x))
			{
				break;
			}
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float num4 = ((Bounds)(ref bounds)).size.y * 0.9f;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (!(num4 > ((Bounds)(ref bounds)).size.y))
			{
				break;
			}
			toAdjust.characterSize *= 0.9f;
		}
		string[] wholeText = text.Split(new char[1] { ' ' });
		if (chosenLanguage == "_jp" || chosenLanguage == "_ch")
		{
			char[] array = text.ToCharArray();
			wholeText = new string[array.Length];
			for (int i = 0; i < wholeText.Length; i++)
			{
				wholeText[i] = array[i].ToString();
			}
		}
		float charSize = toAdjust.characterSize;
		bool again;
		float y;
		do
		{
			again = false;
			toAdjust.characterSize = charSize;
			int counter = 1;
			toAdjust.text = wholeText[0];
			string previous = toAdjust.text;
			string helpText = "";
			while (counter < wholeText.Length)
			{
				int helpCounter = 0;
				while (true)
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float x = ((Bounds)(ref bounds)).size.x;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if ((!(x < ((Bounds)(ref bounds)).size.x) && !(toAdjust.text == wholeText[(counter < wholeText.Length) ? counter : (wholeText.Length - 1)])) || counter >= wholeText.Length)
					{
						break;
					}
					previous = toAdjust.text;
					toAdjust.text = toAdjust.text + ((chosenLanguage == "_jp" || chosenLanguage == "_ch") ? "" : " ") + wholeText[counter];
					counter++;
					helpCounter++;
					yield return null;
					yield return (object)new WaitForSeconds(0.1f);
				}
				if (helpCounter == 0)
				{
					Debug.Log((object)"again");
					again = true;
					break;
				}
				if (counter < wholeText.Length)
				{
					toAdjust.text = wholeText[counter - 1];
					Debug.Log((object)(counter + "<" + wholeText.Length));
					helpText = helpText + previous + "\n";
				}
				else
				{
					helpText += toAdjust.text;
					Debug.Log((object)(counter + ">=" + wholeText.Length));
				}
				Debug.Log((object)(wholeText.Length + "##" + counter));
				yield return null;
				yield return (object)new WaitForSeconds(0.1f);
			}
			toAdjust.text = helpText;
			charSize *= 0.9f;
			Debug.Log((object)"enddd");
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			y = ((Bounds)(ref bounds)).size.y;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
		}
		while (y > ((Bounds)(ref bounds)).size.y || again);
	}

	public void AdjustFontSize(TextMesh toAdjust, bool rowSplit)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		string text = toAdjust.text;
		Bounds bounds;
		while (true)
		{
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float num = ((Bounds)(ref bounds)).size.x * 1.1f;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (!(num < ((Bounds)(ref bounds)).size.x))
			{
				break;
			}
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float num2 = ((Bounds)(ref bounds)).size.y * 1.1f;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (!(num2 < ((Bounds)(ref bounds)).size.y))
			{
				break;
			}
			toAdjust.characterSize *= 1.1f;
		}
		if (!rowSplit)
		{
			while (true)
			{
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float x = ((Bounds)(ref bounds)).size.x;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (x > ((Bounds)(ref bounds)).size.x)
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float y = ((Bounds)(ref bounds)).size.y;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if (y > ((Bounds)(ref bounds)).size.y)
					{
						toAdjust.characterSize *= 0.9f;
						continue;
					}
					break;
				}
				break;
			}
			return;
		}
		string[] array = text.Split(new char[1] { ' ' });
		if (chosenLanguage == "_jp" || chosenLanguage == "_ch")
		{
			char[] array2 = text.ToCharArray();
			array = new string[array2.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array2[i].ToString();
			}
		}
		bool flag = false;
		float num3 = toAdjust.characterSize;
		int num6;
		do
		{
			flag = false;
			toAdjust.characterSize = num3;
			int num4 = 1;
			toAdjust.text = array[0];
			string text2 = toAdjust.text;
			string text3 = "";
			while (num4 < array.Length)
			{
				int num5 = 0;
				while (true)
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float x2 = ((Bounds)(ref bounds)).size.x;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if ((!(x2 < ((Bounds)(ref bounds)).size.x) && !(toAdjust.text == array[(num4 < array.Length) ? num4 : (array.Length - 1)])) || num4 >= array.Length)
					{
						break;
					}
					text2 = toAdjust.text;
					toAdjust.text = toAdjust.text + ((chosenLanguage == "_jp" || chosenLanguage == "_ch") ? "" : " ") + array[num4];
					num4++;
					num5++;
				}
				if (num5 == 0)
				{
					Debug.Log((object)"again");
					flag = true;
					break;
				}
				if (num4 < array.Length)
				{
					toAdjust.text = array[num4 - 1];
					Debug.Log((object)(num4 + "<" + array.Length));
					text3 = text3 + text2 + "\n";
				}
				else
				{
					text3 += toAdjust.text;
					Debug.Log((object)(num4 + ">=" + array.Length));
				}
				Debug.Log((object)(array.Length + "##" + num4));
			}
			toAdjust.text = text3;
			num3 *= 0.9f;
			Debug.Log((object)"enddd");
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float y2 = ((Bounds)(ref bounds)).size.y;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (!(y2 > ((Bounds)(ref bounds)).size.y))
			{
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float x3 = ((Bounds)(ref bounds)).size.x;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				num6 = ((x3 > ((Bounds)(ref bounds)).size.x) ? 1 : 0);
			}
			else
			{
				num6 = 1;
			}
		}
		while (((uint)num6 | (flag ? 1u : 0u)) != 0);
		string[] array3 = new string[5];
		bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
		float y3 = ((Bounds)(ref bounds)).size.y;
		bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
		array3[0] = (y3 > ((Bounds)(ref bounds)).size.y).ToString();
		array3[1] = "###";
		bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
		float x4 = ((Bounds)(ref bounds)).size.x;
		bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
		array3[2] = (x4 > ((Bounds)(ref bounds)).size.x).ToString();
		array3[3] = "###";
		array3[4] = flag.ToString();
		Debug.Log((object)string.Concat(array3));
	}
}
