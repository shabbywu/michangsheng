using UnityEngine;

namespace Fungus;

[ExecuteInEditMode]
public class View : MonoBehaviour
{
	[Tooltip("Orthographic size of the camera view in world units.")]
	[SerializeField]
	protected float viewSize = 0.5f;

	[Tooltip("Aspect ratio of the primary view rectangle. (e.g. 4:3 aspect ratio = 1.333)")]
	[SerializeField]
	protected Vector2 primaryAspectRatio = new Vector2(4f, 3f);

	[Tooltip("Aspect ratio of the secondary view rectangle. (e.g. 2:1 aspect ratio = 2.0)")]
	[SerializeField]
	protected Vector2 secondaryAspectRatio = new Vector2(2f, 1f);

	public virtual float ViewSize
	{
		get
		{
			return viewSize;
		}
		set
		{
			viewSize = value;
		}
	}

	public virtual Vector2 PrimaryAspectRatio
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return primaryAspectRatio;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			primaryAspectRatio = value;
		}
	}

	public virtual Vector2 SecondaryAspectRatio
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return secondaryAspectRatio;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			secondaryAspectRatio = value;
		}
	}

	protected virtual void Update()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
