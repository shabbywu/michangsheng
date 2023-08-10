using System.Collections;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public int basePointPerMatch;

	private int multiples;

	public int Multiples
	{
		get
		{
			return multiples;
		}
		set
		{
			multiples *= value;
		}
	}

	private void Start()
	{
		multiples = 1;
		basePointPerMatch = 100;
		InitMenu();
	}

	public void InitMenu()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		GameObject obj = NGUITools.AddChild(((Component)UICamera.mainCamera).gameObject, (GameObject)Resources.Load("StartPanel"));
		obj.AddComponent<Menu>();
		((Component)obj.transform.Find("NoticeLabel")).gameObject.SetActive(false);
	}

	public void InitInteraction()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		GameObject obj = NGUITools.AddChild(((Component)UICamera.mainCamera).gameObject, (GameObject)Resources.Load("InteractionPanel"));
		((Object)obj).name = ((Object)obj).name.Replace("(Clone)", "");
		obj.AddComponent<Interaction>();
	}

	public void InitScene()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		string text = "";
		text = (((int)Application.platform != 11) ? Application.dataPath : Application.persistentDataPath);
		FileInfo fileInfo = new FileInfo(text + "\\data.json");
		GameObject obj = NGUITools.AddChild(((Component)UICamera.mainCamera).gameObject, (GameObject)Resources.Load("BackgroundPanel"));
		((Object)obj).name = ((Object)obj).name.Replace("(Clone)", "");
		GameObject obj2 = NGUITools.AddChild(((Component)UICamera.mainCamera).gameObject, (GameObject)Resources.Load("ScenePanel"));
		((Object)obj2).name = ((Object)obj2).name.Replace("(Clone)", "");
		GameObject gameObject = ((Component)obj2.transform.Find("Player")).gameObject;
		HandCards handCards = gameObject.AddComponent<HandCards>();
		handCards.cType = CharacterType.Player;
		gameObject.AddComponent<PlayCard>();
		GameObject gameObject2 = ((Component)obj2.transform.Find("ComputerOne")).gameObject;
		HandCards handCards2 = gameObject2.AddComponent<HandCards>();
		handCards2.cType = CharacterType.ComputerOne;
		gameObject2.AddComponent<SimpleSmartCard>();
		((Component)gameObject2.transform.Find("ComputerNotice")).gameObject.SetActive(false);
		GameObject gameObject3 = ((Component)obj2.transform.Find("ComputerTwo")).gameObject;
		HandCards handCards3 = gameObject3.AddComponent<HandCards>();
		handCards3.cType = CharacterType.ComputerTwo;
		gameObject3.AddComponent<SimpleSmartCard>();
		((Component)gameObject3.transform.Find("ComputerNotice")).gameObject.SetActive(false);
		((Component)((Component)obj2.transform.Find("Desk")).gameObject.transform.Find("NoticeLabel")).gameObject.SetActive(false);
		if (!fileInfo.Exists)
		{
			handCards.Integration = 1000;
			handCards2.Integration = 1000;
			handCards3.Integration = 1000;
		}
		else
		{
			GameData dataWithoutBOM = GetDataWithoutBOM(text);
			handCards.Integration = dataWithoutBOM.playerIntegration;
			handCards2.Integration = dataWithoutBOM.computerOneIntegration;
			handCards3.Integration = dataWithoutBOM.computerTwoIntegration;
		}
		UpdateIntegration(CharacterType.Player);
		UpdateIntegration(CharacterType.ComputerOne);
		UpdateIntegration(CharacterType.ComputerTwo);
	}

	public void DealCards()
	{
		Deck.Instance.Shuffle();
		CharacterType characterType = CharacterType.Player;
		for (int i = 0; i < 51; i++)
		{
			if (characterType == CharacterType.Desk)
			{
				characterType = CharacterType.Player;
			}
			DealTo(characterType);
			characterType++;
		}
		for (int j = 0; j < 3; j++)
		{
			DealTo(CharacterType.Desk);
		}
		for (int k = 1; k < 5; k++)
		{
			MakeHandCardsSprite((CharacterType)k, isSelected: false);
		}
	}

	public void GameOver()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		Identity accessIdentity = GameObject.Find(OrderController.Instance.Type.ToString()).GetComponent<HandCards>().AccessIdentity;
		for (int i = 1; i < 4; i++)
		{
			StatisticalIntegral((CharacterType)i, accessIdentity);
		}
		GameObject val = NGUITools.AddChild(((Component)UICamera.mainCamera).gameObject, (GameObject)Resources.Load("GameOverPanel"));
		val.AddComponent<Restart>();
		int integration = GameObject.Find(CharacterType.Player.ToString()).GetComponent<HandCards>().Integration;
		int integration2 = GameObject.Find(CharacterType.ComputerOne.ToString()).GetComponent<HandCards>().Integration;
		int integration3 = GameObject.Find(CharacterType.ComputerTwo.ToString()).GetComponent<HandCards>().Integration;
		if (integration > 0)
		{
			val.GetComponent<Restart>().SetTimeToNext(3f);
			if (integration2 <= 0)
			{
				((MonoBehaviour)this).StartCoroutine(ChangeComputer(CharacterType.ComputerOne));
			}
			if (integration3 <= 0)
			{
				((MonoBehaviour)this).StartCoroutine(ChangeComputer(CharacterType.ComputerTwo));
			}
			DisplayOverInfo(enough: true, val, accessIdentity);
			GameData data = new GameData
			{
				playerIntegration = ((integration > 0) ? integration : 1000),
				computerOneIntegration = ((integration2 > 0) ? integration2 : 1000),
				computerTwoIntegration = ((integration3 > 0) ? integration3 : 1000)
			};
			SaveDataWithUTF8(data);
		}
		else
		{
			DisplayOverInfo(enough: false, val, accessIdentity);
			GameData data2 = new GameData
			{
				playerIntegration = 1000,
				computerOneIntegration = 1000,
				computerTwoIntegration = 1000
			};
			SaveDataWithUTF8(data2);
		}
	}

	private void StatisticalIntegral(CharacterType type, Identity winner)
	{
		HandCards component = GameObject.Find(type.ToString()).GetComponent<HandCards>();
		int num = component.Multiples * Multiples * basePointPerMatch * (int)(component.AccessIdentity + 1);
		if (component.AccessIdentity != winner)
		{
			num = -num;
		}
		component.Integration += num;
		UpdateIntegration(type);
	}

	private IEnumerator ChangeComputer(CharacterType type)
	{
		DisplayDeskNotice(show: true);
		yield return (object)new WaitForSeconds(3f);
		Object.Destroy((Object)(object)GameObject.Find(type.ToString()));
		MatchNewComputer(type);
	}

	private void MatchNewComputer(CharacterType type)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		BackToDeck();
		DestroyAllSprites();
		DeskCardsCache.Instance.Clear();
		OrderController.Instance.ResetSmartCard();
		DisplayDeskNotice(show: false);
		GameObject val = NGUITools.AddChild(GameObject.Find("ScenePanel"), (GameObject)Resources.Load(type.ToString()));
		((Object)val).name = ((Object)val).name.Replace("(Clone)", "");
		val.AddComponent<HandCards>().cType = type;
		val.AddComponent<HandCards>().Integration = 1000;
		((Component)val.transform.Find("ComputerNotice")).gameObject.SetActive(false);
		val.AddComponent<SimpleSmartCard>();
		if (Random.Range(1, 3) == 1)
		{
			((Component)val.transform.Find("HeadPortrait")).gameObject.GetComponent<UISprite>().spriteName = "role1";
		}
		else
		{
			((Component)val.transform.Find("HeadPortrait")).gameObject.GetComponent<UISprite>().spriteName = "role";
		}
	}

	private void SaveDataWithUTF8(GameData data)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		string text = "";
		text = (((int)Application.platform != 11) ? Application.dataPath : Application.persistentDataPath);
		FileStream fileStream = new FileStream(text + "\\data.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
		new XmlSerializer(data.GetType()).Serialize(streamWriter, data);
		streamWriter.Close();
		fileStream.Close();
	}

	private GameData GetDataWithoutBOM(string fileName)
	{
		GameData gameData = new GameData();
		Stream stream = new FileStream(fileName + "\\data.json", FileMode.Open, FileAccess.Read, FileShare.None);
		StreamReader streamReader = new StreamReader(stream, detectEncodingFromByteOrderMarks: true);
		GameData result = new XmlSerializer(gameData.GetType()).Deserialize(streamReader) as GameData;
		streamReader.Close();
		stream.Close();
		return result;
	}

	public void BackToDeck()
	{
		HandCards[] array = new HandCards[3]
		{
			GameObject.Find("Player").GetComponent<HandCards>(),
			GameObject.Find("ComputerOne").GetComponent<HandCards>(),
			GameObject.Find("ComputerTwo").GetComponent<HandCards>()
		};
		for (int i = 0; i < array.Length; i++)
		{
			while (array[i].CardsCount != 0)
			{
				Card card = array[i][array[i].CardsCount - 1];
				Deck.Instance.AddCard(card);
				array[i].PopCard(card);
			}
		}
	}

	public void DestroyAllSprites()
	{
		CardSprite[][] array = new CardSprite[3][]
		{
			GameObject.Find("Player").GetComponentsInChildren<CardSprite>(),
			GameObject.Find("ComputerOne").GetComponentsInChildren<CardSprite>(),
			GameObject.Find("ComputerTwo").GetComponentsInChildren<CardSprite>()
		};
		for (int i = 0; i < array.GetLength(0); i++)
		{
			for (int j = 0; j < array[i].Length; j++)
			{
				array[i][j].Destroy();
			}
		}
	}

	public void CardsOnTable(CharacterType type)
	{
		HandCards component = GameObject.Find(type.ToString()).GetComponent<HandCards>();
		component.Multiples = 2;
		CardSprite[] componentsInChildren = GameObject.Find("Desk").GetComponentsInChildren<CardSprite>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Destroy();
		}
		while (DeskCardsCache.Instance.CardsCount != 0)
		{
			Card card = DeskCardsCache.Instance.Deal();
			component.AddCard(card);
		}
		MakeHandCardsSprite(type, isSelected: true);
		UpdateIndentity(type, Identity.Landlord);
	}

	private void DealTo(CharacterType person)
	{
		if (person == CharacterType.Desk)
		{
			Card card = Deck.Instance.Deal();
			DeskCardsCache.Instance.AddCard(card);
		}
		else
		{
			HandCards component = GameObject.Find(person.ToString()).GetComponent<HandCards>();
			Card card2 = Deck.Instance.Deal();
			component.AddCard(card2);
		}
	}

	private void MakeSprite(CharacterType type, Card card, bool selected)
	{
		if (!card.isSprite)
		{
			Object obj = Resources.Load("poker");
			GameObject prefab = (GameObject)(object)((obj is GameObject) ? obj : null);
			CardSprite component = NGUITools.AddChild(GameObject.Find(type.ToString()), prefab).gameObject.GetComponent<CardSprite>();
			component.Poker = card;
			component.Select = selected;
		}
	}

	private void MakeHandCardsSprite(CharacterType type, bool isSelected)
	{
		if (type == CharacterType.Desk)
		{
			DeskCardsCache instance = DeskCardsCache.Instance;
			for (int i = 0; i < instance.CardsCount; i++)
			{
				MakeSprite(type, instance[i], isSelected);
			}
		}
		else
		{
			HandCards component = GameObject.Find(type.ToString()).GetComponent<HandCards>();
			component.Sort();
			for (int j = 0; j < component.CardsCount; j++)
			{
				if (!component[j].isSprite)
				{
					MakeSprite(type, component[j], isSelected);
				}
			}
			UpdateLeftCardsCount(component.cType, component.CardsCount);
		}
		AdjustCardSpritsPosition(type);
	}

	public static void AdjustCardSpritsPosition(CharacterType type)
	{
		if (type == CharacterType.Desk)
		{
			DeskCardsCache instance = DeskCardsCache.Instance;
			CardSprite[] componentsInChildren = GameObject.Find(type.ToString()).GetComponentsInChildren<CardSprite>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					if (componentsInChildren[j].Poker == instance[i])
					{
						componentsInChildren[j].GoToPosition(GameObject.Find(type.ToString()), i);
					}
				}
			}
			return;
		}
		HandCards component = GameObject.Find(type.ToString()).GetComponent<HandCards>();
		CardSprite[] componentsInChildren2 = GameObject.Find(type.ToString()).GetComponentsInChildren<CardSprite>();
		for (int k = 0; k < component.CardsCount; k++)
		{
			for (int l = 0; l < componentsInChildren2.Length; l++)
			{
				if (componentsInChildren2[l].Poker == component[k])
				{
					componentsInChildren2[l].GoToPosition(GameObject.Find(type.ToString()), k);
				}
			}
		}
	}

	public static int GetWeight(Card[] cards, CardsType rule)
	{
		int num = 0;
		if (rule == CardsType.ThreeAndOne || rule == CardsType.ThreeAndTwo)
		{
			for (int i = 0; i < cards.Length; i++)
			{
				if (i < cards.Length - 2 && cards[i].GetCardWeight == cards[i + 1].GetCardWeight && cards[i].GetCardWeight == cards[i + 2].GetCardWeight)
				{
					num = (int)(num + cards[i].GetCardWeight);
					num *= 3;
					break;
				}
			}
		}
		else
		{
			for (int j = 0; j < cards.Length; j++)
			{
				num = (int)(num + cards[j].GetCardWeight);
			}
		}
		return num;
	}

	public static void UpdateLeftCardsCount(CharacterType type, int cardsCount)
	{
		((Component)GameObject.Find(type.ToString()).transform.Find("LeftPoker")).gameObject.GetComponent<UILabel>().text = "剩余扑克:" + cardsCount;
	}

	public static void UpdateIndentity(CharacterType type, Identity identity)
	{
		GameObject gameObject = ((Component)GameObject.Find(type.ToString()).transform.Find("Identity")).gameObject;
		GameObject.Find(type.ToString()).GetComponent<HandCards>().AccessIdentity = identity;
		gameObject.GetComponent<UISprite>().spriteName = "Identity_" + identity;
	}

	public static void UpdateIntegration(CharacterType type)
	{
		int integration = GameObject.Find(type.ToString()).GetComponent<HandCards>().Integration;
		((Component)GameObject.Find(type.ToString()).transform.Find("IntegrationLabel")).gameObject.GetComponent<UILabel>().text = "积分:" + integration;
	}

	public static void DisplayDeskNotice(bool show)
	{
		((Component)GameObject.Find("Desk").transform.Find("NoticeLabel")).gameObject.SetActive(show);
	}

	public static void DisplayOverInfo(bool enough, GameObject gameovePanel, Identity winner)
	{
		if (enough)
		{
			((Component)gameovePanel.transform.Find("Button")).gameObject.SetActive(false);
			gameovePanel.GetComponent<Restart>().SetTimeToNext(3f);
		}
		if (winner == Identity.Farmer)
		{
			gameovePanel.GetComponentInChildren<UISprite>().spriteName = "role1";
			gameovePanel.GetComponentInChildren<UILabel>().text = "农民获得胜利";
		}
		else
		{
			gameovePanel.GetComponentInChildren<UISprite>().spriteName = "role";
			gameovePanel.GetComponentInChildren<UILabel>().text = "地主获得胜利";
		}
	}
}
