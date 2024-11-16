using UnityEngine;
using UnityEngine.Android;


public class LocationService : MonoBehaviour
{
    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("Location services are not enabled.");
                return;
            }
        }
    }

    private void Update()
    {
        while (Application.platform == RuntimePlatform.Android)
        {
            while (Input.location.isEnabledByUser)
            {
                Input.location.Start();

                while (Input.location.status == LocationServiceStatus.Running)
                {
                    double latitude = Input.location.lastData.latitude;
                    double longitude = Input.location.lastData.longitude;

                    Debug.Log("Latitude: " + latitude.ToString() + ", Longitude: " + longitude.ToString());

                    Input.location.Stop();
                }
            }
        }

        while (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            while (Input.location.isEnabledByUser)
            {
                Input.location.Start();

                while (Input.location.status == LocationServiceStatus.Running)
                {
                    double latitude = Input.location.lastData.latitude;
                    double longitude = Input.location.lastData.longitude;

                    Debug.Log("Latitude: " + latitude.ToString() + ", Longitude: " + longitude.ToString());

                    Input.location.Stop();
                }
            }
        }
    }
}
