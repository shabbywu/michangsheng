using System;
using UnityEngine;
using UnityEngine.UI;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E40 RID: 3648
	public class EquipButtonExample : MonoBehaviour
	{
		// Token: 0x060057AC RID: 22444 RVA: 0x0003EAE6 File Offset: 0x0003CCE6
		private void OnValidate()
		{
			this.MatchImage();
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x0003EAEE File Offset: 0x0003CCEE
		private void MatchImage()
		{
			if (this.inventoryImage != null)
			{
				this.inventoryImage.sprite = this.asset.sprite;
			}
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x0003EB14 File Offset: 0x0003CD14
		private void Start()
		{
			this.MatchImage();
			base.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.equipSystem.Equip(this.asset);
			});
		}

		// Token: 0x040057A2 RID: 22434
		public EquipAssetExample asset;

		// Token: 0x040057A3 RID: 22435
		public EquipSystemExample equipSystem;

		// Token: 0x040057A4 RID: 22436
		public Image inventoryImage;
	}
}
