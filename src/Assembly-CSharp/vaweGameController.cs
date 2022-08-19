using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DC RID: 988
public class vaweGameController : MonoBehaviour
{
	// Token: 0x06002005 RID: 8197 RVA: 0x000E1C3E File Offset: 0x000DFE3E
	private void Start()
	{
		this.pageNumber.text = string.Format("当前页码：0", Array.Empty<object>());
		this.pageView.OnPageChanged = new Action<int>(this.pageChanged);
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x000E1C71 File Offset: 0x000DFE71
	private void pageChanged(int index)
	{
		this.pageNumber.text = string.Format("当前页码：{0}", index.ToString());
	}

	// Token: 0x06002007 RID: 8199 RVA: 0x000E1C90 File Offset: 0x000DFE90
	public void onClick()
	{
		try
		{
			int index = int.Parse(this.inputField.text);
			this.pageView.pageTo(index);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("请输入数字" + ex.ToString());
		}
	}

	// Token: 0x06002008 RID: 8200 RVA: 0x000E1CE4 File Offset: 0x000DFEE4
	private void Destroy()
	{
		this.pageView.OnPageChanged = null;
	}

	// Token: 0x04001A06 RID: 6662
	[SerializeField]
	private Text pageNumber;

	// Token: 0x04001A07 RID: 6663
	[SerializeField]
	private InputField inputField;

	// Token: 0x04001A08 RID: 6664
	[SerializeField]
	private PageView pageView;
}
