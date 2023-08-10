using UnityEngine;

public class HUDFPS : MonoBehaviour
{
	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;

	private void Start()
	{
		timeleft = updateInterval;
		((Component)((Component)this).transform).GetComponent<GUIText>().fontSize = Screen.height / 20;
	}

	private void Update()
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if ((double)timeleft <= 0.0)
		{
			float num = accum / (float)frames;
			string text = $"{num:F2} FPS";
			((Component)this).GetComponent<GUIText>().text = text;
			if (num < 25f)
			{
				((Component)this).GetComponent<GUIText>().material.color = Color.yellow;
			}
			else if (num < 15f)
			{
				((Component)this).GetComponent<GUIText>().material.color = Color.red;
			}
			else
			{
				((Component)this).GetComponent<GUIText>().material.color = Color.green;
			}
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
	}
}
