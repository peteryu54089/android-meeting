package com.ntut.web;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.ComponentName;
import android.content.DialogInterface;
import android.content.ServiceConnection;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.os.IBinder;
import android.os.Message;
import android.os.Messenger;
import android.os.RemoteException;
import android.util.Log;
import android.view.Window;
import android.view.WindowManager;
import android.webkit.JsResult;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.TextView;

import com.ntut.mainactivity.R;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.ScoreDataItem;
import com.ntut.utils.GsonUtils;

public class WebViewerActivity extends Activity {

	@Override
	public void onBackPressed() {
		// TODO Auto-generated method stub
		// super.onBackPressed();
	}

	private final String TAG = "WebViewerActivity";
	private WebView webView = null;
	private JsScoreObj scoreObj = null;
	// private MeetingServiceBinder binder = null;

	private ServiceConnection serviceConnection = new ServiceConnection() {

		@Override
		public void onServiceDisconnected(ComponentName name) {
			Log.e(TAG, "onServiceDisconnected");
			finish();
		}

		@Override
		public void onServiceConnected(ComponentName name, IBinder service) {
			Log.e(TAG, "onServiceConnected");
			// binder = (MeetingServiceBinder) service;
		}
	};

	private OnScoreFinishedListenner listenner = new OnScoreFinishedListenner() {

		@Override
		public void onScoreFinished(String scoresJsonStr, String reasonsJsonStr) {
			Log.d(TAG, String.format("Scores : %s", scoresJsonStr));
			Log.d(TAG, String.format("Reasons : %s", reasonsJsonStr));
			int id = getIntent().getExtras().getInt("scoreId");
			ScoreDataItem dataItem = new ScoreDataItem();
			dataItem.setId(id);
			float[] scores = (float[]) GsonUtils.JsonToObject(scoresJsonStr,
					float[].class);
			String[] reasons = (String[]) GsonUtils.JsonToObject(
					reasonsJsonStr, String[].class);

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
			finish();
		}
	};

	private WebChromeClient AlertJS = new WebChromeClient() {
		@Override
		public boolean onJsAlert(WebView view, String url, String message,
				final JsResult result) {
			if (message.length() != 0) {
				AlertDialog.Builder builder = new AlertDialog.Builder(
						WebViewerActivity.this);
				builder.setTitle("From JavaScript")
						.setMessage(message)
						.setPositiveButton("確認",
								new DialogInterface.OnClickListener() {

									@Override
									public void onClick(DialogInterface dialog,
											int which) {
										result.cancel();
									}
								});
				builder.setCancelable(false);
				AlertDialog dialog = builder.create();
				dialog.show();
				TextView textView = (TextView) dialog
						.findViewById(android.R.id.message);
				textView.setTextSize(36);
				Button button = (Button) dialog
						.findViewById(android.R.id.button1);
				button.setTextSize(36);
				return true;
			} else {
				return false;
			}
			// return super.onJsAlert(view, url, message, result);
		}
	};

	private String parseRelativePath() {
		String relativePath = "";
		String fullPathString = getIntent().getExtras().getString("path");
		int start = fullPathString.lastIndexOf("\\") + 1;
		int end = fullPathString.lastIndexOf(".");
		relativePath = fullPathString.substring(start, end);
		return relativePath;
	}

	Messenger serviceMessenger = null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
		getWindow().requestFeature(Window.FEATURE_NO_TITLE);
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		setContentView(R.layout.webviewer);
		webView = (WebView) findViewById(R.id.webView1);
		webView.setWebChromeClient(AlertJS);
		webView.getSettings().setJavaScriptEnabled(true);
		webView.getSettings().setLoadsImagesAutomatically(true);
		webView.getSettings().setBuiltInZoomControls(true);
		webView.getSettings().setAppCacheEnabled(false);
		String pathString = parseRelativePath();
		Log.d("Command", pathString);
		scoreObj = new JsScoreObj(pathString);
		scoreObj.setOnScoreFinishedListenner(listenner);
		webView.addJavascriptInterface(scoreObj, "scoreObj");

		serviceMessenger = (Messenger) getIntent().getExtras().get(
				"serviceMessenger");
		String url = getIntent().getExtras().getString("url");
		webView.loadUrl(url);

		webView.setWebViewClient(new WebViewClient() {
			@Override
			public boolean shouldOverrideUrlLoading(WebView view, String url) {
				view.loadUrl(url);
				return true;
			}
		});
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
	}

}
