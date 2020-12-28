package test;

import org.videolan.vlc.LibVLC;
import org.videolan.vlc.LibVlcException;
import org.videolan.vlc.R;
import org.videolan.vlc.WeakHandler;

import android.app.Activity;
import android.content.res.Configuration;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.ViewGroup.LayoutParams;

public class BroadcastActivity extends Activity implements
		SurfaceHolder.Callback {
	private final String TAG = "BroadcastFragment";
	private SurfaceHolder surfaceHolder = null;
	private SurfaceView surfaceView = null;
	private LibVLC mLibVLC = null;
	private int mVideoHeight;
	private int mVideoWidth;

	private static final int SURFACE_BEST_FIT = 0;
	private static final int SURFACE_FIT_HORIZONTAL = 1;
	private static final int SURFACE_FIT_VERTICAL = 2;
	private static final int SURFACE_FILL = 3;
	private static final int SURFACE_16_9 = 4;
	private static final int SURFACE_4_3 = 5;
	private static final int SURFACE_ORIGINAL = 6;
    private static final int FADE_OUT = 1;
    private static final int SHOW_PROGRESS = 2;
    private static final int SURFACE_SIZE = 3;
    private static final int FADE_OUT_INFO = 4;
    private static final int HIDE_NAV = 5;
	private int mCurrentSize = SURFACE_BEST_FIT;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.broadcast);
	}

	@Override
	public void onDestroy() {
		if (mLibVLC != null)
			mLibVLC.finalize();
		super.onDestroy();
	}

	@Override
	public void onStart() {
		super.onStart();
		surfaceView = (SurfaceView) findViewById(R.id.player_surface);
		surfaceHolder = surfaceView.getHolder();
		try {
			LibVLC.useIOMX(getApplicationContext());
			mLibVLC = LibVLC.getInstance();
		} catch (LibVlcException e) {
			e.printStackTrace();
		}
		openBroadcast();
	}

	// private ServerInfo getServerInfo() {
	// return (ServerInfo) getActivity().getIntent().getSerializableExtra(
	// MeetingService.INFO);
	// }
	//
	// private String getURL(ServerInfo info) {
	// return String.format("Http://%s:%d", info.getIp(),
	// info.getScreenServicePort());
	// }
	/**
     * Handle resize of the surface and the overlay
     */
    private final Handler mHandler = new VideoPlayerHandler(this);

    private static class VideoPlayerHandler extends WeakHandler<BroadcastActivity> {
        public VideoPlayerHandler(BroadcastActivity owner) {
            super(owner);
        }

        @Override
        public void handleMessage(Message msg) {
        	BroadcastActivity activity = getOwner();
            if(activity == null) // WeakReference could be GC'ed early
                return;

            switch (msg.what) {
                case FADE_OUT:
                    //activity.hideOverlay(false);
                    break;
                case SHOW_PROGRESS:
//                    int pos = activity.setOverlayProgress();
//                    if (activity.canShowProgress()) {
//                        msg = obtainMessage(SHOW_PROGRESS);
//                        sendMessageDelayed(msg, 1000 - (pos % 1000));
//                    }
                    break;
                case SURFACE_SIZE:
                    activity.changeSurfaceSize();
                    break;
                case FADE_OUT_INFO:
//                    activity.fadeOutInfo();
                    break;
                case HIDE_NAV:
//                    activity.dimStatusBar(true);
                    break;
            }
        }
    };

	private void openBroadcast() {
		// ServerInfo info = getServerInfo();
		// Log.d(TAG, info.toString());
		// String url = getURL(info);
		// Log.d(TAG, String.format("Broadcast Server URL : %s", url));
		if (mLibVLC != null) {
			try {
				String pathUri = LibVLC.getInstance().nativeToURI(
						"Http://192.168.1.34:8080");
				Log.d(TAG, String.format("Path URI : %s", pathUri));
				mLibVLC.readMedia(pathUri, false);
			} catch (LibVlcException e) {
				e.printStackTrace();
			}
		}
	}

	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		super.onConfigurationChanged(newConfig);
	}

	public void setSurfaceSize(int width, int height) {
		mVideoHeight = height;
		mVideoWidth = width;
		Message msg = mHandler.obtainMessage(SURFACE_SIZE);
		mHandler.sendMessage(msg);
	}

	@Override
	public void surfaceChanged(SurfaceHolder holder, int format, int width,
			int height) {
		mLibVLC.attachSurface(holder.getSurface(), BroadcastActivity.this,
				width, height);

	}

	@Override
	public void surfaceCreated(SurfaceHolder holder) {
	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		mLibVLC.detachSurface();

	}

	public void changeSurfaceSize() {
		// get screen size
		int dw = getWindow().getDecorView().getWidth();
		int dh = getWindow().getDecorView().getHeight();

		// getWindow().getDecorView() doesn't always take orientation into
		// account, we have to correct the values
		boolean isPortrait = getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT;
		if (dw > dh && isPortrait || dw < dh && !isPortrait) {
			int d = dw;
			dw = dh;
			dh = d;
		}
		if (dw * dh == 0)
			return;

		// calculate aspect ratio
		double ar = (double) mVideoWidth / (double) mVideoHeight;
		// calculate display aspect ratio
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
			dw = mVideoWidth;
			break;
		}

		surfaceHolder.setFixedSize(mVideoWidth, mVideoHeight);
		LayoutParams lp = surfaceView.getLayoutParams();
		lp.width = dw;
		lp.height = dh;
		surfaceView.setLayoutParams(lp);
		surfaceView.invalidate();
	}
}
