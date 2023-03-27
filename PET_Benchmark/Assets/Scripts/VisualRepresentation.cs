using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading;
using Unity.VisualScripting;
using Image = UnityEngine.UI.Image;

public class VisualRepresentation : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image image;
    private GameObject _obj;
    private RectTransform rectTransform;
    private float _horizontal_radius;
    private float _vertical_radius;
    

    void Start()
    {
        _player = GameObject.Find("Player");
        //img.color = new Color(1, 1, 0f);
        
        Debug.Log("center: " + _canvas.pixelRect.center);
        Debug.Log("height: " + _canvas.pixelRect.height/_canvas.referencePixelsPerUnit);
        Debug.Log("width: " + _canvas.pixelRect.width/_canvas.referencePixelsPerUnit);
        
        Debug.Log("pixels per unit" + _canvas.referencePixelsPerUnit);
        
        // radius = Mathf.Min(Screen.width/2, Screen.height/2);
        _horizontal_radius = _canvas.pixelRect.center.x/_canvas.referencePixelsPerUnit - 0.5f;
        _vertical_radius = _canvas.pixelRect.center.y/_canvas.referencePixelsPerUnit - 1f;

        rectTransform = image.GetComponent<RectTransform>();

        Debug.Log("x radius" + rectTransform.transform.position);
        Debug.Log("y radius" + image.transform.localPosition);

        var pos_x = 0;
        var pos_y = 0;
        var pos_z = image.transform.localPosition.z;
        image.transform.localPosition = new Vector3(pos_x, pos_y, pos_z);
        
        
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
            double radians = Math.Atan2(direction.z, direction.x);

            float rad = (float)radians - Mathf.PI - angle ;
            
            Debug.Log("radian: " + radians);

            float y =  - _vertical_radius * Mathf.Sin(rad);
            float x = - _horizontal_radius * Mathf.Cos(rad);

            var z = image.transform.localPosition.z;
            image.transform.localPosition = new Vector3(x, y, z);
            
            // distance
            // get current values
            float _r = image.color.r;
            float _b = image.color.b;
            
            // map from distance (clip to max 4?) to color range (0,1)
            distance = Mathf.Clamp(distance, 0f, 4f);
            distance = distance / 4; // should be between 0 and 1
            image.color = new Color(_r, distance, _b);
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

    private IEnumerator Move()
    {
        /*for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            var pos_x = rectTransform.transform.position.x;
            var pos_y = rectTransform.transform.position.y;
            var pos_z = rectTransform.transform.position.z;
            Debug.Log("post position: " + pos_x + " " + pos_y + " " + pos_z);
            rectTransform.transform.position = new Vector3(pos_x + 0.1f, pos_y, pos_z);
            float _r = image.color.r;
            float _g = image.color.g;
            float _b = image.color.b;
            image.color = new Color(_r, _g - 0.1f, _b);
            //_cue.style.backgroundColor = new StyleColor(new Color(_r, _g - 0.1f, _b));
        }*/
        
        var z = image.transform.localPosition.z;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 360; i += 20)
        {
            yield return new WaitForSeconds(1f);
            float y =  -_vertical_radius * Mathf.Sin(i * Mathf.Deg2Rad + Mathf.PI);
            float x = - _horizontal_radius * Mathf.Cos(i * Mathf.Deg2Rad+ Mathf.PI);
            image.transform.localPosition = new Vector3(x, y, z);
            Debug.Log("post position: " + x + " " + y + " " + z);
        }
        
    }



}

