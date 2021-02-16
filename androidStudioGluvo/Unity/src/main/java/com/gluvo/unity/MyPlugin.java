package com.gluvo.unity;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.util.Log;

import java.io.IOException;
import java.io.OutputStream;
import java.util.Set;
import java.util.UUID;

public class MyPlugin {
    private static final MyPlugin ourInstance = new MyPlugin();

    public static MyPlugin getInstance() { return ourInstance; }

    private double msg;
    private MyPlugin() {
        msg = 2.0;
    }

    public double getMsg() {
        return msg;
    }

    private static final String LOGTAG = "GLUVO";
    private static BluetoothDevice esp32;
    private static BluetoothSocket esp32_socket;
    private static OutputStream out_stream;


    private String failureMsg(String msg) {
        Log.i(LOGTAG, msg);
        return msg;
    }

    public String findDevice(String deviceName) {
        BluetoothAdapter bt_adapter = BluetoothAdapter.getDefaultAdapter();
        if (bt_adapter == null) {
            return failureMsg("Unable to get default adapter");
        }

        if (!bt_adapter.isEnabled()) {
            return failureMsg("Bluetooth is not enabled.");
        }

        Set<BluetoothDevice> bonded_devices = bt_adapter.getBondedDevices();
        if (bonded_devices.isEmpty()) {
            return failureMsg("There are no bonded devices");
        } else {
            boolean found = false;
            for (BluetoothDevice bt_device : bonded_devices) {
                if (bt_device.getName().equals(deviceName)) {
                    found = true;
                    esp32 = bt_device;
                }
            }
            if (!found) {
                return failureMsg("Could not find specified device");
            }
        }
        return "Successfully found device";
    }

    public String connectToDevice() {
        if (esp32 == null) {
            return failureMsg("There is no device to connect to");
        }
        try {
            UUID uuid = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"); //Standard SerialPortService ID
            esp32_socket = esp32.createRfcommSocketToServiceRecord(uuid);
            esp32_socket.connect();
        } catch (IOException e) {
            return failureMsg(e.getMessage());
        }
        return "Successfully connected to device";
    }

    public String setupOutputStream() {
        try {
            out_stream = esp32_socket.getOutputStream();
            return "Output stream setup!";
        } catch (IOException e) {
            return failureMsg((e.getMessage()));
        }
    }

    public String sendMessage(String msg) {
        try {
            out_stream.write(msg.getBytes());
            return "Successfully wrote to device";
        } catch (IOException e) {
            return failureMsg(e.getMessage());
        }
    }

    public String closeConnection() {
        try {
            out_stream.close();
            esp32_socket.close();
            return "Closed connections...";
        } catch (IOException e) {
            return failureMsg(e.getMessage());
        }
    }
}
