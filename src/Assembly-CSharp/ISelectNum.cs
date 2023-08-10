using UnityEngine.Events;

public interface ISelectNum
{
	void Init(string itemName, int maxNum, UnityAction Ok = null, UnityAction Cancel = null);

	void AddNum();

	void ReduceNum();
}
