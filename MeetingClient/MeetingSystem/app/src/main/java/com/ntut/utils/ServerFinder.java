package com.ntut.utils;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.SocketException;

import android.util.Log;

import com.ntut.meetingInterface.ServerCallback;
import com.ntut.structure.ServerInfo;

public class ServerFinder {
	private final String TAG = "ServerFinder";
	private final int port = 3389;
	private DatagramSocket datagramSocket = null;
	private Thread thread = null;
	private ServerCallback callback = null;

	public ServerFinder() {
	};

	public void start() {
		if (datagramSocket == null) {
			try {
				datagramSocket = new DatagramSocket(port);
				datagramSocket.setBroadcast(true);
			} catch (SocketException e) {
				e.printStackTrace();
			}
		}
		if (thread == null) {
			thread = new Thread(runnable);
			thread.start();
		}
	}

	public void stop() {
		if (datagramSocket != null) {
			datagramSocket.close();
			datagramSocket = null;
		}
		if (thread != null) {
			thread.interrupt();
			thread = null;
		}
	}

	public void setServerCallback(ServerCallback callback) {
		this.callback = callback;
	}

	private void notifyOnServerFinder(ServerInfo info) {
		if (callback != null)
			callback.onServerFinded(info);
	}

	private Runnable runnable = new Runnable() {

		@Override
		public void run() {
			try {
				byte[] data = new byte[1024];
				Thread.sleep(50);
				while (thread != null && datagramSocket != null
						&& !thread.isInterrupted()) {
					DatagramPacket datagramPacket = new DatagramPacket(data,
							data.length);
					datagramSocket.receive(datagramPacket);
					String jsonStr = new String(datagramPacket.getData(), 0,
							datagramPacket.getLength());
					Log.d(TAG, "Json String : " + jsonStr + " Received.");
					ServerInfo info = (ServerInfo) GsonUtils.JsonToObject(jsonStr,ServerInfo.class);
					notifyOnServerFinder(info);
				}
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	};
}
