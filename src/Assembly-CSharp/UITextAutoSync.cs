using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000367 RID: 871
[RequireComponent(typeof(Text))]
public class UITextAutoSync : MonoBehaviour
{
	// Token: 0x06001D36 RID: 7478 RVA: 0x000CF129 File Offset: 0x000CD329
	private void Awake()
	{
		this.me = base.GetComponent<Text>();
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x000CF138 File Offset: 0x000CD338
	private void Update()
	{
		if (this.Target != null && this.me.text != this.Target.text)
		{
			this.me.text = this.Target.text;
		}
	}

	// Token: 0x040017CD RID: 6093
	private Text me;

	// Token: 0x040017CE RID: 6094
	public Text Target;
}
