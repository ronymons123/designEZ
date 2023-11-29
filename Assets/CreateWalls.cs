using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateWalls : MonoBehaviour
{
    bool creating, flag = true;
    public GameObject start;
    public GameObject end;
    Vector3 actualstart; 

    public GameObject wallprefab;
    public GameObject wall;
    float sum1=0, sum2=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        getInput();

    }

    void getInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            setEnd();
        }
        else
        {
            if (creating)
            {
                adjust();
            }

        }
    }

    void setStart()
    {
        creating = true;
        if (flag)
        {
            flag = false;
            start.transform.position = getWordPoint();
            actualstart= start.transform.position;
            Vector3 originalPosition = start.transform.position;
            sum1 = sum1 + originalPosition.x;
            sum2 = sum2 + originalPosition.y;
            Debug.Log(sum1);
        }

        else
        {
            start.transform.position = end.transform.position;
            Vector3 originalPosition = start.transform.position;
            sum1 = sum1 + originalPosition.x;
            sum2 = sum2 + originalPosition.y;
            Debug.Log(sum1);

        }
        wall = (GameObject)Instantiate(wallprefab, start.transform.position, Quaternion.identity);
    }
    void setEnd()
    {
        creating = false;
        end.transform.position = getWordPoint();
        if (Vector3.Distance(actualstart, end.transform.position) <= 1f)
        {
            end.transform.position = actualstart;
            adjustWall();
        }
    }
    void adjust()
    {
        end.transform.position = getWordPoint();
        adjustWall();
    }
    void adjustWall()
    {
        start.transform.LookAt(end.transform.position);
        end.transform.LookAt(start.transform.position);
        float distance = Vector3.Distance(start.transform.position, end.transform.position);
        wall.transform.position = start.transform.position + distance / 2 * start.transform.forward;
        wall.transform.rotation = Quaternion.Euler(0f, start.transform.rotation.eulerAngles.y, 0f);
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance);
    }
    Vector3 getWordPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}