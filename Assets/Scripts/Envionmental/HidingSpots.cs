using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont
// Collates all of the hiding spot transforms within the arena.

public class HidingSpots : MonoBehaviour
{
    private List<Waypoints> hidingSpotsList = new List<Waypoints>();

    public List<Waypoints> GetHidingSpotsList() => hidingSpotsList;

    void Awake()
    {
        foreach (Transform environmentObject in this.transform)
        {
            foreach (Transform childChild in environmentObject)
            {
                if (childChild.CompareTag("HidingSpots"))
                {
                    Waypoints waypoint = new Waypoints(childChild.transform);
                    hidingSpotsList.Add(waypoint);
                }
            }
        }
    }
}
