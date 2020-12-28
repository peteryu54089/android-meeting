package com.ntut.structure;

import java.util.ArrayList;
import java.util.List;

import android.widget.ListAdapter;

public class VoteDataItem {
	private int id;
	List<voteData> candidateList = new ArrayList<voteData>();
	
    public class voteData
	{
        private String name;
        private Boolean agreeChoose;
        private Boolean rejectChoose;
        private Boolean invalidateChoose;
		public String getName() {
			return name;
		}
		public void setName(String name) {
			this.name = name;
		}
		public Boolean getAgreeChoose() {
			return agreeChoose;
		}
		public void setAgreeChoose(Boolean agreeChoose) {
			this.agreeChoose = agreeChoose;
		}
		public Boolean getRejectChoose() {
			return rejectChoose;
		}
		public void setRejectChoose(Boolean rejectChoose) {
			this.rejectChoose = rejectChoose;
		}
		public Boolean getInvalidateChoose() {
			return invalidateChoose;
		}
		public void setInvalidateChoose(Boolean invalidateChoose) {
			this.invalidateChoose = invalidateChoose;
		}	
	}

	public List<voteData> getCandidateList() {
		return candidateList;
	}

	public void setCandidateList(List<voteData> candidateList) {
		this.candidateList = candidateList;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}
}
