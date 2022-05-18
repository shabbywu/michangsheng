using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001326 RID: 4902
	[EventHandlerInfo("MonoBehaviour", "CharacterCollider", "The block will execute when tag filtered OnCharacterColliderHit is received")]
	[AddComponentMenu("")]
	public class CharacterControllerCollide : TagFilteredEventHandler
	{
		// Token: 0x0600774F RID: 30543 RVA: 0x0005150C File Offset: 0x0004F70C
		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			base.ProcessTagFilter(hit.gameObject.tag);
		}
	}
}
