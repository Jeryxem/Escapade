using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
    public class GameLevel
    {
        private int levelNo;
        private List<long> progressTimeTracker;

        public GameLevel()
        {
            levelNo = 1;
        }

        public int Level
        {
            get { return levelNo; }
            set { levelNo = value; }
        }

        public List<long> Tracker
        {
            get { return progressTimeTracker; }
        }

        public long this[int index]
        {
            get
            {
                try
                {
                    return progressTimeTracker[index];
                }
                catch (IndexOutOfRangeException)
                {
                    return 0;
                }
            }
        }

        public void AddToTracker(long timeTicks)
        {
            progressTimeTracker.Add(timeTicks);
        }

        public void RemoveFromTracker(long timeTicks)
        {
            progressTimeTracker.Remove(timeTicks);
        }

        public void ClearTracker()
        {
            progressTimeTracker.Clear();
        }

        public double AverageLevelProgressTime ()
        {
            double average = 0;
            long totalTime = 0;

            foreach (long timeTick in progressTimeTracker)
            {
                totalTime += timeTick;
            }

            average = totalTime / progressTimeTracker.Count;

            return Math.Floor(average);
        }

        public void IncreaseLevel()
        {
            levelNo++;
        }

        public void DecreaseLevel()
        {
            levelNo--;
        }

        public String PrintLevel()
        {
            return levelNo.ToString();
        }
    }
}
