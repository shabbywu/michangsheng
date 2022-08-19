using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class HeroDie : MonoBehaviour
{
	// Token: 0x06000B67 RID: 2919 RVA: 0x00045620 File Offset: 0x00043820
	private void OnDestroy()
	{
		GameManager gameManager = (GameManager)Object.FindObjectOfType(typeof(GameManager));
		if (gameManager)
		{
			gameManager.GameEvent("endgame");
		}
	}
}
