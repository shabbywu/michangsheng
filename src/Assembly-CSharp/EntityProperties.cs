using UnityEngine;

public class EntityProperties : MonoBehaviour
{
	public int Type;

	[HideInInspector]
	public Vector3 originalPosition;

	public int minimumLevel = 1;

	public int Verovatnoca = 100;

	[HideInInspector]
	public bool currentlyUsable;

	public bool DozvoljenoSkaliranje;

	[HideInInspector]
	public Vector3 originalScale;

	public int maxBrojPojavljivanja;

	[HideInInspector]
	public int brojPojavljivanja;

	public bool slobodanEntitet = true;

	public bool trenutnoJeAktivan;

	[HideInInspector]
	public bool smanjivanjeVerovatnoce;

	[HideInInspector]
	public bool instanciran;

	private void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		originalPosition = ((Component)this).transform.localPosition;
		originalScale = ((Component)this).transform.localScale;
	}
}
