using System.Collections;
using UnityEngine;

namespace Fungus;

internal class TaskManager : MonoBehaviour
{
	public class TaskState
	{
		public delegate void FinishedHandler(bool manual);

		private IEnumerator coroutine;

		private bool running;

		private bool paused;

		private bool stopped;

		public bool Running => running;

		public bool Paused => paused;

		public event FinishedHandler Finished;

		public TaskState(IEnumerator c)
		{
			coroutine = c;
		}

		public void Pause()
		{
			paused = true;
		}

		public void Unpause()
		{
			paused = false;
		}

		public void Start()
		{
			running = true;
			((MonoBehaviour)singleton).StartCoroutine(CallWrapper());
		}

		public void Stop()
		{
			stopped = true;
			running = false;
		}

		private IEnumerator CallWrapper()
		{
			yield return null;
			IEnumerator e = coroutine;
			while (running)
			{
				if (paused)
				{
					yield return null;
				}
				else if (e != null && e.MoveNext())
				{
					yield return e.Current;
				}
				else
				{
					running = false;
				}
			}
			this.Finished?.Invoke(stopped);
		}
	}

	private static TaskManager singleton;

	public static TaskState CreateTask(IEnumerator coroutine)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)singleton == (Object)null)
		{
			singleton = new GameObject("TaskManager").AddComponent<TaskManager>();
		}
		return new TaskState(coroutine);
	}
}
