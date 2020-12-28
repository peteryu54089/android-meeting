package com.ntut.structure;

public class ScoreDataItem {
	private int id;
    private float[] scores;
    private String[] reasons;
    
	public String[] getReasons() {
		return reasons;
	}
	public void setReasons(String[] reasons) {
		this.reasons = reasons;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public float[] getScores() {
		return scores;
	}
	public void setScores(float[] scores) {
		this.scores = scores;
	}
}
