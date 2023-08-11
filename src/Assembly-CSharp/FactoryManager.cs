using UnityEngine;

public class FactoryManager : MonoBehaviour
{
	public static FactoryManager inst;

	public NPCFactory npcFactory;

	public CreateNewPlayerFactory createNewPlayerFactory;

	public LoadPlayerDateFactory loadPlayerDateFactory;

	public SaveLoadFactory SaveLoadFactory;

	private void Awake()
	{
		inst = this;
	}

	private void Start()
	{
		npcFactory = new NPCFactory();
		createNewPlayerFactory = new CreateNewPlayerFactory();
		loadPlayerDateFactory = new LoadPlayerDateFactory();
		SaveLoadFactory = new SaveLoadFactory();
	}
}
