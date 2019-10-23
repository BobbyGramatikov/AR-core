using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore.Examples.Common;
using GoogleARCore;
using UnityEngine.UI;



public class ButtonInstructions : MonoBehaviour
{
    public int IndexOfObjectToBeCreated = 0;
    //Array of Objects Prefabs


    //list of game objects
    List<GameObject> unityGameObjects = new List<GameObject>();

    public Dropdown ddInstructionSet;

    //Game object that contains everything on load
    public GameObject[] objects;

    //Buttons
    public GameObject btnCreateObject; 
    public GameObject btnShowObjects;
    public GameObject btnSelectItem;

    //camera to be used for the ray casting
    public Camera cam;

    public bool ObjectIsSelected = false;
    List<string> InstructionSets = new List<string>() {"Please select an item", "Adding Paper", "Change car oil" };
    //Layer that holds the created objects which will collide with the ray cast
    public int layerMask = 0xffffff;

    public Touch touch;

    public GameObject FunctionInputGameObject;

    //Declaration of the generative objects
    public GameObject generativeCube;
    public GameObject generativeSphere;
    public GameObject generativeQuad;

    //public Anchor anchor;

    private float NewGOPositionShift = -25f;

    private readonly float MOVING_STEP = 0.1f;

    public Text ShowCube;
    public Text selectedSet;

    

    // Start is called before the first frame update

    //public Anchor createAnchor(Pose pose) {

    void Start()
    {
        //    anchor = new Anchor();
        //    anchor.getPose();
        //    Pose p;
        //    generativeCube.create
        PopulateList();
       
    }


    public void Dropdown_IndexChanged(int index)
    {
        selectedSet.text = InstructionSets[index];
        Debug.Log("index " + index );
    }

        public void PopulateList()
    {
        ddInstructionSet.AddOptions(InstructionSets);
    }

    // Update is called once per frame
    void Update()
    {
       

        //    anchor.add(session.createAnchor(
        //frame.getCamera().getPose()
        //    .compose(Pose.makeTranslation(0, 0, -1f))
        //    .extractTranslation()))


        //Checks if there has been a touch on the display if not exits update function. 
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        //Checks if there has been a mouse click on the display if not exits update function. 
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            //Create ray and RaycastHit objects
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //Creates a vector containing x,y,z axis 
            Vector3 v = cam.ScreenToViewportPoint(Input.mousePosition);


            Debug.Log("Ray " + ray.ToString());

            //If no object has been selected 
            if (!ObjectIsSelected)
            {
                //check if the ray collides with the objects 
                if (Physics.Raycast(ray, out hit, layerMask))
                {
                    if (hit.transform.gameObject.layer == 0
                        && hit.transform.gameObject.GetComponent<Button>() == null && hit.distance <= 100f)
                    {
                        FunctionInputGameObject = hit.transform.gameObject;
                        // && hit.distance <= 0.20f and this to the if statment fo r the distance 
                        SelectedGameObject = FunctionInputGameObject.name;
                        Debug.Log("First parent  " + FunctionInputGameObject.transform.parent);

                        Debug.Log("Selected object was : " + SelectedGameObject);

                        var Scale = FunctionInputGameObject.transform.localScale;

                        // Find the ARCamera in the scene
                        //GameObject ARCameraGameObject = GameObject.Find("ARCamera");

                        GameObject ARCameraGameObject = GameObject.Find("Main Camera");

                        //Set the parent to be Camera
                        Debug.Log("parent  " + FunctionInputGameObject.transform.parent);

                        FunctionInputGameObject.transform.parent = ARCameraGameObject.transform;

                        //Preserve Scale
                        FunctionInputGameObject.transform.localScale = Scale;

                        //Center the gameobject in the camera frame. Only put it few cm away
                        FunctionInputGameObject.transform.localPosition = new Vector3(0f, 0f, 25f);

                        Debug.Log("layer " + FunctionInputGameObject.layer);
                        Debug.Log("name " + FunctionInputGameObject.name);
                        Debug.Log("transform " + FunctionInputGameObject.transform);
                        Debug.Log("localPosition " + FunctionInputGameObject.transform.localPosition);
                        Debug.Log("Scale " + Scale);
                        Debug.Log("---------------------------------------------");
                        ObjectIsSelected = true;


                        //Change the color of the selected object while setting the color for all other objects to default
                        //HighlightSelectedObject(SelectedGameObject);
                    }
                    else //Ray cast doesn't collide with any objects
                    {
                        //DeselectObject();
                    }
                }
            }
            else
            {

                if (v.z >= 0)
                {
                    v.z = 10;
                }

                Debug.Log("name " + FunctionInputGameObject.name);

                FunctionInputGameObject.transform.localPosition = v;
                Debug.Log("layer " + FunctionInputGameObject.layer);
                Debug.Log("name " + FunctionInputGameObject.name);
                Debug.Log("transform " + FunctionInputGameObject.transform);
                Debug.Log("localPosition " + FunctionInputGameObject.transform.localPosition);
                Debug.Log("scale " + FunctionInputGameObject.transform.localScale);
                Debug.Log("---------------------------------------------");

                ObjectIsSelected = false;

                //Preserve Scale
                // FunctionInputGameObject.transform.localScale = Scale;
                //Set the parent to be Camera
                //FunctionInputGameObject.transform.parent = ARCameraGameObject.transform;
            }
        }
    }

    private string SelectedGameObject;

    private void OnGUI()
    {
        foreach (GameObject go in objects)
        {
            bool active = GUILayout.Toggle(go.activeSelf, go.name);
            if (active != go.activeSelf)
            {
                go.SetActive(active);
            }
            // GUILayout.Toggle(go.activeSelf, go.name);
        }

    }

    public void ShowObjects()
    {
        Debug.Log("our button was clicked");

        foreach (GameObject go in objects)
        {

            if (go.activeSelf != true)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
        foreach (GameObject go in unityGameObjects)
        {
            if (go.activeSelf != true)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }

    public void CreateObjectt()
    {
        //Create new GameObject
        GameObject NewGameObject;

        //FIND THE SELECTED TYPE TO BE CREATED
        //The index of the object to be created, 
        //0: Cube
        //1: Sphere

        //INSTANTIATE NEW OBJECT
        NewGameObject = Instantiate(objects[IndexOfObjectToBeCreated]);

        //Set the local position and add a shift in case you create multiple objects at the same place
        NewGameObject.transform.localPosition = new Vector3(NewGOPositionShift, 150f, 2f);

        //Increment the PositionShift to avoid multiple objects on the same spot when creating another object
        NewGOPositionShift = NewGOPositionShift + -2;

        //set every NewGameObject in layer 9 to make sure that they are the only collidable objects in the scene when raycasting 
        NewGameObject.layer = 0;

        unityGameObjects.Add(NewGameObject);


        // DistinctiveObjectData distinctiveObjectData = NewGameObject.GetComponent<DistinctiveObjectData>();
        //distinctiveObjectData.type = IndexOfObjectToBeCreated;
    }

    int i = 0;
    public void ChangeSelectedITem()
    {
        i++;

        if (i > 3)
        {
            i = 0;
        }
        if (i == 0)
        {
            btnSelectItem.GetComponentInChildren<Text>().text = "Cube";
            IndexOfObjectToBeCreated = 0;
        }
        else if (i == 1)
        {
            btnSelectItem.GetComponentInChildren<Text>().text = "Sphere";
            IndexOfObjectToBeCreated = 1;
            
        }
        else
        {

            btnSelectItem.GetComponentInChildren<Text>().text = "The Quad";
            IndexOfObjectToBeCreated = 3;
        }
    }



}

