using Fungus;
using UnityEngine;

public class talkCompont : MonoBehaviour
{
	public Flowchart flowchat;

	private void Start()
	{
	}

	public void StartFight()
	{
		Tools.instance.startFight(flowchat.GetIntegerVariable("MonsterID"));
	}
}
