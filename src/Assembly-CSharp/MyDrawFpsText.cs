using UnityEngine;
using UnityEngine.UI;

public class MyDrawFpsText : MonoBehaviour
{
	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;

	private Text fpsText;

	private void Start()
	{
		timeleft = updateInterval;
		fpsText = ((Component)this).GetComponent<Text>();
	}

	private void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if ((double)timeleft <= 0.0)
		{
			float num = accum / (float)frames;
			string text = $"{num:F2} FPS";
			fpsText.text = text;
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
	}
}
