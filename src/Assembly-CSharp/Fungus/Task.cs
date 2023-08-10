using System.Collections;

namespace Fungus;

public class Task
{
	public delegate void FinishedHandler(bool manual);

	private TaskManager.TaskState task;

	public bool Running => task.Running;

	public bool Paused => task.Paused;

	public event FinishedHandler Finished;

	public Task(IEnumerator c, bool autoStart = true)
	{
		task = TaskManager.CreateTask(c);
		task.Finished += TaskFinished;
		if (autoStart)
		{
			Start();
		}
	}

	public void Start()
	{
		task.Start();
	}

	public void Stop()
	{
		task.Stop();
	}

	public void Pause()
	{
		task.Pause();
	}

	public void Unpause()
	{
		task.Unpause();
	}

	private void TaskFinished(bool manual)
	{
		this.Finished?.Invoke(manual);
	}
}
