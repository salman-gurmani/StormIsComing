using UnityEngine;

public class FacebookAnalytics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        //if (FB.IsInitialized)
        //{
        //    FB.ActivateApp();
        //}
        //else
        //{
        //    //Handle FB.Init
        //    FB.Init(() => {
        //        FB.ActivateApp();
        //    });
        //}
    }
    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        //if (!pauseStatus)
        //{
        //    //app resume
        //    if (FB.IsInitialized)
        //    {
        //        FB.ActivateApp();
        //    }
        //    else
        //    {
        //        //Handle FB.Init
        //        FB.Init(() => {
        //            FB.ActivateApp();
        //        });
        //    }
        //}
    }
}
