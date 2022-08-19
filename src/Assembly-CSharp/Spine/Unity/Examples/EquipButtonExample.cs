using System;
using UnityEngine;
using UnityEngine.UI;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AEE RID: 2798
	public class EquipButtonExample : MonoBehaviour
	{
		// Token: 0x06004E26 RID: 20006 RVA: 0x002157F0 File Offset: 0x002139F0
		private void OnValidate()
		{
			this.MatchImage();
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x002157F8 File Offset: 0x002139F8
		private void MatchImage()
		{
			if (this.inventoryImage != null)
			{
				this.inventoryImage.sprite = this.asset.sprite;
			}
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x0021581E File Offset: 0x00213A1E
		private void Start()
		{
			this.MatchImage();
			base.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.equipSystem.Equip(this.asset);
			});
		}

		// Token: 0x04004D8B RID: 19851
		public EquipAssetExample asset;

		// Token: 0x04004D8C RID: 19852
		public EquipSystemExample equipSystem;

		// Token: 0x04004D8D RID: 19853
		public Image inventoryImage;
	}
}
