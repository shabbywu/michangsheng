using UnityEngine;

namespace Fungus;

[AddComponentMenu("")]
public class Node : MonoBehaviour
{
	[SerializeField]
	protected Rect nodeRect = new Rect(0f, 0f, 120f, 30f);

	[SerializeField]
	protected Color tint = Color.white;

	[SerializeField]
	protected bool useCustomTint;

	public virtual Rect _NodeRect
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return nodeRect;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			nodeRect = value;
		}
	}

	public virtual Color Tint
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return tint;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			tint = value;
		}
	}

	public virtual bool UseCustomTint
	{
		get
		{
			return useCustomTint;
		}
		set
		{
			useCustomTint = value;
		}
	}
}
