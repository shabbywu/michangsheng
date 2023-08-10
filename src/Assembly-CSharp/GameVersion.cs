using UnityEngine;

public class GameVersion : MonoBehaviour
{
	[SerializeField]
	private int gameVersion;

	public static GameVersion inst;

	public bool realTest;

	private void Awake()
	{
		inst = this;
	}

	private void Start()
	{
		realTest = false;
	}

	public int GetGameVersion()
	{
		return gameVersion;
	}
}
