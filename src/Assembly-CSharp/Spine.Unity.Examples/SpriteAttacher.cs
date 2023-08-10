using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples;

public class SpriteAttacher : MonoBehaviour
{
	public const string DefaultPMAShader = "Spine/Skeleton";

	public const string DefaultStraightAlphaShader = "Sprites/Default";

	public bool attachOnStart = true;

	public bool overrideAnimation = true;

	public Sprite sprite;

	[SpineSlot("", "", false, true, false)]
	public string slot;

	private RegionAttachment attachment;

	private Slot spineSlot;

	private bool applyPMA;

	private static Dictionary<Texture, AtlasPage> atlasPageCache;

	private static AtlasPage GetPageFor(Texture texture, Shader shader)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		if (atlasPageCache == null)
		{
			atlasPageCache = new Dictionary<Texture, AtlasPage>();
		}
		atlasPageCache.TryGetValue(texture, out var value);
		if (value == null)
		{
			value = AtlasUtilities.ToSpineAtlasPage(new Material(shader));
			atlasPageCache[texture] = value;
		}
		return value;
	}

	private void Start()
	{
		Initialize(overwrite: false);
		if (attachOnStart)
		{
			Attach();
		}
	}

	private void AnimationOverrideSpriteAttach(ISkeletonAnimation animated)
	{
		if (overrideAnimation && ((Behaviour)this).isActiveAndEnabled)
		{
			Attach();
		}
	}

	public void Initialize(bool overwrite = true)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		if (!overwrite && attachment != null)
		{
			return;
		}
		ISkeletonComponent component = ((Component)this).GetComponent<ISkeletonComponent>();
		SkeletonRenderer val = (SkeletonRenderer)(object)((component is SkeletonRenderer) ? component : null);
		if ((Object)(object)val != (Object)null)
		{
			applyPMA = val.pmaVertexColors;
		}
		else
		{
			SkeletonGraphic val2 = (SkeletonGraphic)(object)((component is SkeletonGraphic) ? component : null);
			if ((Object)(object)val2 != (Object)null)
			{
				applyPMA = val2.MeshGenerator.settings.pmaVertexColors;
			}
		}
		if (overrideAnimation)
		{
			ISkeletonAnimation val3 = (ISkeletonAnimation)(object)((component is ISkeletonAnimation) ? component : null);
			if (val3 != null)
			{
				val3.UpdateComplete -= new UpdateBonesDelegate(AnimationOverrideSpriteAttach);
				val3.UpdateComplete += new UpdateBonesDelegate(AnimationOverrideSpriteAttach);
			}
		}
		spineSlot = spineSlot ?? component.Skeleton.FindSlot(slot);
		Shader val4 = (applyPMA ? Shader.Find("Spine/Skeleton") : Shader.Find("Sprites/Default"));
		attachment = (applyPMA ? AttachmentRegionExtensions.ToRegionAttachmentPMAClone(sprite, val4, (TextureFormat)4, false, (Material)null, 0f) : AttachmentRegionExtensions.ToRegionAttachment(sprite, GetPageFor((Texture)(object)sprite.texture, val4), 0f));
	}

	private void OnDestroy()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		ISkeletonAnimation component = ((Component)this).GetComponent<ISkeletonAnimation>();
		if (component != null)
		{
			component.UpdateComplete -= new UpdateBonesDelegate(AnimationOverrideSpriteAttach);
		}
	}

	public void Attach()
	{
		if (spineSlot != null)
		{
			spineSlot.Attachment = (Attachment)(object)attachment;
		}
	}
}
