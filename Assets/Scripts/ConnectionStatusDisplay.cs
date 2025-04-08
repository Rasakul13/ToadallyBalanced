using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConnectionStatusDisplay : MonoBehaviour
{
    public Sprite connectedSprite;
    public Sprite disconnectedSprite;

    private Image imageComponent;
    private bool? lastConnectionState = null;

    void Awake() 
    {
        imageComponent = GetComponent<Image>();
        imageComponent.enabled = false;
    }

    void Start()
    {
        UpdateConnectionStatus();
        InvokeRepeating(nameof(UpdateConnectionStatus), 0.5f, 0.5f); // Check every 0.5s

        StartCoroutine(DelayedInitialCheck());
    }

    void UpdateConnectionStatus()
    {
        if (UdpManager.Instance == null)
        {
            Debug.LogWarning("UdpManager.Instance is null!");
            return;
        }

        bool isConnected = UdpManager.Instance.IsConnected;

        if (lastConnectionState == null || isConnected != lastConnectionState.Value)
        {
            imageComponent.sprite = isConnected ? connectedSprite : disconnectedSprite;
            
            RectTransform rt = imageComponent.rectTransform;
            if (isConnected)
            {
                rt.sizeDelta = new Vector2(50f, 40f);
            }
            else
            {
                rt.sizeDelta = new Vector2(50f, 50f);
            }

            lastConnectionState = isConnected;

            Debug.Log("Connection status updated. Connected: " + isConnected);
        }
    }

    private IEnumerator DelayedInitialCheck()
    {
        yield return new WaitForSeconds(0.6f);

        imageComponent.enabled = true; // Show image
    }
}
