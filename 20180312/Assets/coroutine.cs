using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coroutine : MonoBehaviour {


	// Use this for initialization
	void Start () {
        StartCoroutine(callFunc());
	}
	
    IEnumerator callFunc()
    {
        while (true)
        {
            Debug.Log("111");
            yield return new WaitForSeconds(5);//WaitForSeconds객체를 생성해서 반환
            Debug.Log("222");
        }
    }

	// Update is called once per frame
	void Update () {
        Debug.Log("333");
    }

}
