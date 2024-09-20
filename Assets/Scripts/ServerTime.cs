using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ServerTime
{
    private string url;
    private int timezoneOffset;
    private DateTime time;

    public ServerTime(string url, int timezoneOffset = 0)
    {
        this.url = url;
        this.timezoneOffset = timezoneOffset;
    }

    public async UniTask<DateTime> GetCurrentTime()
    {
        await CheckGlobalTimeAsync();
        time = time.AddMilliseconds(-3);
        return time;
    }
    
    private async UniTask CheckGlobalTimeAsync()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            await request.SendWebRequest();

            // Проверяем на ошибки запроса
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching time from server: " + request.error);

                // Используем локальное системное время
                time = DateTime.Now.AddHours(timezoneOffset);
                Debug.Log("Using local system time: " + time.ToString("HH:mm:ss"));
                return;
            }

            // Извлекаем заголовок "Date" из ответа
            if (request.GetResponseHeaders().TryGetValue("Date", out string timeStr))
            {
                if (DateTime.TryParse(timeStr, out DateTime globDateTime))
                {
                    // Время в UTC
                    time = globDateTime.ToUniversalTime().AddHours(timezoneOffset);
                    Debug.Log(time.ToString("HH:mm:ss"));
                }
                else
                {
                    Debug.LogError("Failed to parse the date from the response.");
                }
            }
            else
            {
                Debug.LogError("Date header not found in the response.");
            }

            UniTask.Delay(1000);
        }
    }
}