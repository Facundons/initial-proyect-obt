using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{

    private string apiPath = $"http://worldtimeapi.org/api/timezone/America/Argentina/Tucuman";
    private UnityWebRequest request;
    private DateTimeApiModel result;
    public event EventHandler<DateTimeApiModel> onDateTimeRecieved;

    public void GetDateTimeFromApi()
    {
        StartCoroutine(GetDateTimeFromApiCoroutine());
    }

    IEnumerator GetDateTimeFromApiCoroutine()
    {
        request = UnityWebRequest.Get(apiPath);
        yield return request.SendWebRequest();
        result = JsonUtility.FromJson<DateTimeApiModel>(request.downloadHandler.text);
        onDateTimeRecieved?.Invoke(this,result);
    }



}
