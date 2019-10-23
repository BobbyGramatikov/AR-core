using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
public class AnchorManager : MonoBehaviour
{

    public GameObject anchoredPrefab;
    public GameObject unanchoredPrefab;

    public Anchor anchor;

    Vector3 lastAnchoredPosition;
    Quaternion lastAnchoredRotation;
    // Start is called before the first frame update
    void Start()
    {
        QuitOnConnectionErrors();


    }

    //checks if camera permissions are granted. 
    void QuitOnConnectionErrors()
    {
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                "Camera permission is needed to run this application.", 5));
        }
        else if (Session.Status.IsError())
        {
            // This covers a variety of errors.  See reference for details
            // https://developers.google.com/ar/reference/unity/namespace/GoogleARCore
            StartCoroutine(CodelabUtils.ToastAndExit(
                "ARCore encountered a problem connecting. Please restart the app.", 5));
        }
    }

    void Update()
    {
        // The session status must be Tracking in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }






    //public touch touch;

    //public float x;

    //public float y;

    //// Update is called once per frame
    //void Update()
    //{

    //    Session.GetTrackables<DetectedPlane>(temp);

    //    Frame.Raycast(x, y, filter, out TrackableHit hitResult);

    //    TrackableHit hit;
    //    TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;
    //    if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
    //    {
    //        // Check if the out hit is a detected plane
    //        if ((hit.Trackable is DetectedPlane))
    //        {
    //            //Add the virtual object
    //        }
    //    }

    //    hit.Pose.position; //Position of the incidence 


    //    hit.Pose.rotation; //Rotation of the incidence of the ray to plane
    //                       //rotation of the plane on to which the ray cast 
    //    hits((DetectedPlane)hit.Trackable).CenterPose.rotation;


    //    //For euler to quaternion
    //    Quaternion.Euler(x, y, z); // where x,y,z are euler angles
    //                               //For getting euler angles from the quaternion variable
    //    Quaternion q;
    //    q.eulerAngles.x; //get x rotation 
    //    q.eulerAngles.y; //get y rotation
    //    q.eulerAngles.z; //get z rotation


    //    var anchor = hit.Trackable.CreateAnchor(hit.Pose);                     // Make Andy model a child of the anchor.                   
    //    andyObject.transform.parent = anchor.transform;


    //    //anchor.getPose();
    //    // Debug.Log("no touch found");

    //    if (Input.touchCount >= 0)
    //    {
    //        Debug.Log("touch found");


    //        Session.CreateAnchor(transform.position, transform.rotation);
    //        GameObject.Instantiate(anchoredPrefab,
    //            anchor.transform.position,
    //            anchor.transform.rotation,
    //            anchor.transform);
    //        GameObject.Instantiate(unanchoredPrefab,
    //            anchor.transform.position,
    //            anchor.transform.rotation);
    //        lastAnchoredPosition = anchor.transform.position;
    //        lastAnchoredRotation = anchor.transform.rotation;
    //    }
    //    if (anchor == null)
    //    {
    //        return;
    //    }
    //    if (anchor.transform.position != lastAnchoredPosition)
    //    {
    //        Debug.Log(Vector3.Distance(anchor.transform.position, lastAnchoredPosition));
    //    }
    //    if (anchor.transform.rotation != lastAnchoredRotation)
    //    {
    //        Debug.Log(Quaternion.Angle(anchor.transform.rotation, lastAnchoredRotation));
    //        lastAnchoredRotation = anchor.transform.rotation;
    //    }
    //    if (Input.touchCount == 0)
    //    {
    //        return;

    //    }
    //}
}
