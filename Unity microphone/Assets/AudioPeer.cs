using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPeer : MonoBehaviour
{
    public AudioClip aud;
    int sampleRate = 44100;
    private float[] samples;

    public float rmsValue;
    public  float modulate;
    public int resultValue;
    public int cutValue;
    private void Start()
    {
        samples = new float[sampleRate];
        aud = Microphone.Start(Microphone.devices[0].ToString(), true, 1, sampleRate);
    }

    private void Update()
    {
        aud.GetData(samples, 0);
        //GetData로 현재 녹음된 데이터를 실수형 배열로 가져올 수 있다.
        //실수형 배열 선언 > 데이터를 넣어준다.
        //파라메터 ( 실수형 배열, 시작위치)


        //데이터값의 평균을 구해야 한다 ----why???----
        //데이터의 방향이 -1~ 1까지의 실수형 배열이라 그냥 평균을 구하게 된다면 0에 가까워진다.
        //그래서 배열의 값을 전부 제곱하여 더해준 뒤
        //배열의 크기만큼 나눠준다.
        //그리고 제곱 근을 구해주면 됨.
        float sum = 0;

        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        //제곱 근은 Mathf.Sqrt로 구하면 된다.
        rmsValue =Mathf.Sqrt(sum / samples.Length);
        rmsValue = rmsValue * modulate;
        rmsValue = Mathf.Clamp(rmsValue, 0, 100);
        resultValue = Mathf.RoundToInt(rmsValue);//소수점 버려준다.
        
        //가만히 있어도 오버되는 변수 값을 잘라준다.
        if (resultValue < cutValue)
        {
            resultValue = 0;
        }

    }
}
