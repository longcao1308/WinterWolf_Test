using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePooler : MonoBehaviour
{
    private Dictionary<string, LinkedList<GameObject>> _dictionary;
    private static SimplePooler _instance;
    public static SimplePooler instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("Pooling").AddComponent<SimplePooler>(); 
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _dictionary = new Dictionary<string, LinkedList<GameObject>>();
    }

    public GameObject Spawn(string name, GameObject objectToSpawn)
    {
        GameObject newObject;
        if (!_dictionary.ContainsKey(objectToSpawn.name))
        {
            LinkedList<GameObject> newStack = new LinkedList<GameObject>();
            newObject = Instantiate(objectToSpawn);
            newObject.name = name;
            newStack.AddLast(newObject);

            _dictionary.Add(name, newStack);
        }
        else
        {
            if (_dictionary[name].First.Value.activeSelf)
            {
                newObject = Instantiate(objectToSpawn);
                newObject.name = name;
                _dictionary[name].AddLast(newObject);
            }
            else
            {
                newObject = _dictionary[name].First.Value;
                _dictionary[name].Remove(newObject);
                _dictionary[name].AddLast(newObject);
                newObject.SetActive(true);
                newObject.transform.localScale = objectToSpawn.transform.localScale;
            }
        }

        return newObject;
    }
    public void Despawn(string name, GameObject objectToDespawn)
    {
        objectToDespawn.SetActive(false);
        _dictionary[name].Remove(objectToDespawn);
        _dictionary[name].AddFirst(objectToDespawn);
    }
}
