using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections;

public class Car : MonoBehaviour
{

    string ip = "192.168.137.242";
    string port = "5000";
    private float prevVert = 0f;
    private float prevHoriz = 0f;
    private bool sendingDrive= false;
    private bool sendingTurn = false;
    [SerializeField]
    private InputField ipInput;

    // Update is called once per frame
    void Update()
    {
        float currentVert = Input.GetAxis("Vertical");
        if (currentVert != prevVert && !sendingDrive)
        {
            prevVert = currentVert;
            sendingDrive = true;
            StartCoroutine(move("drive", currentVert));
        }
        float currentHoriz = Input.GetAxis("Horizontal");
        if (currentHoriz != prevHoriz && !sendingTurn)
        {
            prevHoriz = currentHoriz;
            sendingTurn = true;
            StartCoroutine(move("turn", currentHoriz));
        }

    }

    IEnumerator move(string type, float value)
    {
        WWWForm form = new WWWForm();
        form.AddField("value", value + "");
        UnityWebRequest www = UnityWebRequest.Post("http://" + ip + ":" + port + "/" + type, form);
        yield return www.SendWebRequest();

        if (type.Equals("drive"))
        {
            sendingDrive = false;
        }
        else
        {
            sendingTurn = false;
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
    }

    public void updateIp()
    {
        this.ip = ipInput.text; 
    }

}
