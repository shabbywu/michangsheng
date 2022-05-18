using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000577 RID: 1399
public class vaweGameController : MonoBehaviour
{
	// Token: 0x06002385 RID: 9093 RVA: 0x0001CB2D File Offset: 0x0001AD2D
	private void Start()
	{
		this.pageNumber.text = string.Format("当前页码：0", Array.Empty<object>());
		this.pageView.OnPageChanged = new Action<int>(this.pageChanged);
	}

	// Token: 0x06002386 RID: 9094 RVA: 0x0001CB60 File Offset: 0x0001AD60
	private void pageChanged(int index)
	{
		this.pageNumber.text = string.Format("当前页码：{0}", index.ToString());
	}

	// Token: 0x06002387 RID: 9095 RVA: 0x0012465C File Offset: 0x0012285C
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

	// Token: 0x06002388 RID: 9096 RVA: 0x0001CB7E File Offset: 0x0001AD7E
	private void Destroy()
	{
		this.pageView.OnPageChanged = null;
	}

	// Token: 0x04001E98 RID: 7832
	[SerializeField]
	private Text pageNumber;

	// Token: 0x04001E99 RID: 7833
	[SerializeField]
	private InputField inputField;

	// Token: 0x04001E9A RID: 7834
	[SerializeField]
	private PageView pageView;
}
