package com.ntut.structure;

import java.util.ArrayList;
import java.util.List;

import android.R.integer;

public class RankDataItem {
	private int id;
	private List<RankData> rankCandidateList=new ArrayList<RankDataItem.RankData>();
	public class RankData
	{
		String name;
		String rank;
		public String getName() {
			return name;
		}
		public void setName(String name) {
			this.name = name;
		}
		public String getRank() {
			return rank;
		}
		public void setRank(String rank) {
			this.rank = rank;
		}
		
		
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public List<RankData> getRankCandidateList() {
		return rankCandidateList;
	}
	public void setRankCandidateList(List<RankData> rankCandidateList) {
		this.rankCandidateList = rankCandidateList;
	}
}
