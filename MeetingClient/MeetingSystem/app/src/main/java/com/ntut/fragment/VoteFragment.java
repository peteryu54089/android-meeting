package com.ntut.fragment;

import java.util.List;

import android.app.Fragment;
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
import com.ntut.structure.VoteDataItem;
import com.ntut.structure.VoteDataItem.voteData;
import com.ntut.utils.GsonUtils;
import com.ntut.web.JsVoteObj;
import com.ntut.web.OnFinishListenner;

public class VoteFragment extends Fragment implements OnFinishListenner {
	private final String TAG = "VoteFragment";
	private WebView webView = null;
	private JsVoteObj voteObj = null;
	private Updatable voteListUpdatable = null;
	private Messenger serviceMessenger = null;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		return inflater.inflate(R.layout.webviewer, container, false);
	}

	@Override
	public void onStart() {
		super.onStart();
		voteListUpdatable = (Updatable) getActivity().getIntent()
				.getSerializableExtra(VoteListFragment.UPDATER);
		webViewSetting();
		String pathString = parseRelativePath();
		Log.d(TAG, pathString);

		voteObj = new JsVoteObj(pathString);
		voteObj.setOnVoteFinishListener(this);

		webView.addJavascriptInterface(voteObj, "voteObj");
		serviceMessenger = ((MeetingMainActivity) getActivity())
				.getServiceMessager();
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
		String fileName = getActivity().getIntent().getExtras()
				.getString("fileName");
		return fileName;
	}

	private void webViewSetting() {
		webView = (WebView) getActivity().findViewById(R.id.webView1);
		webView.setWebChromeClient(new WebChromeClient());
		webView.getSettings().setJavaScriptEnabled(true);
		webView.getSettings().setLoadsImagesAutomatically(true);
		webView.getSettings().setBuiltInZoomControls(true);
		webView.getSettings().setCacheMode(WebSettings.LOAD_NO_CACHE);
		webView.getSettings().setAppCacheEnabled(false);
	}

	@Override
	public void onFinished(String voteJsonStr) {
		// TODO Auto-generated method stub
		Log.i(TAG, "onVoteFinished");
		Log.d(TAG, voteJsonStr);
		int id = getActivity().getIntent().getExtras().getInt("voteId");
		VoteDataItem item = new VoteDataItem();
		item.setId(id);
		item.setCandidateList((List<voteData>) GsonUtils.JsonToObject(
				voteJsonStr, item.getCandidateList().getClass()));

		Message message = Message.obtain();
		message.what = ClientMessage.VoteMessage.ordinal();
		message.obj = item;
		try {
			serviceMessenger.send(message);
		} catch (RemoteException e) {
			e.printStackTrace();
		}

		getFragmentManager().beginTransaction().remove(this).commit();
		voteListUpdatable.sendUpdateMessage();
	}
	@Override
	public void onDestroy() {
		// TODO Auto-generated method stub
		super.onDestroy();
		webView.clearCache(true);
	}

}
