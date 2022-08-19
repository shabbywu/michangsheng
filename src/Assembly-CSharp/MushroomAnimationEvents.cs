using System;
using UnityEngine;

// Token: 0x020004CA RID: 1226
public class MushroomAnimationEvents : MonoBehaviour
{
	// Token: 0x0600279F RID: 10143 RVA: 0x00128AE9 File Offset: 0x00126CE9
	private void Start()
	{
		this.playerController = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x00128B00 File Offset: 0x00126D00
	private void ReturnFromMushroom()
	{
		this.playerController.GetComponent<Rigidbody2D>().isKinematic = false;
		this.playerController.GetComponent<Rigidbody2D>().velocity = new Vector2(this.playerController.maxSpeedX, -10f);
		this.playerController.SlideNaDole = true;
		this.playerController.Glide = true;
	}

	// Token: 0x04002282 RID: 8834
	private MonkeyController2D playerController;
}
