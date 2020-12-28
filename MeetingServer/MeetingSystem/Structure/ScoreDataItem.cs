using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Structure
{
    class ScoreDataItem
    {
        private int id;
        private float[] scores;
        private String[] reasons;

        public String[] Reasons
        {
            get { return reasons; }
            set { reasons = value; }
        }

        public float[] Scores
        {
            get { return scores; }
            set { scores = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public void print()
        {
            Console.Write("id:" + Id);
            Console.Write("   scores:");
            for (int i = 0; i < scores.Count(); i++)
                Console.Write(scores.ElementAt(i) + " ");
            Console.Write("   reasons:");
            for (int i = 0; i < reasons.Count(); i++)
                Console.Write(reasons.ElementAt(i) + " ");
            Console.WriteLine("");
        }
    }
}
