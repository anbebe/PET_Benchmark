using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class VibrationTest : MonoBehaviour
{
    
    private BhapticsSettings currentSettings;
    private int[] motors = new int[32] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    private int[] testMotors = new int[32] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    private float distance = 0.0f;
    private float timer = 0.0f;
    [SerializeField] private LayerMask sensorLayer;

    // Start is called before the first frame update
    void Start()
    {
        currentSettings = BhapticsSettings.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (timer > -0.5f)
        {
            timer += Time.deltaTime;
            if (timer >= distance)
            {
                timer = 0.0f;
                BhapticsLibrary.PlayMotors((int) Bhaptics.SDK2.PositionType.Vest, motors: motors, 50);
            }
        }*/
        //BhapticsLibrary.PlayMotors((int) Bhaptics.SDK2.PositionType.Vest, motors: testMotors, 50);
        BhapticsLibrary.PlayMotors((int) Bhaptics.SDK2.PositionType.Vest, motors: motors, 100);

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("collision enter");
            Array.Clear(motors, 0, motors.Length);
            Vector3 direction = (new Vector3(other.transform.position.x,
                this.transform.position.y, other.transform.position.z) - this.transform.position);
            distance = Mathf.Abs(direction.magnitude) -1 ;
            timer = distance; 
            float intensity = 100 - 100 * distance;

            Debug.DrawRay(this.transform.position, direction, Color.red);
            
            RaycastHit[] hit = Physics.SphereCastAll(this.transform.position, 0.3f,direction, 10, sensorLayer, QueryTriggerInteraction.Collide); 
            for (int i = 0; i< hit.Length; i++)
            {
                Debug.Log(hit[i].transform.gameObject.name);
                foreach (var motor in hit[i].transform.gameObject.GetComponent<SensorData>().motors)
                {
                    motors[motor] = (int) intensity;
                }
                
            }
            
            /*float intensity = 100;// - 100 * Mathf.Abs((new Vector3(this.transform.position.x, 0.0f, this.transform.position.z) - new Vector3(other.transform.position.x, 0.0f, other.transform.position.z)).magnitude);
            int index = 4;
            motors[index] = (int) intensity;
            */
            
            //Debug.Log("collision enter");
            
            
            //BhapticsLibrary.PlayMotors((int) Bhaptics.SDK2.PositionType.Vest, motors: new int[16]{0,100,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, 1000);
            //BhapticsLibrary.PlayParam(BhapticsEvent.PET_BENCHMARK_FRONT, 0.5f, 1f, 0f, 0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Array.Clear(motors, 0, motors.Length);
            
            Vector3 direction = (new Vector3(other.transform.position.x,
                this.transform.position.y, other.transform.position.z) - this.transform.position);
            distance = Mathf.Abs(direction.magnitude) -1 ;
            float intensity = 100 - 100 * distance;

            Debug.DrawRay(this.transform.position, direction, Color.red);
            
            RaycastHit[] hit = Physics.SphereCastAll(this.transform.position, 0.3f,direction, 10, sensorLayer, QueryTriggerInteraction.Collide); 
            for (int i = 0; i< hit.Length; i++)
            {
                Debug.Log(hit[i].transform.gameObject.name);
                foreach (var motor in hit[i].transform.gameObject.GetComponent<SensorData>().motors)
                {
                    motors[motor] = (int) intensity;
                }
                
            }
            
            //BhapticsLibrary.PlayMotors((int) Bhaptics.SDK2.PositionType.Vest, motors: new int[16]{0,100,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, 1000);
            //BhapticsLibrary.PlayParam(BhapticsEvent.PET_BENCHMARK_FRONT, 0.5f, 1f, 0f, 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Array.Clear(motors, 0, motors.Length);
            //Debug.Log("collision end");
        }
    }
}
