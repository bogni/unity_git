using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bogni : MonoBehaviour {

    public float speed = 0;
    public string isStat = ""; // O:Open / C:Close
    public string deviceStat = ""; // E:eject / I:insert
    
    Quaternion cam_temp;
    GameObject ServerOBJ;

    // Use this for initialization
    void Start () {
        cam_temp = Camera.main.transform.localRotation;
        //Debug.Log("cam_temp="+ cam_temp);

        // 초기 구동시 카메라앵글 조정
        Camera.main.transform.position = new Vector3(9.37f, 39.27f, -45.87f);
        //Camera.main.transform.position = new Vector3(8.73f, 36.7f, -49.42f);
        Camera.main.transform.localRotation = cam_temp;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject Door = GameObject.Find("SRV_DOOR_1");

        // Debug.Log("isStat=" + isStat + " / localRotation.y=" + Door.transform.localRotation.y);
        // Debug.Log("--------------------------------");

        doWork();

        if (isStat == "O" && Door.transform.localRotation.y < 0.85f)
        {
            OpenDoor();
        }

        else if (isStat == "C" || Door.transform.localRotation.y > 0.89f)
        {
            //isStat = "C";
            CloseDoor();
        }


        if (deviceStat != "")
        {
            if (deviceStat == "E" && ServerOBJ.transform.position.z < -0.03f)
            {
                ejectDevice();
            }

            else if (deviceStat == "I" || ServerOBJ.transform.position.z > -8.0f)
            {
                deviceStat = "I";
                insertDevice();
            }
        }
    }

    void doWork()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayValue = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(rayValue, out hit, 5000))
            {
                // 렉 선택시
                if (hit.collider.name == "GlassPlat0")
                {
                    GameObject Door = GameObject.Find("SRV_DOOR_1");

                    //Debug.Log("isStat=" + isStat + " /deviceStat=" + deviceStat);
                    //Debug.Log(Door.transform.localRotation.y);

                    if (Door.transform.localRotation.y < 0.86f)
                    {
                        isStat = "O";
                    }
                    else if (Door.transform.localRotation.y > 0.84f)
                    {
                        isStat = "C";
                    }
                }
                // 개별장비 선택시
                else if (hit.collider.name.Substring(0, 5) == "front")
                {
                    string numb = hit.collider.name.Substring(6, 3);
                    ServerOBJ = GameObject.Find("SVR_" + numb);

                    // -0.036 ~ -8.0
                    if (ServerOBJ.transform.position.z < -0.03f && ServerOBJ.transform.position.z > -8.0f)
                    {
                        deviceStat = "E";
                    }
                    else
                    {
                        deviceStat = "I";
                    }
                }
            }
        }
    }


    #region 서버문 열기
    void OpenDoor()
    {
        speed = 3.5f;
        GameObject Door = GameObject.Find("SRV_DOOR_1");
        Door.transform.Rotate(new Vector3(0, 1, 0), speed);

        // 회전멈추기 및 Y축 보정
        //if (Door.transform.localRotation.y > 1.0f)
        if (Door.transform.localRotation.y > 0.86f)
        {
            isStat = "";
            Door.transform.Rotate(new Vector3(0, 1, 0), -speed);
            Door.transform.eulerAngles = new Vector3(0, 299, 0);
            
        }
    }
    #endregion


    #region 서버문 닫기
    void CloseDoor()
    {
        // 문닫기전 나와있는 장비여부 체크
        if (deviceStat == "EE")
        {
            insertDevice();
        }
        else if (deviceStat == "")
        {
            speed = 3.5f;
            GameObject Door = GameObject.Find("SRV_DOOR_1");
            Door.transform.Rotate(new Vector3(0, -1, 0), speed);

            // 회전멈추기
            if (Door.transform.localRotation.y < 0.01f)
            {
                isStat = "";
                Door.transform.Rotate(new Vector3(0, -1, 0), -speed);
                Door.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }
    #endregion


    #region 장비꺼내기
    void ejectDevice()
    {
        speed = 15;

        // -0.036 ~ -8.0
        if (ServerOBJ.transform.position.z > -8.0f)
        {
            ServerOBJ.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            deviceStat = "EE";
        }

    }
    #endregion


    #region 장비넣기
    void insertDevice()
    {
        speed = 15;

        // -0.036 ~ -8.0
        if (ServerOBJ.transform.position.z < -0.045f)
        {
            ServerOBJ.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else
        {
            // Z축 보정
            ServerOBJ.transform.position = new Vector3(ServerOBJ.transform.position.x, ServerOBJ.transform.position.y, -0.036f);
            deviceStat = "";
        }
        
        
    }
    #endregion

}
