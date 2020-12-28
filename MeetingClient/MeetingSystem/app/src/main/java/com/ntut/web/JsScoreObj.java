package com.ntut.web;

import android.util.Log;
import android.webkit.JavascriptInterface;

public class JsScoreObj {
	private final String TAG = "JsScoreObj";
	private final String ERROR = "OnScoreFinishedListenner Uninitialized";
	private OnScoreFinishedListenner listenner = null;
	private String relativePath;
	public JsScoreObj(String relativePath)
	{
		this.relativePath=relativePath;
	}
	
	@JavascriptInterface 
	public void sendJsonScore(String scoresJsonStr,String reasonsJsonStr)
	{
		Log.i(TAG, "sendJsonScore");
		if(listenner != null)
			listenner.onScoreFinished(scoresJsonStr, reasonsJsonStr);
		else
			Log.d(TAG, ERROR);
	}
	
	@JavascriptInterface 
	public String getRelativePath()
	{ 
		Log.i(TAG, "getRelativePath");
		return this.relativePath;
	}
	
	public void setOnScoreFinishedListenner(OnScoreFinishedListenner listenner)
	{
		this.listenner = listenner;
	}
}
