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
    private float prevVertTime = 0f;
    private float prevHorizTime = 0f;

    // Update is called once per frame
    void Update()
    {
        float currentVert = Input.GetAxis("Vertical");
        if (currentVert != 0f && prevVertTime + Math.Abs(prevVert / 100.0) <= Time.time)
        {
            prevVert = currentVert;
            prevVertTime = Time.time;
            StartCoroutine(move("drive", currentVert));
        }
        float currentHoriz = Input.GetAxis("Horizontal");
        if (currentHoriz != 0f && prevHorizTime + Math.Abs(prevHoriz / 100.0) <= Time.time)
        {
            prevHoriz = currentHoriz;
            prevHorizTime = Time.time;
            StartCoroutine(move("turn", Input.GetAxis("Horizontal")));
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
