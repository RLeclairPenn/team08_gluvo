using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System.Runtime.InteropServices;

public class BtAndDebugScript : MonoBehaviour
{
    public Text text;
    string messageDisplayed;

    const string pluginName = "com.gluvo.unity.MyPlugin";

    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;

    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
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
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstance;
        }
    }

    BluetoothDevice tempDevice;

    //  NEED TO ASSIGN THE TEXT VARIABLE FROM THE UNITY EDITOR IF NOT
    //  ASSIGNED ALREADY

    void Start()
    {
        string resultSearch = findDevice("ESP32test");
        DisplaySingleLine(resultSearch);

        string resultConnect = connectToDevice();
        DisplaySingleLine(resultConnect);

        string resultStream = setupOutputStream();
        DisplaySingleLine(resultStream);

        //StartCoroutine(sendMessages());

    }

    IEnumerator sendMessages()
    {

        string resultMsg = sendMessage("Hello!");
        DisplaySingleLine(resultMsg);

        //yield return new WaitForSeconds(1);

        resultMsg = sendMessage("Hello 2!");
        DisplaySingleLine(resultMsg);

        //yield return new WaitForSeconds(1);

        resultMsg = sendMessage("Hello 3!");
        DisplaySingleLine(resultMsg);

        yield return new WaitForSeconds(1);
    }

    public void DisplaySingleLine(string msg)
    {
        ResetMsg();
        AppendToMessage(msg);
    }

    public void AppendToMessage(string msg)
    {
        messageDisplayed += msg;
        messageDisplayed += '\n';

        UpdateDisplayedText();
    }

    public void ResetMsg()
    {
        messageDisplayed = "";
        UpdateDisplayedText();
    }

    void UpdateDisplayedText()
    {
        text.text = messageDisplayed;
    }

    double getNumber()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<double>("getMsg");
        }
        AppendToMessage("Not in correct platform");
        return 0;
    }

    string findDevice(string device_name)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("findDevice", device_name);
        }
        AppendToMessage("Not in correct platform");
        return "Failed";
    }

    string connectToDevice()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("connectToDevice");
        }
        AppendToMessage("Not in correct platform");
        return "Failed";
    }

    string setupOutputStream()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("setupOutputStream");
        }
        AppendToMessage("Not in correct platform");
        return "Failed";
    }

    public string sendMessage(string msg)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("sendMessage", msg);
        }
        AppendToMessage("Not in correct platform");
        return "Failed";
    }

    string closeConnection()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return PluginInstance.Call<string>("closeConnection");
        }
        AppendToMessage("Not in correct platform");
        return "Failed";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
