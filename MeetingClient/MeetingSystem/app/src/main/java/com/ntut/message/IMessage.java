package com.ntut.message;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;

import android.content.Context;

public interface IMessage {
    void sendMessage(DataOutput dataOutput, Object obj) throws IOException;
    void handlerMessage(Context context, Object obj);
    Object receivedMessage(DataInput dataInput) throws IOException;
}
