package com.ntut.meetingInterface;

import com.ntut.client.MeetingClient;
import com.ntut.message.MessagePool.ClientMessage;

public interface MeetingClientCallback {
	public void onError(MeetingClient client,Exception e);
	public void onReceiced(ClientMessage clientMessage,Object obj);
}
