using System;
using System.Collections.Generic;
using System.Text;

namespace VisualIntelligentScissors
{
	class StopWatch
	{
		private DateTime startTime;
		private TimeSpan carriedOverTime;
		private bool running;

		public StopWatch()
		{
			carriedOverTime = TimeSpan.Zero;
			running = false;
		}

		public void Start()
		{
			carriedOverTime = TimeSpan.Zero;
			startTiming();
		}

		public void Restart()
		{
			startTiming();
		}

		public void Stop()
		{
			stopTiming();
		}

		public TimeSpan Elapsed
		{
			get
			{
				if (running)
					return carriedOverTime.Add(DateTime.Now - startTime);
				else
					return carriedOverTime;
			}
		}


		private void startTiming()
		{
			running = true;
			startTime = DateTime.Now;
		}

		private void stopTiming()
		{
			carriedOverTime = carriedOverTime.Add(DateTime.Now - startTime);
			running = false;
		}
	}
}
