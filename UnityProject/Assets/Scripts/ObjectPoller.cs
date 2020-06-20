using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoller : MonoBehaviour
{
    public GameObject[] prefabs;
    Dictionary<string, List<GameObject>> poolDict;
    public int bufferAmount;
   
    // Start is called before the first frame update
    void Awake()
    {
        poolDict = new Dictionary<string, List<GameObject>>();
        for (int i = 0; i < prefabs.Length; ++i)
        {
            var gol = new List<GameObject>();
            var prefab = prefabs[i];
            for (int j = 0; j < bufferAmount; ++j)
            {
                var go = Instantiate(prefab);
                go.SetActive(false);
                go.transform.position = Vector3.zero;
                gol.Add(go);
            }
            poolDict.Add(prefab.name, gol);
        }
    }

    public bool isSpawned(string key)
    {
        var gol = poolDict[key];
        if (gol == null)
            return false;

        var go = gol.FirstOrDefault<GameObject>(g => g.activeInHierarchy == true);

        if (go != null)
            return true;
        return false;
    }

    public GameObject Spawn(string key, Vector3 pos)
    {
        var gol = poolDict[key];
        var go = gol.FirstOrDefault<GameObject>(g => g.activeInHierarchy == false);

        if (go != null)
        {
            go.SetActive(true);

            go.transform.position = pos;
            go.transform.rotation = Quaternion.identity;
            


            var r = go.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
            }
            return go;
        }
        else
        {
            var prefab = prefabs.FirstOrDefault<GameObject>(g => g.name == key);
            go = Instantiate(prefab);
            go.transform.position = pos;
            go.transform.rotation = Quaternion.identity;
            var r = go.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
            }
            gol.Add(go);
            return go;
        }
    }
    public GameObject Delete(string key)
    {
        var gol = poolDict[key];
        var go = gol.FirstOrDefault<GameObject>(g => g.activeInHierarchy == true);

        if (go != null)
        {
            foreach (Transform child in go.transform)
            {
                child.gameObject.SetActive(false);
            }
            go.SetActive(false);
            
            return go;
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
