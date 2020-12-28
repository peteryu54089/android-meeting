package com.ntut.message;

import java.util.Hashtable;

public class MessagePool {
	public static enum ClientMessage {
		ScoreMessage, VoteMessage, FileMessage, RankMessage, Close,  Abort
	};

	private static Hashtable<ClientMessage, IMessage> messages = new Hashtable<ClientMessage, IMessage>();

	public static void registerMessage(ClientMessage command, IMessage message) {
		messages.put(command, message);
	}

	public static void unregisterMessage(ClientMessage command, IMessage message) {
		messages.remove(command);
	}

	public static IMessage getMessage(ClientMessage command) {
		return (IMessage) messages.get(command);
	}
}
