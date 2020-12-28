package com.ntut.fragment;

import org.videolan.vlc.EventManager;
import org.videolan.vlc.LibVLC;
import org.videolan.vlc.LibVlcException;
import org.videolan.vlc.Util;
import org.videolan.vlc.VLCApplication;
import org.videolan.vlc.WeakHandler;

import android.app.Fragment;
import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.res.Configuration;
import android.graphics.ImageFormat;
import android.graphics.PixelFormat;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.webkit.WebView;

import com.ntut.mainactivity.R;
import com.ntut.service.MeetingService;
import com.ntut.structure.ServerInfo;

public class BroadcastFragment extends Fragment implements
		SurfaceHolder.Callback {
	private static final String TAG = "BroadcastFragment";
	private SurfaceView mSurface = null;
	private SurfaceHolder mSurfaceHolder = null;
	private ProgressDialog dialog = null;
	private LibVLC mLibVLC = null;
	private EventManager mEventManger = null;
	private boolean mIsPlaying;
	private int mVideoHeight;
	private int mVideoWidth;
	private int mSarNum;
	private int mSarDen;
	private int mSurfaceAlign;
	private static final int SURFACE_SIZE = 3;
	private static final int SURFACE_BEST_FIT = 0;
	private static final int SURFACE_FIT_HORIZONTAL = 1;
	private static final int SURFACE_FIT_VERTICAL = 2;
	private static final int SURFACE_FILL = 3;
	private static final int SURFACE_16_9 = 4;
	private static final int SURFACE_4_3 = 5;
	private static final int SURFACE_ORIGINAL = 6;
	private int mCurrentSize = SURFACE_BEST_FIT;
	
	private int OS_VERSION;
	private WebView mWebview = null;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		return inflater.inflate(R.layout.broadcast, container, false);
	}

	@Override
	public void onStart() {
		super.onStart();
		
		OS_VERSION = Integer.parseInt(VERSION.RELEASE.replace(".", ""));
		Log.d("VERSION", VERSION.RELEASE);
		if (OS_VERSION > 0) {   //  Android版本若大於4.2.1使用VLC會有錯誤
			mSurface = (SurfaceView) getActivity()
					.findViewById(R.id.player_surface);
			mSurface.setVisibility(View.GONE);  //  把surface去掉
			
			mWebview = (WebView) getActivity()  //  利用webview把url的畫面播放出來
				.findViewById(R.id.webView2);
		}
		else {  
			mWebview = (WebView) getActivity()  
					.findViewById(R.id.webView2);
			mWebview.setVisibility(View.GONE);  //  把webview去掉
			
			mSurface = (SurfaceView) getActivity().findViewById(R.id.player_surface);  //  利用surface把VLC的畫面播放出來
			mSurfaceHolder = mSurface.getHolder();
			mSurfaceHolder.addCallback(this);
			mSurface.setKeepScreenOn(true);
			SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(getActivity());
			int pitch;
			String chroma = pref.getString("chroma_format", "");
			if (Util.isGingerbreadOrLater() && chroma.equals("YV12")) {
				mSurfaceHolder.setFormat(ImageFormat.YV12);
				pitch = ImageFormat.getBitsPerPixel(ImageFormat.YV12) / 8;
			} else if (chroma.equals("RV16")) {
				mSurfaceHolder.setFormat(PixelFormat.RGB_565);
				PixelFormat info = new PixelFormat();
				PixelFormat.getPixelFormatInfo(PixelFormat.RGB_565, info);
				pitch = info.bytesPerPixel;
			} else {
				mSurfaceHolder.setFormat(PixelFormat.RGBX_8888);
				PixelFormat info = new PixelFormat();
				PixelFormat.getPixelFormatInfo(PixelFormat.RGBX_8888, info);
				pitch = info.bytesPerPixel;
			}
			mSurfaceAlign = 16 / pitch - 1;
			enableIOMX(true);
			try {
				mLibVLC = LibVLC.getInstance();
			} catch (LibVlcException e) {
				Log.i(TAG, "LibVLC.getInstance() error:" + e.toString());
				e.printStackTrace();
				return;
			}
			mEventManger = EventManager.getInstance();
			mEventManger.addHandler(mEventHandler);
		}
	}

	private void enableIOMX(boolean enableIomx) {
		SharedPreferences p = PreferenceManager
				.getDefaultSharedPreferences(VLCApplication.getAppContext());
		Editor e = p.edit();
		e.putBoolean("enable_iomx", enableIomx);
		// LibVLC.restart();
	}

	private final VideoEventHandler mEventHandler = new VideoEventHandler(this);

	private class VideoEventHandler extends WeakHandler<BroadcastFragment> {
		public VideoEventHandler(BroadcastFragment owner) {
			super(owner);
		}

		@Override
		public void handleMessage(Message msg) {
			BroadcastFragment fragment = getOwner();
			if (fragment == null)
				return;
			switch (msg.getData().getInt("event")) {
			case EventManager.MediaPlayerPlaying:
				Log.i(TAG, "MediaPlayerPlaying");
				removeProgressDialog();
				mIsPlaying = true;
				break;
			case EventManager.MediaPlayerPaused:
				Log.i(TAG, "MediaPlayerPaused");
				mIsPlaying = false;
				break;
			case EventManager.MediaPlayerStopped:
				Log.i(TAG, "MediaPlayerStopped");
				mIsPlaying = false;
				break;
			case EventManager.MediaPlayerEndReached:
				Log.i(TAG, "MediaPlayerEndReached");
				break;
			case EventManager.MediaPlayerVout:
				break;
			default:
				Log.e(TAG, String.format("Event not handled (0x%x)", msg
						.getData().getInt("event")));
				break;
			}
			super.handleMessage(msg);
		}
	}

	@Override
	public void surfaceChanged(SurfaceHolder holder, int format, int width,
			int height) {
		Log.i(TAG, "surfaceChanged");
		// mLibVLC.attachSurface(mSurfaceHolder.getSurface(), this, width, height);
		Log.i(TAG, " width=" + width + " height=" + height);
	}

	@Override
	public void surfaceCreated(SurfaceHolder holder) {
		// TODO Auto-generated method stu

	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		// TODO Auto-generated method stub
		mLibVLC.detachSurface();

	}

	public void setSurfaceSize(int width, int height, int sar_num, int sar_den) {
		Log.i(TAG, "setSurfaceSize");
		if (width * height == 0)
			return;
		// store video size
		mVideoHeight = height;
		mVideoWidth = width;
		mSarNum = sar_num;
		mSarDen = sar_den;
		Message msg = mHandler.obtainMessage(SURFACE_SIZE);
		mHandler.sendMessage(msg);
		Log.i(TAG, " width=" + width + " height=" + height);
	}

	private final Handler mHandler = new VideoPlayerHandler(this);

	private static class VideoPlayerHandler extends
			WeakHandler<BroadcastFragment> {
		public VideoPlayerHandler(BroadcastFragment owner) {
			super(owner);
		}

		@Override
		public void handleMessage(Message msg) {
			BroadcastFragment fragment = getOwner();
			if (fragment == null) // WeakReference could be GC'ed early
				return;

			switch (msg.what) {
			case SURFACE_SIZE:
				fragment.changeSurfaceSize();
				break;
			}
		}
	};

	private ServerInfo getServerInfo() {
		return (ServerInfo) getActivity().getIntent().getSerializableExtra(
				MeetingService.INFO);
	}

	private String getURL(ServerInfo info) {
		return String.format("Http://%s:%d", info.getIp(),
				info.getScreenServicePort());
	}

	@Override
	public void onStop() {
		super.onStop();
		removeProgressDialog();
		
		mWebview.clearHistory();   //  切到其他Fragment先清除webview資料
		mWebview.clearCache(true);
		mWebview.loadUrl("about:blank");
		mWebview.freeMemory(); 
		mWebview.pauseTimers();
	}

	@Override
	public void onResume() {
		super.onResume();
		ServerInfo info = getServerInfo();
		Log.d(TAG, info.toString());
		String url = getURL(info);
		Log.d(TAG, String.format("Broadcast Server URL : %s", url));
		if (mLibVLC != null) {
			try {
				mLibVLC.readMedia(url, false);
			} catch (Exception e) {
				Log.i(TAG, e.toString());
				return;
			}
		} else {
			mWebview.loadUrl(url);
			return;
		}
		if (dialog == null)
			dialog = ProgressDialog.show(getActivity(), "", "Loading...", true);
	}
	
	private void removeProgressDialog()
	{
		if (dialog != null)
			dialog.dismiss();
		dialog = null;
	}

	private void changeSurfaceSize() {
		// get screen size
		int dw = getActivity().getWindow().getDecorView().getWidth();
		int dh = getActivity().getWindow().getDecorView().getHeight();

		// getWindow().getDecorView() doesn't always take orientation into
		// account, we have to correct the values
		boolean isPortrait = getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT;
		if (dw > dh && isPortrait || dw < dh && !isPortrait) {
			int d = dw;
			dw = dh;
			dh = d;
		}

		// sanity check
		if (dw * dh == 0 || mVideoWidth * mVideoHeight == 0) {
			Log.e(TAG, "Invalid surface size");
			return;
		}

		// compute the aspect ratio
		double ar, vw;
		double density = (double) mSarNum / (double) mSarDen;
		if (density == 1.0) {
			/* No indication about the density, assuming 1:1 */
			vw = mVideoWidth;
			ar = (double) mVideoWidth / (double) mVideoHeight;
		} else {
			/* Use the specified aspect ratio */
			vw = mVideoWidth * density;
			ar = vw / mVideoHeight;
		}

		// compute the display aspect ratio
		double dar = (double) dw / (double) dh;

		switch (mCurrentSize) {
		case SURFACE_BEST_FIT:
			if (dar < ar)
				dh = (int) (dw / ar);
			else
				dw = (int) (dh * ar);
			break;
		case SURFACE_FIT_HORIZONTAL:
			dh = (int) (dw / ar);
			break;
		case SURFACE_FIT_VERTICAL:
			dw = (int) (dh * ar);
			break;
		case SURFACE_FILL:
			break;
		case SURFACE_16_9:
			ar = 16.0 / 9.0;
			if (dar < ar)
				dh = (int) (dw / ar);
			else
				dw = (int) (dh * ar);
			break;
		case SURFACE_4_3:
			ar = 4.0 / 3.0;
			if (dar < ar)
				dh = (int) (dw / ar);
			else
				dw = (int) (dh * ar);
			break;
		case SURFACE_ORIGINAL:
			dh = mVideoHeight;
			dw = (int) vw;
			break;
		}

		// align width on 16bytes
		int alignedWidth = (mVideoWidth + mSurfaceAlign) & ~mSurfaceAlign;

		// force surface buffer size
		mSurfaceHolder.setFixedSize(alignedWidth, mVideoHeight);

		// set display size
		LayoutParams lp = mSurface.getLayoutParams();
		lp.width = dw * alignedWidth / mVideoWidth;
		lp.height = dh;
		mSurface.setLayoutParams(lp);
		mSurface.invalidate();
	}

	@Override
	public void onDestroy() {
		if (mLibVLC == null) {
			mWebview.clearHistory();
			mWebview.clearCache(true);
			mWebview.loadUrl("about:blank");
			mWebview.freeMemory(); 
			mWebview.pauseTimers();
			mWebview = null;
			super.onDestroy();
			return;
		}
		if (mLibVLC.isPlaying()) {
			mLibVLC.stop();
		}
		mLibVLC = null;
		super.onDestroy();
	}
}