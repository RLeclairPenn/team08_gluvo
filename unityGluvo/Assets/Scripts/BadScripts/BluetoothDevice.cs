using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Runtime.InteropServices;

public class BluetoothDevice
{
    const string plugin_name = "com.gluvo.unity.MyPlugin";
    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;

    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(plugin_name);
            }
            return _pluginClass;
        }
    }

    public static AndroidJavaObject PluginInstance
    {
        get
        {
            if (_pluginInstance == null)
            {
                _pluginInstance = PluginInstance.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstance;
        }
    }

    /// <summary>
    /// Creates a new Bluetooth Device object, must be given a string name to work
    /// this device must be connected through Oculus on it's bluetooth settings
    /// </summary>
    /// <param name="device_name"></param>

    public BluetoothDevice(string device_name)
    {
        findDevice(device_name);
        connectToDevice();
    }

    public string findDevice(string device_name)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("findDevice", device_name);
        }
        return "Failed";
    }

    public string connectToDevice()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("connectToDevice");
        }
        return "Failed";
    }

    public string setupOutputStream()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("setupOutputStream");
        }
        return "Failed";
    }

    public string sendMessage(string msg)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("sendMessage", msg);
        }
        return "Failed";
    }

    public string closeConnection()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("closeConnection");
        }
        return "Failed";
    }
}
