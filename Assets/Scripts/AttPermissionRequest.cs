using UnityEngine;
using System.Collections;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class AttPermissionRequest : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(PromptATT());
    }

    IEnumerator PromptATT()
    {
        yield return new WaitForSeconds(2.0f);

#if UNITY_IOS
        // Check the user's consent status.
        // If the status is undetermined, display the request request:
        var AttStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        Debug.LogError($"ATT status: {AttStatus.ToString()}");
        if (AttStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif
    }

}