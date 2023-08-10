public class OrderController
{
	private CharacterType biggest;

	private CharacterType currentAuthority;

	private static OrderController instance;

	public static OrderController Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new OrderController();
			}
			return instance;
		}
	}

	public CharacterType Type => currentAuthority;

	public CharacterType Biggest
	{
		get
		{
			return biggest;
		}
		set
		{
			biggest = value;
		}
	}

	public event CardEvent smartCard;

	public event CardEvent activeButton;

	private OrderController()
	{
		currentAuthority = CharacterType.Desk;
	}

	public void Init(CharacterType type)
	{
		currentAuthority = type;
		Biggest = type;
		if (currentAuthority == CharacterType.Player)
		{
			this.activeButton(arg: false);
		}
		else
		{
			this.smartCard(arg: true);
		}
	}

	public void Turn()
	{
		currentAuthority++;
		if (currentAuthority == CharacterType.Desk)
		{
			currentAuthority = CharacterType.Player;
		}
		if (currentAuthority == CharacterType.ComputerOne || currentAuthority == CharacterType.ComputerTwo)
		{
			this.smartCard(biggest == currentAuthority);
		}
		else if (currentAuthority == CharacterType.Player)
		{
			this.activeButton(biggest != currentAuthority);
		}
	}

	public void ResetButton()
	{
		this.activeButton = null;
	}

	public void ResetSmartCard()
	{
		this.smartCard = null;
	}
}
