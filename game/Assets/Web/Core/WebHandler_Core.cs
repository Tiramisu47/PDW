using UnityEngine;

public class WebHandler_Core : MonoBehaviour
{
    /// **********************************
    ///  SINGLETON
    /// **********************************
    public static WebHandler_Core Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// **********************************
    public void HandleInput(string message)
    {
        InputMessage inputMessage = JsonUtility.FromJson<InputMessage>(message);
        switch (inputMessage.eventType)
        {
            case "source_joinSessionS":
                source_joinSessionS(message);
                break;
            case "other_toggleElementStateS":
                other_toggleElementStateS(message);
                break;
            case "other_rangeElementStateS":
                other_rangeElementStateS(message);
                break;
            default:
                break;
        }
    }

    /// **********************************
    ///  INCOMING EVENTS
    /// **********************************

    void source_joinSessionS(string message)
    {
        CORE_source_joinSessionS source_joinRoomS_message = JsonUtility.FromJson<CORE_source_joinSessionS>(message);
        Debug.Log(source_joinRoomS_message.sessionToken);
    }

    void other_toggleElementStateS(string message)
    {
        CORE_other_toggleElementStateS other_toggleElementStateS_message = JsonUtility.FromJson<CORE_other_toggleElementStateS>(message);
        ToggleElement[] list = FindObjectsOfType<ToggleElement>();
        foreach (ToggleElement element in list)
        {
            if (element.uid == other_toggleElementStateS_message.elementUid)
            {
                element.Toggle();
                break;
            }
        }
    }

    void other_rangeElementStateS(string message)
    {
        CORE_other_rangeElementStateS other_rangeElementStateS_message = JsonUtility.FromJson<CORE_other_rangeElementStateS>(message);
        RangeElement[] list = FindObjectsOfType<RangeElement>();
        foreach (RangeElement element in list)
        {
            if (element.uid == other_rangeElementStateS_message.elementUid)
            {
                element.NewValue(other_rangeElementStateS_message.value);
                break;
            }
        }
    }


    /// **********************************
    ///  OUTCOMING EVENTS
    /// **********************************

    public void try_createSession()
    {
        var CORE_try_leaveRoom_json = new CORE_try_createSession
        {
            eventType = "try_createSession",
            channelType = "core",
        };

        string json = JsonUtility.ToJson(CORE_try_leaveRoom_json);
        WebHandler.Instance.Send(json);
    }

}
