using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public readonly bool useProdServer = false;
    public readonly string prodServer = "ws://0.0.0.0:80";
    public readonly string localServer = "ws://localhost:80";
    public readonly int connectionTimeoutSeconds = 3;

    /// **********************************
    ///  SINGLETON
    /// **********************************
    public static ConfigManager Instance;
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
}
