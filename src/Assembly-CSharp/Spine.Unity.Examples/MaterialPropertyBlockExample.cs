using UnityEngine;

namespace Spine.Unity.Examples;

public class MaterialPropertyBlockExample : MonoBehaviour
{
	public float timeInterval = 1f;

	public Gradient randomColors = new Gradient();

	public string colorPropertyName = "_FillColor";

	private MaterialPropertyBlock mpb;

	private float timeToNextColor;

	private void Start()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		mpb = new MaterialPropertyBlock();
	}

	private void Update()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (timeToNextColor <= 0f)
		{
			timeToNextColor = timeInterval;
			Color val = randomColors.Evaluate(Random.value);
			mpb.SetColor(colorPropertyName, val);
			((Renderer)((Component)this).GetComponent<MeshRenderer>()).SetPropertyBlock(mpb);
		}
		timeToNextColor -= Time.deltaTime;
	}
}
