using UnityEngine;

public class HUDFPS1 : MonoBehaviour
{
	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;

	private TextMesh guiText1;

	private void Start()
	{
		timeleft = updateInterval;
		guiText1 = ((Component)((Component)this).transform).GetComponent<TextMesh>();
	}

	private void Update()
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if ((double)timeleft <= 0.0)
		{
			float num = accum / (float)frames;
			string text = $"{num:F2} FPS";
			guiText1.text = text;
			if (num < 25f)
			{
				guiText1.color = Color.yellow;
			}
			else if (num < 15f)
			{
				guiText1.color = Color.red;
			}
			else
			{
				guiText1.color = Color.green;
			}
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
	}
}
