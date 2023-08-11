using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Spine.Unity.Examples;

public class EquipButtonExample : MonoBehaviour
{
	public EquipAssetExample asset;

	public EquipSystemExample equipSystem;

	public Image inventoryImage;

	private void OnValidate()
	{
		MatchImage();
	}

	private void MatchImage()
	{
		if ((Object)(object)inventoryImage != (Object)null)
		{
			inventoryImage.sprite = asset.sprite;
		}
	}

	private void Start()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		MatchImage();
		((UnityEvent)((Component)this).GetComponent<Button>().onClick).AddListener((UnityAction)delegate
		{
			equipSystem.Equip(asset);
		});
	}
}
