using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEnvironment
{
    private static GameEnvironment _instance;
    private List<GameObject> _checkPoints = new List<GameObject>();

    public List<GameObject> CheckPoints => _checkPoints;

    public static GameEnvironment Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEnvironment();
                _instance._checkPoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
                _instance._checkPoints = _instance._checkPoints.OrderBy(waypoint => waypoint.name).ToList();
            }

            return _instance;
        }
    }
}
