package com.ntut.structure;

public class RankSettingDataItem {
    public enum RankSettingStatus { ACTIVE, NONACTIVE, COMPLETE };
    private RankSettingStatus status;
    private int rankId;
    private String rankSourcePath;
    private String rankResultPath;
    private String rankWebIp;
    private String webFileName;
	public RankSettingStatus getStatus() {
		return status;
	}
	public void setStatus(RankSettingStatus status) {
		this.status = status;
	}
	public int getRankId() {
		return rankId;
	}
	public void setRankId(int rankId) {
		this.rankId = rankId;
	}
	public String getRankSourcePath() {
		return rankSourcePath;
	}
	public void setRankSourcePath(String rankSourcePath) {
		this.rankSourcePath = rankSourcePath;
	}
	public String getRankResultPath() {
		return rankResultPath;
	}
	public void setRankResultPath(String rankResultPath) {
		this.rankResultPath = rankResultPath;
	}
	public String getRankWebIp() {
		return rankWebIp;
	}
	public void setRankWebIp(String rankWebIp) {
		this.rankWebIp = rankWebIp;
	}
	public String getWebFileName() {
		return webFileName;
	}
	public void setWebFileName(String webFileName) {
		this.webFileName = webFileName;
	}  
}
