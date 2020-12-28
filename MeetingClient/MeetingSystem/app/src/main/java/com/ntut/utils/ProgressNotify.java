package com.ntut.utils;

import java.math.BigInteger;

public interface ProgressNotify {
	void notifyProgressValue(int current,BigInteger totalLength);
}
