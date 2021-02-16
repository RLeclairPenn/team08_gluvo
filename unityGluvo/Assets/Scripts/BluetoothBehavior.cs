using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System.Runtime.InteropServices;

public class BluetoothBehavior : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {        
        text = GetComponent<Text>();
        AppendToMessage("Hello There!");
        AppendToMessage("This is a test :)");
        double testDouble = getNumber();
        AppendToMessage(testDouble.ToString());
        AppendToMessage("Trying to find the ESP32");
        string resultSearch = findDevice("ESP32test");
        AppendToMessage(resultSearch);

        string resultConnect = connectToDevice();
        AppendToMessage(resultConnect);

        string resultStream = setupOutputStream();
        AppendToMessage(resultStream);

        StartCoroutine(sendMessages());        

    }

    IEnumerator sendMessages()
    {
        
        string resultMsg = sendMessage("Hello!");
        AppendToMessage(resultMsg);

        //yield return new WaitForSeconds(1);

        resultMsg = sendMessage("Hello 2!");
        AppendToMessage(resultMsg);

        //yield return new WaitForSeconds(1);

        resultMsg = sendMessage("Hello 3!");
        AppendToMessage(resultMsg);

        yield return new WaitForSeconds(1);
    }

    void AppendToMessage(string msg)
    {
        messageDisplayed += msg;
        messageDisplayed += '\n';

        UpdateDisplayedText();
    }

    void ResetMsg()
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

    string sendMessage(string msg)
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
