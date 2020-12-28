using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem
{
    class VoteDataItem
    {
        private int id;       
        List<voteData> candidateList = new List<voteData>();
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private List<voteData> CandidateList
        {
            get { return candidateList; }
            set { candidateList = value; }
        }
        class voteData
        {
            private String name;
            private Boolean agreeChoose;
            private Boolean rejectChoose;
            public String Name
            {
                get { return name; }
                set { name = value; }
            }
            public Boolean AgreeChoose
            {
                get { return agreeChoose; }
                set { agreeChoose = value; }
            }
            public Boolean RejectChoose
            {
                get { return rejectChoose; }
                set { rejectChoose = value; }
            }

        }
    }
}
