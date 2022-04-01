package com.unity3d.player;

import android.app.Activity;
import android.util.Log;

/* import com.transsion.gamepay.core.ConfigCallback;
import com.transsion.gamepay.core.PayCallback;
import com.transsion.gamepay.core.PayHelper;
import com.transsion.gamepay.core.PayParams;
import com.transsion.gamepay.core.bean.OrderInfo;
import com.transsion.gamepay.core.bean.ProductConfig; */

import java.util.List;

public class UnityPay {
    private static final String TAG = "UnityPay";

    public static void GetPayConfig() {
        /* PayHelper.getProductConfig(new ConfigCallback() {
            @Override
            public void response(List<ProductConfig> result) {
                Log.d(TAG, "response() called with: result = [" + result + "]");
            }
        }); */
    }

    public static void StartPurchase(Activity activity, String productId, Runnable success, Runnable fail) {
        /* PayHelper.pay(activity, new PayParams.Builder(productId)
                // 如果你有自有的订单号需要传入 ，可通过这个方法传入
                .setCustomizeOrderId("PS")
                // 如果你需要传递额外参数 ，你可以通过这个方法传入
                .setExtra("PS1"), // 支付回调
                new PayCallback() {
                    @Override
                    public void paySuccess(PayParams params, OrderInfo orderInfo) {
                        // 支付成功
                        Log.d(TAG,
                                "paySuccess() called with: params = [" + params + "], orderInfo = [" + orderInfo + "]");
                        success.run();
                    }

                    @Override
                    public void payFailure(PayParams params, OrderInfo orderInfo, int errorCode) {
                        // 支付失败
                        Log.d(TAG, "payFailure() called with: params = [" + params + "], orderInfo = [" + orderInfo
                                + "], errorCode = [" + errorCode + "]");
                        fail.run();
                    }
                }); */
    }
}
