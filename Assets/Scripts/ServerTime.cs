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
        return time;
    }
    
    private async UniTask CheckGlobalTimeAsync()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            await request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching time from server: " + request.error);
                
                time = DateTime.Now.AddHours(timezoneOffset);
                return;
            }
            
            if (request.GetResponseHeaders().TryGetValue("Date", out string timeStr))
            {
                if (DateTime.TryParse(timeStr, out DateTime globDateTime))
                {
                    time = globDateTime.ToUniversalTime().AddHours(timezoneOffset);
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

            await UniTask.Delay(1000);
        }
    }
}