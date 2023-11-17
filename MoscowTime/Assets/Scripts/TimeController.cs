using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class TimeController : MonoBehaviour
{
    public void GetMoscowTime()
    {
        StartCoroutine(GetTime());

        IEnumerator GetTime()
        {
            UnityWebRequest www = UnityWebRequest.Get("http://www.unn.ru/time/");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string pattern = @"<div style=""width: 99%; height:150px; position: absolute; top:50%; margin-top: -75px; text-align:center; font-size:150px; font-weight:bold"" id=""servertime"" >([\d:\s]+)<\/div>";
                Match match = Regex.Match(www.downloadHandler.text, pattern);

                if (match.Success)
                {

                    string timeString = match.Groups[1].Value;
                    string jsCode = "alert('"+"Время по МСК : " + timeString + "');";
                    Application.OpenURL("javascript:" + jsCode);
                }
                else
                {
                    Debug.Log("Строка времени не найдена в HTML.");
                }
            }
        }
    }
}

