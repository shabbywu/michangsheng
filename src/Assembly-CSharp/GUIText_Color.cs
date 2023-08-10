using UnityEngine;

[RequireComponent(typeof(GUIText))]
public class GUIText_Color : MonoBehaviour
{
	public Color labelColor;

	private void Awake()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).GetComponent<GUIText>().material.color = labelColor;
	}
}
