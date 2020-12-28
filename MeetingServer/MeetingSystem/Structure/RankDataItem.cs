using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Structure
{
    public class RankDataItem
    {
        private int id;
        private List<RankData> rankCandidateList = new List<RankDataItem.RankData>();
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public List<RankData> RankCandidateList
        {
            get { return rankCandidateList; }
            set { rankCandidateList = value; }
        }
        public class RankData
        {
            String name;

            public String Name
            {
                get { return name; }
                set { name = value; }
            }
            String rank;

            public String Rank
            {
                get { return rank; }
                set { rank = value; }
            }

            public void print()
            {
                Console.Write(name + "/" + rank);
            }
        }

        public void print()
        {
            Console.Write("id:" + Id);
            Console.Write("   rank:");
            for (int i = 0; i < RankCandidateList.Count(); i++)
            {
                RankCandidateList.ElementAt(i).print();
                Console.Write("   ");
            }
            Console.WriteLine("");
        }
    }
}
