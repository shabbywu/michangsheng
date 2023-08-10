using UnityEngine;

public class MushroomAnimationEvents : MonoBehaviour
{
	private MonkeyController2D playerController;

	private void Start()
	{
		playerController = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
	}

	private void ReturnFromMushroom()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		((Component)playerController).GetComponent<Rigidbody2D>().isKinematic = false;
		((Component)playerController).GetComponent<Rigidbody2D>().velocity = new Vector2(playerController.maxSpeedX, -10f);
		playerController.SlideNaDole = true;
		playerController.Glide = true;
	}
}
