using UnityEngine;

public class Water2DScript : MonoBehaviour
{
	public Vector2 speed = new Vector2(0.01f, 0f);

	private Renderer rend;

	private Material mat;

	private void Awake()
	{
		rend = ((Component)this).GetComponent<Renderer>();
		rend.enabled = true;
		mat = rend.material;
	}

	private void LateUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Time.deltaTime * speed;
		Material obj = mat;
		obj.mainTextureOffset += val;
	}
}
