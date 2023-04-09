using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    public static event Action<bool> InternetAvailabilityChanged;

    [SerializeField] private string IP1 = "1.1.1.1"; // Cloudflare
    [SerializeField] private string IP2 = "8.8.8.8"; // google-public-dns-a.google.com.
    [SerializeField] private string IP3 = "8.8.4.4"; // google-public-dns-b.google.com

    [SerializeField] private float CheckDelay = 2f;
    [SerializeField] private float MaxResponseTime = 3f;

    private static bool
        _internetAvailable =
            true; // We make it true from start intentionally (to not prevent all systems from initialization)

    private bool _exInternetStatus = true;
    private bool _isRunning = false;

    public static bool InternetAvailable => _internetAvailable;

    public void Init()
    {
        if (!_isRunning)
            StartCoroutine(CheckConnection());
    }

    private void Update()
    {
        if (!_isRunning)
            StartCoroutine(CheckConnection());
    }

    private void CheckExStatus()
    {
        if (_exInternetStatus != InternetAvailable)
        {
            _exInternetStatus = InternetAvailable;
            InternetAvailabilityChanged?.Invoke(InternetAvailable);
        }
    }

    private IEnumerator CheckConnection()
    {
        _isRunning = true;
        var success = false;

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            var ping1 = new Ping(IP1);
            var ping2 = new Ping(IP2);
            var ping3 = new Ping(IP3);
            var exitTime = Time.unscaledTime + MaxResponseTime;
            yield return null;

            while (Time.unscaledTime < exitTime)
            {
                if (ping1.isDone || ping2.isDone || ping3.isDone)
                {
                    success = true;
                    break;
                }

                yield return new WaitForSecondsRealtime(0.05f);
            }
        }

        _internetAvailable = success;
        CheckExStatus();

        yield return new WaitForSecondsRealtime(CheckDelay);

        _isRunning = false;
    }
}