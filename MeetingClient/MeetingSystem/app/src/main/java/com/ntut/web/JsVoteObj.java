package com.ntut.web;

import android.util.Log;
import android.webkit.JavascriptInterface;

public class JsVoteObj {
	private final String TAG = "JsVoteObj";
	private final String ERROR = "OnScoreFinishedListenner Uninitialized";
	private OnFinishListenner listenner=null;
	private String relativePathString="";
	public JsVoteObj(String relativePath)
	{
		this.relativePathString=relativePath;
	}
	@JavascriptInterface
	public void sendJsonVote(String votesJsonStr)
	{
		Log.i(TAG,"sendJsonVote");
		if(listenner !=null)
			listenner.onFinished(votesJsonStr);
		else 
			Log.d(TAG,"listenner UNINITIAL");			
	}
	@JavascriptInterface
	public String getRelativePath()
	{
		return this.relativePathString;
	}
	public void setOnVoteFinishListener(OnFinishListenner listenner)
	{
		this.listenner=listenner;
	}
}
