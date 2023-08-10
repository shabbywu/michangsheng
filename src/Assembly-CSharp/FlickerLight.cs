using System.Collections;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
	public float lampSpeed = 0.1f;

	public float intens_Speed = 9f;

	public bool timung;

	public float minIntens = 0.8f;

	public float maxIntens = 3.5f;

	public bool loopEnd;

	public float range_Speed = 12f;

	public float minRange = 2.8f;

	public float maxRange = 13.5f;

	public Color col_Main = Color.white;

	public float col_Speed = 1.5f;

	public Color col_Blend1 = Color.yellow;

	public Color col_Blend2 = Color.red;

	private Color refCol;

	private float intens;

	private float randomIntens;

	private float range;

	private float randomRange;

	private GameObject lamp;

	private void Start()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		lamp = ((Component)this).gameObject;
		intens = lamp.GetComponent<Light>().intensity;
		range = lamp.GetComponent<Light>().range;
		lamp.GetComponent<Light>().color = col_Main;
		((MonoBehaviour)this).StartCoroutine(Timer());
	}

	private void LateUpdate()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (loopEnd)
		{
			((MonoBehaviour)this).StartCoroutine(Timer());
		}
		intens = Mathf.SmoothStep(intens, randomIntens, Time.deltaTime * intens_Speed);
		range = Mathf.SmoothStep(range, randomRange, Time.deltaTime * range_Speed);
		lamp.GetComponent<Light>().intensity = intens;
		lamp.GetComponent<Light>().range = range;
		col_Main = Color.Lerp(col_Main, refCol, Time.deltaTime * col_Speed);
		lamp.GetComponent<Light>().color = col_Main;
	}

	private IEnumerator Timer()
	{
		timung = false;
		randomIntens = Random.Range(minIntens, maxIntens);
		randomRange = Random.Range(minRange, maxRange);
		refCol = Color.Lerp(col_Blend1, col_Blend2, Random.value);
		yield return (object)new WaitForSeconds(lampSpeed);
		timung = true;
		randomIntens = Random.Range(minIntens, maxIntens);
		randomRange = Random.Range(minRange, maxRange);
		refCol = Color.Lerp(col_Blend1, col_Blend2, Random.value);
		yield return (object)new WaitForSeconds(lampSpeed);
		loopEnd = true;
		randomIntens = Random.Range(minIntens, maxIntens);
		randomRange = Random.Range(minRange, maxRange);
		refCol = Color.Lerp(col_Blend1, col_Blend2, Random.value);
		yield return null;
	}
}
