namespace UltimateSurvival;

public class ET
{
	public enum ActionRepeatType
	{
		Single,
		Repetitive
	}

	public enum PointOrder
	{
		Sequenced,
		Random
	}

	public enum AIMovementState
	{
		Idle,
		Walking,
		Running
	}

	public enum BuildableType
	{
		Foundation,
		Wall,
		Floor
	}

	public enum MaterialType
	{
		Wood,
		Stone,
		Metal
	}

	public enum InputType
	{
		Standalone,
		Mobile
	}

	public enum InputMode
	{
		Buttons,
		Axes
	}

	public enum StandaloneAxisType
	{
		Unity,
		Custom
	}

	public enum MobileAxisType
	{
		Custom
	}

	public enum ButtonState
	{
		Down,
		Up
	}

	public enum CharacterType
	{
		Player
	}

	public enum FireMode
	{
		SemiAuto,
		Burst,
		FullAuto
	}

	public enum FileCreatorMode
	{
		ScriptableObject,
		ScriptFile,
		Both
	}

	public enum TimeOfDay
	{
		Day,
		Night
	}

	public enum InventoryState
	{
		Closed = 0,
		Normal = 1,
		Loot = 2,
		Furnace = 3,
		Anvil = 4,
		Campfire = 6
	}
}
