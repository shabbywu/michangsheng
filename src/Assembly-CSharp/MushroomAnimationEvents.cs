using System;
using UnityEngine;

// Token: 0x0200072E RID: 1838
public class MushroomAnimationEvents : MonoBehaviour
{
	// Token: 0x06002EA7 RID: 11943 RVA: 0x00022A6F File Offset: 0x00020C6F
	private void Start()
	{
		this.playerController = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x00174384 File Offset: 0x00172584
	private void ReturnFromMushroom()
	{
		this.playerController.GetComponent<Rigidbody2D>().isKinematic = false;
		this.playerController.GetComponent<Rigidbody2D>().velocity = new Vector2(this.playerController.maxSpeedX, -10f);
		this.playerController.SlideNaDole = true;
		this.playerController.Glide = true;
	}

	// Token: 0x040029C9 RID: 10697
	private MonkeyController2D playerController;
}
