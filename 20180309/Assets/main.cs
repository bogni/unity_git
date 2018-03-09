using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {

        Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

        MakeObj("Pyramid");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    
    public void MakeObj(string objName)
    {
        Instantiate(Resources.Load("Prefabs/" + objName), new Vector3(0, 0, 0), Quaternion.identity);

        GameObject targetObject = GameObject.Find(objName+"(Clone)");

        //Debug.Log("CNT="+ targetObject.transform.childCount);

        //int childCnt = 0;
        //childCnt = targetObject.transform.childCount;
        //if (childCnt > 0)
        //{
        //    for (int i = 0; i < childCnt; i++)
        //    {
        //        Debug.Log("name=" + targetObject.transform.GetComponentInChildren("cube"));
        //    }
        //}

        var transformList = GetComponentsInChildren<Transform>();

        if (transformList != null)
        {
            foreach (var child in transformList)
            {
                Debug.Log("name=" + child.name);
            }
        }

    }

}
