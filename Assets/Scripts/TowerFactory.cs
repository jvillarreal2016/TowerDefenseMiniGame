using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] Transform towerParentTransform;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        print(towerQueue.Count);
        int numTowers = towerQueue.Count;

        if (numTowers < towerLimit)
        {
            InstantiateTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }

    }

    private void InstantiateTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform.transform;
        baseWaypoint.isPlaceable = false;

        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newBasewaypoint)
    {
        var oldTower = towerQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true; //to free up the block
        newBasewaypoint.isPlaceable = false;

        oldTower.baseWaypoint = newBasewaypoint;

        oldTower.transform.position = newBasewaypoint.transform.position;

        towerQueue.Enqueue(oldTower);
        Debug.Log("Max towers reached");
    }
}