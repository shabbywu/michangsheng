using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA4 RID: 3748
	[EventHandlerInfo("MonoBehaviour", "CharacterCollider", "The block will execute when tag filtered OnCharacterColliderHit is received")]
	[AddComponentMenu("")]
	public class CharacterControllerCollide : TagFilteredEventHandler
	{
		// Token: 0x06006A19 RID: 27161 RVA: 0x00292A34 File Offset: 0x00290C34
		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			base.ProcessTagFilter(hit.gameObject.tag);
		}
	}
}
