using NativeWebSocket;
using UnityEngine;

public class WebHandler : MonoBehaviour
{
    /// **********************************
    ///  SINGLETON
    /// **********************************
    public static WebHandler Instance;
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
    WebSocket ws;
    bool successfullyConnected = false;

    void Start()
    {
        bool useProdServer = ConfigManager.Instance.useProdServer;
        string prodServer = ConfigManager.Instance.prodServer;
        string localServer = ConfigManager.Instance.localServer;
        int connectionTimeoutSeconds = ConfigManager.Instance.connectionTimeoutSeconds;

        ws = new WebSocket(useProdServer ? prodServer : localServer);
        ws.OnOpen += OnConnected;
        ws.OnMessage += OnMessage;

        ws.Connect();
        Invoke(nameof(Timeout), connectionTimeoutSeconds);
    }

    private void OnConnected()
    {
        Debug.Log("Polaczone");
        //NotificationBox.Instance.Display("Połączono z serwerem gry.", NotificationType.Info);
        successfullyConnected = true;

        WebHandler_Core.Instance.try_createSession();
    }

    void Timeout()
    {
        if (successfullyConnected) return;

        Debug.Log("Blad polaczenia");
        ws.Close();
    }

    void OnMessage(byte[] bytes)
    {
        string message = System.Text.Encoding.UTF8.GetString(bytes);
        Debug.Log(message);
        InputMessage inputMessage = JsonUtility.FromJson<InputMessage>(message);
        switch (inputMessage.channelType)
        {
            case "core":
                WebHandler_Core.Instance.HandleInput(message);
                break;
        }
    }



    void OnDestroy()
    {
        ws.Close();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        ws.DispatchMessageQueue();
#endif
    }

    public void Send(string json)
    {
        ws.SendText(json);
    }

    /// **********************************
    ///  OUT EVENTS
    /// **********************************

}
