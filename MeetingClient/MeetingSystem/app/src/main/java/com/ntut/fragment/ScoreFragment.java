package com.ntut.fragment;

import android.annotation.SuppressLint;
import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.os.Message;
import android.os.Messenger;
import android.os.RemoteException;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;

import com.ntut.activity.MeetingMainActivity;
import com.ntut.mainactivity.R;
import com.ntut.meetingInterface.Updatable;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.ScoreDataItem;
import com.ntut.structure.ScoreSettingDataItem;
import com.ntut.utils.GsonUtils;
import com.ntut.web.JsScoreObj;
import com.ntut.web.OnScoreFinishedListenner;

public class ScoreFragment extends Fragment implements OnScoreFinishedListenner{
	private final String TAG = "ScoreFragment";
	private WebView webView = null;
	private JsScoreObj scoreObj = null;
	private Messenger serviceMessenger = null;
	private Updatable scoreListUpdatable=null;
	public ScoreFragment()
	{
	}
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
							 Bundle savedInstanceState) {
		return inflater.inflate(R.layout.webviewer, container, false);
	}

	@Override
	public void onStart() {
		super.onStart();
		scoreListUpdatable=(Updatable)getActivity().getIntent().getSerializableExtra(ScoreListFragment.UPDATER);
		//webView.clearCache(true);
		webView = (WebView) getActivity().findViewById(R.id.webView1);

		webView.setWebChromeClient(new WebChromeClient());
		webView.getSettings().setCacheMode(WebSettings.LOAD_NO_CACHE);
		webView.getSettings().setJavaScriptEnabled(true);
		webView.getSettings().setLoadsImagesAutomatically(true);
		webView.getSettings().setBuiltInZoomControls(true);
		webView.getSettings().setAppCacheEnabled(false);
		String pathString = parseRelativePath();
		Log.d(TAG, pathString);
		scoreObj = new JsScoreObj(pathString);
		scoreObj.setOnScoreFinishedListenner(this);
		webView.addJavascriptInterface(scoreObj, "scoreObj");

		serviceMessenger = ((MeetingMainActivity)getActivity()).getServiceMessager();
		String url = getActivity().getIntent().getExtras().getString("url")+"?timestamp="+System.currentTimeMillis();
		webView.loadUrl(url);

		webView.setWebViewClient(new WebViewClient() {
			@Override
			public boolean shouldOverrideUrlLoading(WebView view, String url) {
				view.loadUrl(url);
				return true;
			}
		});
	}

	private String parseRelativePath() {
		String fileName = getActivity().getIntent().getExtras().getString("fileName");
		return fileName;
	}

	@Override
	public void onScoreFinished(String scoresJsonStr, String reasonsJsonStr) {
		Log.i(TAG, "OnScoreFinished");
		Log.d(TAG, String.format("Scores : %s", scoresJsonStr));
		Log.d(TAG, String.format("Reasons : %s", reasonsJsonStr));
		int id = getActivity().getIntent().getExtras().getInt("scoreId");
		ScoreDataItem dataItem = new ScoreDataItem();
		dataItem.setId(id);
		float[] scores = (float[]) GsonUtils.JsonToObject(scoresJsonStr, float[].class);
		String[] reasons = (String[]) GsonUtils.JsonToObject(reasonsJsonStr, String[].class);

		dataItem.setScores(scores);
		dataItem.setReasons(reasons);

		Message message = Message.obtain();
		message.what = ClientMessage.ScoreMessage.ordinal();
		Bundle data = new Bundle();
		data.putString("RET", "Activity");
		message.setData(data);
		message.obj = dataItem;
		try {
			serviceMessenger.send(message);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		getFragmentManager().beginTransaction().remove(this).commit();
		scoreListUpdatable.sendUpdateMessage();
//		Log.d(TAG, Boolean.toString(getFragmentManager().popBackStackImmediate()));
	}
	@Override
	public void onDestroy() {
		// TODO Auto-generated method stub
		super.onDestroy();
		webView.clearCache(true);
	}
}
