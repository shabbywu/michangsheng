using UnityEngine;

public class HeroDie : MonoBehaviour
{
	private void OnDestroy()
	{
		GameManager gameManager = (GameManager)(object)Object.FindObjectOfType(typeof(GameManager));
		if (Object.op_Implicit((Object)(object)gameManager))
		{
			gameManager.GameEvent("endgame");
		}
	}
}
