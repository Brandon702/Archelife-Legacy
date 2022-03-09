using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float yRateOfChange = .6f;
    private float zRateOfChange = .2f;
    [SerializeField] private int zoomLevel = 0;
    private float xMoveStrength = 5f;
    private float zMoveStrength = 5f;
    private Vector3 cameraPos;

    private void Update()
    {
        //Zoom in
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && zoomLevel <= 23)
        {
            changeMoveStrength();
            GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y-yRateOfChange, transform.position.z+zRateOfChange);
            if(zoomLevel >= 16)
            {
                transform.Rotate(-5, 0, 0);
            }
            zoomLevel++;

            if (GetComponent<Transform>().position.z > 5.5f)
            {
                GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 5.5f);
            }
            else if (GetComponent<Transform>().position.z < -5f)
            {
                GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -5f);
            }
            else if (GetComponent<Transform>().position.x > 8f)
            {
                GetComponent<Transform>().position = new Vector3(8f, transform.position.y, transform.position.z);
            }
            else if (GetComponent<Transform>().position.x < -8f)
            {
                GetComponent<Transform>().position = new Vector3(-8f, transform.position.y, transform.position.z);
            }

        }
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && zoomLevel >= 1)
        {
            changeMoveStrength();
            GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y+yRateOfChange, transform.position.z-zRateOfChange);
            if (zoomLevel > 16)
            {
                transform.Rotate(5, 0, 0);
            }

            zoomLevel--;

            switch(zoomLevel)
            {
                case int n when (n <= 2):
                    if (GetComponent<Transform>().position.z > 1.5f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 1.5f);
                    }
                    else if (GetComponent<Transform>().position.z < -1.5f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -1.5f);
                    }
                    if(GetComponent<Transform>().position.x > 2.7f)
                    {
                        GetComponent<Transform>().position = new Vector3(2.7f, transform.position.y, transform.position.z);
                    }
                    else if (GetComponent<Transform>().position.x < -2.7f)
                    {
                        GetComponent<Transform>().position = new Vector3(-2.7f, transform.position.y, transform.position.z);
                    }
                    break;
                case int n when (n <= 4 && n > 2):
                    if (GetComponent<Transform>().position.z > 2f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 2f);
                    }
                    else if (GetComponent<Transform>().position.z < -2f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -2f);
                    }
                    if (GetComponent<Transform>().position.x > 3.2f)
                    {
                        GetComponent<Transform>().position = new Vector3(3.2f, transform.position.y, transform.position.z);
                    }
                    else if (GetComponent<Transform>().position.x < -3.2f)
                    {
                        GetComponent<Transform>().position = new Vector3(-3.2f, transform.position.y, transform.position.z);
                    }
                    break;
                case int n when (n <= 9 && n > 4):
                    if (GetComponent<Transform>().position.z > 3f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 3f);
                    }
                    else if (GetComponent<Transform>().position.z < -3f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -3f);
                    }
                    if (GetComponent<Transform>().position.x > 4.4f)
                    {
                        GetComponent<Transform>().position = new Vector3(4.4f, transform.position.y, transform.position.z);
                    }
                    else if (GetComponent<Transform>().position.x < -4.4f)
                    {
                        GetComponent<Transform>().position = new Vector3(-4.4f, transform.position.y, transform.position.z);
                    }
                    break;
                case int n when (n <= 14 && n > 9):
                    if (GetComponent<Transform>().position.z > 3.5f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 3.5f);
                    }
                    else if (GetComponent<Transform>().position.z < -3.5f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -3.5f);
                    }
                    if (GetComponent<Transform>().position.x > 5.7f)
                    {
                        GetComponent<Transform>().position = new Vector3(5.7f, transform.position.y, transform.position.z);
                    }
                    else if (GetComponent<Transform>().position.x < -5.7f)
                    {
                        GetComponent<Transform>().position = new Vector3(-5.7f, transform.position.y, transform.position.z);
                    }
                    break;
                case int n when (n <= 16 && n > 14):
                    if (GetComponent<Transform>().position.z > 4f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 4f);
                    }
                    else if (GetComponent<Transform>().position.z < -4f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -4f);
                    }
                    if (GetComponent<Transform>().position.x > 6.2f)
                    {
                        GetComponent<Transform>().position = new Vector3(6.2f, transform.position.y, transform.position.z);
                    }
                    else if (GetComponent<Transform>().position.x < -6.2f)
                    {
                        GetComponent<Transform>().position = new Vector3(-6.2f, transform.position.y, transform.position.z);
                    }
                    break;
                case int n when (n <= 21 && n > 16):
                    if (GetComponent<Transform>().position.z > 5f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 5f);
                    }
                    else if (GetComponent<Transform>().position.z < -5f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -5f);
                    }
                    if (GetComponent<Transform>().position.x > 7.4f)
                    {
                        GetComponent<Transform>().position = new Vector3(7.4f, transform.position.y, transform.position.z);
                    }
                    else if (GetComponent<Transform>().position.x < -7.4f)
                    {
                        GetComponent<Transform>().position = new Vector3(-7.4f, transform.position.y, transform.position.z);
                    }
                    break;
                case int n when (n <= 24 && n > 21):
                    if (GetComponent<Transform>().position.z > 5.5f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 5.5f);
                    }
                    else if (GetComponent<Transform>().position.z < -5.5f)
                    {
                        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -5.5f);
                    }
                    if (GetComponent<Transform>().position.x > 8.1f)
                    {
                        GetComponent<Transform>().position = new Vector3(8.1f, transform.position.y, transform.position.z);
                    }
                    else if (GetComponent<Transform>().position.x < -8.1f)
                    {
                        GetComponent<Transform>().position = new Vector3(-8.1f, transform.position.y, transform.position.z);
                    }
                    break;
                default:
                    break;
            }
                
            

        }

        //Keyboard movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            float val = 1.2f;
            val *= (zoomLevel / 8f) + 1;
            val -= val * 2;
            moveUp(val);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            float val = 1.2f;
            val *= (zoomLevel / 8f) + 1;
            
            moveDown(val);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            float val = 2.2f;
            val *= (zoomLevel / 9f) + 1;

            moveLeft(val);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            float val = 2.2f;
            val *= (zoomLevel / 9f) + 1;
            val -= val * 2;
            moveRight(val);
        }
    }

    //Moving
    private void moveUp(float limit)
    {
        if(limit < GetComponent<Transform>().position.z)
        {
            GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (zMoveStrength * Time.deltaTime));
        }
    }

    private void moveDown(float limit)
    {
        if (limit > GetComponent<Transform>().position.z)
        {
            GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (zMoveStrength * Time.deltaTime));
        }
    }
    private void moveLeft(float limit)
    {
        if (limit > GetComponent<Transform>().position.x)
        {
            GetComponent<Transform>().position = new Vector3(transform.position.x + (xMoveStrength * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
    private void moveRight(float limit)
    {
        if (limit < GetComponent<Transform>().position.x)
        {
            GetComponent<Transform>().position = new Vector3(transform.position.x - (xMoveStrength * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }

    //Movement Speed/Power
    private void changeMoveStrength()
    {
        float val = 5;

        val /= (zoomLevel/4) + 1.4f;


        changeMoveStrength(val);
    }

    private void changeMoveStrength(float strength)
    {
        zMoveStrength = strength;
        xMoveStrength = strength * 1.3f;
    }
}
