using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPacman
{
    public class Timer
    {
        #region Props
        public int Value
        {
            get { return time; }
            set { time = value; }
        }
        private int time;

        public int Count
        {
            get { return timer; }
            set { timer = value; }
        }
        private int timer;

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        private bool isAlive;

        public bool IsFinished
        {
            get { return isFinished; }
            set { isFinished = value; }
        }
        private bool isFinished;
        #endregion

        #region Ctor
        public Timer(int time)
        {
            this.time = time;
            this.IsAlive = true;
            Reset();
        }
        #endregion  

        #region Update
        public void Update()
        {
            if (isAlive)
            {
                timer++;
                if (timer == time)
                    IsFinished = true;
            }
        }
        #endregion

        #region Reset
        public void Reset()
        {
            timer = 0;
            this.isFinished = false;
        }
        #endregion
    }
}
