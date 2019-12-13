using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class Car : MonoBehaviour
{

    string IP = "192.168.1.2";
    string port = "5000";
    private float prevVert = 0f;
    private float prevHoriz = 0f;

    // Update is called once per frame
    void Update()
    {
        float currentVert = Input.GetAxis("Vertical");
        if (currentVert != prevVert) {
            prevVert = currentVert;
            StartCoroutine(move("drive", currentVert));
        }
        float currentHoriz = Input.GetAxis("Horizontal");
        if (currentHoriz != prevHoriz)
        {
            prevHoriz = currentHoriz;
            StartCoroutine(move("turn", currentHoriz));
        }

    }

    IEnumerator move(string type, float value)
    {
        WWWForm form = new WWWForm();
        form.AddField("value", value + "");
        UnityWebRequest www = UnityWebRequest.Post("http://" + IP + ":" + port + "/" + type, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
    }

}
