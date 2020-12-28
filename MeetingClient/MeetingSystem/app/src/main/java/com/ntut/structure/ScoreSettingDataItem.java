package com.ntut.structure;

public class ScoreSettingDataItem {
	private int scoreId;
    private String scoreSourcePath;
    private String scoreResultPath;
	private String scoreWebIp;
	private String webFileName;
	
    public String getWebFileName() {
		return webFileName;
	}
	public void setWebFileName(String webFileName) {
		this.webFileName = webFileName;
	}
	public String getScoreWebIp() {
		return scoreWebIp;
	}
	public void setScoreWebIp(String scoreWebIp) {
		this.scoreWebIp = scoreWebIp;
	}

    public int getScoreId() {
		return scoreId;
	}
	public void setScoreId(int scoreId) {
		this.scoreId = scoreId;
	}
	public String getScoreSourcePath() {
		return scoreSourcePath;
	}
	public void setScoreSourcePath(String scoreSourcePath) {
		this.scoreSourcePath = scoreSourcePath;
	}
	public String getScoreResultPath() {
		return scoreResultPath;
	}
	public void setScoreResultPath(String scoreResultPath) {
		this.scoreResultPath = scoreResultPath;
	}

}
