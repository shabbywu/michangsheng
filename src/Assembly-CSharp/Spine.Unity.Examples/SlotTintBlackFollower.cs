using UnityEngine;

namespace Spine.Unity.Examples;

public class SlotTintBlackFollower : MonoBehaviour
{
	[SpineSlot("", "", false, true, false)]
	[SerializeField]
	protected string slotName;

	[SerializeField]
	protected string colorPropertyName = "_Color";

	[SerializeField]
	protected string blackPropertyName = "_Black";

	public Slot slot;

	private MeshRenderer mr;

	private MaterialPropertyBlock mb;

	private int colorPropertyId;

	private int blackPropertyId;

	private void Start()
	{
		Initialize(overwrite: false);
	}

	public void Initialize(bool overwrite)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		if (overwrite || mb == null)
		{
			mb = new MaterialPropertyBlock();
			mr = ((Component)this).GetComponent<MeshRenderer>();
			slot = ((Component)this).GetComponent<ISkeletonComponent>().Skeleton.FindSlot(slotName);
			colorPropertyId = Shader.PropertyToID(colorPropertyName);
			blackPropertyId = Shader.PropertyToID(blackPropertyName);
		}
	}

	public void Update()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Slot val = slot;
		if (val != null)
		{
			mb.SetColor(colorPropertyId, SkeletonExtensions.GetColor(val));
			mb.SetColor(blackPropertyId, SkeletonExtensions.GetColorTintBlack(val));
			((Renderer)mr).SetPropertyBlock(mb);
		}
	}

	private void OnDisable()
	{
		mb.Clear();
		((Renderer)mr).SetPropertyBlock(mb);
	}
}
