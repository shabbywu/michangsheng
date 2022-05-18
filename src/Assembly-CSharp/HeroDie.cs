using System;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class HeroDie : MonoBehaviour
{
	// Token: 0x06000C56 RID: 3158 RVA: 0x00097150 File Offset: 0x00095350
	private void OnDestroy()
	{
		GameManager gameManager = (GameManager)Object.FindObjectOfType(typeof(GameManager));
		if (gameManager)
		{
			gameManager.GameEvent("endgame");
		}
	}
}
