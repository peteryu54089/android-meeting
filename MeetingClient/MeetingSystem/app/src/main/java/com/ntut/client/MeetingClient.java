package com.ntut.client;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;

import android.content.Context;
import android.util.Log;

import com.ntut.meetingInterface.MeetingClientCallback;
import com.ntut.message.CloseMessageHandler;
import com.ntut.message.FileMessageHandler;
import com.ntut.message.IMessage;
import com.ntut.message.MessagePool;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.message.RankMessageHandler;
import com.ntut.message.ScoreMessageHandler;
import com.ntut.message.VoteMessageHandler;
import com.ntut.structure.ServerInfo;

public class MeetingClient {
	private final String TAG = "MeetingClient";
	private Context context = null;
	private ServerInfo info = null;
	private Socket socket = null;
	private Thread thread = null;
	private MeetingInputStream dataInput = null;
	private DataOutput dataOutput = null;
	private MeetingClientCallback callback = null;
	private Thread listenThread = null;

	public MeetingClient(Context context, ServerInfo info) {
		this.info = info;
		this.context = context;
		initMethods();
	}

	private void initMethods() {
		VoteMessageHandler.init();
		ScoreMessageHandler.init();
		FileMessageHandler.init();
		CloseMessageHandler.init();
		RankMessageHandler.init();
	}

	public void start() {
		Log.i(TAG, "Start.");
		thread = new Thread(runnable);
		thread.start();
	}

	public void stop() {
		Log.i(TAG, "Stop.");
		stoptListen();
		disconnect();
	}

	private Runnable runnable = new Runnable() {

		@Override
		public void run() {
			try {
				connect(info.getIp(), info.getServicePort());
			} catch (Exception e) {
				e.printStackTrace();
				notifyOnError(e);
			}
		}
	};

	private void disconnect() {
		if (socket != null) {
			try {
				socket.close();
				socket = null;
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}

	private void connect(String ip, int port) throws UnknownHostException,
			IOException {
		if (socket == null) {
			socket = new Socket(ip, port);
			dataInput = new MeetingInputStream(socket.getInputStream());
			dataOutput = new MeetingOutputStream(socket.getOutputStream());
			startListen();
		}
	}

	private void stoptListen() {
		Log.d(TAG, "stoptListen");
		if (listenThread != null) {
			listenThread.interrupt();
			listenThread = null;
		}
	}

	private void startListen() {
		Log.d(TAG, "startListen");
		if (listenThread == null) {
			listenThread = new Thread(listenRunnable);
			listenThread.start();
		}
	}

	private Runnable listenRunnable = new Runnable() {

		@Override
		public void run() {
			try {
				while (socket != null && listenThread != null
						&& !listenThread.isInterrupted() && dataInput != null) {
					try {
						dataInput.readMessage();

						// int cmd = dataInput.readInt();
						int cmd = dataInput.getCmd();
						Log.d(TAG, String.format("Command %d Received.", cmd));
						if (cmd < ClientMessage.values().length) {
							IMessage message = MessagePool
									.getMessage(ClientMessage.values()[cmd]);
							if (message == null)
								Log.e(TAG, "Message not found.");
							else {
								Object obj = message.receivedMessage((DataInput)dataInput);
								if (obj != null) {
									message.handlerMessage(context, obj);
									notifyOnReceived(
											ClientMessage.values()[cmd], obj);
								}
							}
						}
						// else {
						// stop();
						// start();
						// }
						// }
						// }
					} catch (NumberFormatException e) {
						e.printStackTrace();
					}
				}
			} catch (IOException e) {
				e.printStackTrace();
				notifyOnError(e);
			}
		}
	};

	public void send(ClientMessage clientMessage, Object obj) throws Exception {
		try {
			IMessage message = MessagePool.getMessage(clientMessage);
			message.sendMessage(dataOutput, obj);
		} catch (IOException e) {
			e.printStackTrace();
			notifyOnError(e);
		}
	}

	private void notifyOnError(Exception e) {
		stoptListen();
		disconnect();
		if (callback != null)
			callback.onError(MeetingClient.this, e);
	}

	private void notifyOnReceived(ClientMessage clientMessage, Object obj) {
		if (callback != null)
			callback.onReceiced(clientMessage, obj);
	}

	public void setMeetingClientCallback(MeetingClientCallback callback) {
		this.callback = callback;
	}
}
