using UnityEngine;

namespace Fight;

public class FightUIMag : MonoBehaviour
{
	public static FightUIMag inst;

	private void Awake()
	{
		inst = this;
	}
}
