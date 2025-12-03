using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlataform : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float movementSpeed;
    private Transform currentPoint;
    private Transform nextPoint;
    private bool isOnReverse;
    private int nextIndex = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = waypoints[0].position;
        currentPoint = this.transform;
        isOnReverse = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length >= 2)
        {
            MovePlataform();

            if (Vector2.Distance(currentPoint.position, nextPoint.position) < 0.1f)
            {
                FetchNextPlataformWayPoint();
            }

        }
        else
        {
            Debug.LogError("In order to move a plataform you need 2 waypoints");
        }
    }

    private void FetchNextPlataformWayPoint()
    {
        int tempIndex = nextIndex;
        if (!isOnReverse)
        {
            tempIndex++;
            nextIndex = tempIndex >= waypoints.Length ? ActiveReverse(-1) : tempIndex;
        }
        else
        {
            tempIndex--;
            nextIndex = tempIndex < 0 ? ActiveReverse(+1) : tempIndex;
        }
    }

    private void MovePlataform()
    {
        nextPoint = waypoints[nextIndex];
        this.transform.position = Vector2.MoveTowards(currentPoint.position, nextPoint.position, movementSpeed * Time.deltaTime);
    }

    private int ActiveReverse(int nextIndexValue)
    {
        isOnReverse = !isOnReverse;
        return nextIndex += nextIndexValue;
    }
}
