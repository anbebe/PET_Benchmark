using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading;

public class VisualRepresentation : MonoBehaviour
{
    private VisualElement _cue;
    [SerializeField] private GameObject _player;
    private GameObject _obj;
    private float _horizontal_radius;
    private float _vertical_radius;
    

    void Start()
    {
        _player = GameObject.Find("Player");
        _cue.style.backgroundColor = new StyleColor(new Color(1, 1, 0f));
        _cue.style.color = new StyleColor(new Color(1, 1, 0f));
        
        // radius = Mathf.Min(Screen.width/2, Screen.height/2);
        _horizontal_radius = Screen.width/2 - 20;
        _vertical_radius = Screen.height / 2 - 20;
        
        FindObject();
        //StartCoroutine(Move());
    }

    void Update()
    {
        if (_obj != null) // _obj != null
        {
            // get position of object
            var _pos = _obj.transform.position;
            
            // get position on the ring for the visual element dependent on positions
            var _own_pos =  _player.transform.position;
            var heading = _pos - _own_pos;
            heading.y = 0;  // This is the overground heading.
            var distance = heading.magnitude;
            
            // get angle between default and current head rotation
            Vector2 default_head = new Vector2(0f, 1f);
            Vector2 camera_head = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
            float angle = Vector2.Angle(default_head, camera_head);
            angle = angle * Mathf.Deg2Rad;
            
            var direction = heading / distance;
            Debug.Log(direction);
            //direction = direction;
            double radians = Math.Atan2(direction.z, direction.x);

            //substract camera heading from direction calculated from a standard position
            //double radian_head = Math.Atan2(Camera.main.transform.forward.z -1, Camera.main.transform.forward.x);
            //Debug.Log(radian_head);
            //radians  += radian_head;
            float rad = (float)radians - angle + Mathf.PI/2;

            float y = (Screen.height/2- 20) + _vertical_radius * Mathf.Cos(rad);
            float x = (Screen.width/2 - 20) + _horizontal_radius * Mathf.Sin(rad);

            var z = _cue.transform.position.z;
            _cue.transform.position = new Vector3(x, y, z);
            
            // distance
            // get current values
            float _r = _cue.style.color.value.r;
            float _b = _cue.style.color.value.b;
            // map from distance (clip to max 4?) to color range (0,1)
            distance = Mathf.Clamp(distance, 0f, 4f);
            distance = distance / 4; // should be between 0 and 1
            _cue.style.color = new StyleColor(new Color(_r, distance, _b));
            _cue.style.backgroundColor = new StyleColor(new Color(_r, distance, _b));
        }
        else
        {
            FindObject();
        }
    }

    private void FindObject()
    {
        // .GetComponent<ObjectInstantiator>().instantiatedObj
        _obj = GameObject.FindGameObjectWithTag("MovingObject");
    }

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();
        Debug.Log("Loaded UI");
        
        var _rootVisualElement = uiDocument.rootVisualElement;

        // Get the  visual element from the ui
        _cue = _rootVisualElement.Q<VisualElement>(name: "cue");
    }

    private IEnumerator Move()
    {
        /*
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            var pos_x = _cue.transform.position.x;
            var pos_y = _cue.transform.position.y;
            var pos_z = _cue.transform.position.z;
            _cue.transform.position = new Vector3(pos_x, pos_y + 10f, pos_z);
            float _r = _cue.style.color.value.r;
            float _g = _cue.style.color.value.g;
            float _b = _cue.style.color.value.b;
            _cue.style.color = new StyleColor(new Color(_r, _g - 0.1f, _b));
            _cue.style.backgroundColor = new StyleColor(new Color(_r, _g - 0.1f, _b));
        }
        */
        var z = _cue.transform.position.z;
        _cue.transform.position = new Vector3((float) Screen.width/2, (float) Screen.height/2, z);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 360; i += 20)
        {
            yield return new WaitForSeconds(1f);
            float y = (Screen.height/2- 20) + _vertical_radius * Mathf.Cos(i * Mathf.Deg2Rad);
            float x = (Screen.width/2 - 20 )+ _horizontal_radius * Mathf.Sin(i * Mathf.Deg2Rad);
            
            z = _cue.transform.position.z;
            _cue.transform.position = new Vector3(x, y, z);
            Debug.Log("post position: " + x + " " + y + " " + z);
        }
        
    }



}

