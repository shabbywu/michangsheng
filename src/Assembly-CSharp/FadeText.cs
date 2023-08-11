using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class FadeText : MonoBehaviour
{
	private TextMesh tm;

	private void Start()
	{
		tm = ((Component)this).GetComponent<TextMesh>();
	}

	private void Update()
	{
		if (Input.GetKeyUp((KeyCode)113))
		{
			((MonoBehaviour)this).StartCoroutine(FadeOut(0.005f));
		}
	}

	private IEnumerator FadeOut(float step)
	{
		float t = 0f;
		while (t < 1f)
		{
			((Component)tm).GetComponent<Renderer>().material.color = new Color(((Component)tm).GetComponent<Renderer>().material.color.r, ((Component)tm).GetComponent<Renderer>().material.color.g, ((Component)tm).GetComponent<Renderer>().material.color.b, Mathf.Lerp(1f, 0f, t));
			t += step;
			yield return null;
		}
	}
}
