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
        //GetData�� ���� ������ �����͸� �Ǽ��� �迭�� ������ �� �ִ�.
        //�Ǽ��� �迭 ���� > �����͸� �־��ش�.
        //�Ķ���� ( �Ǽ��� �迭, ������ġ)


        //�����Ͱ��� ����� ���ؾ� �Ѵ� ----why???----
        //�������� ������ -1~ 1������ �Ǽ��� �迭�̶� �׳� ����� ���ϰ� �ȴٸ� 0�� ���������.
        //�׷��� �迭�� ���� ���� �����Ͽ� ������ ��
        //�迭�� ũ�⸸ŭ �����ش�.
        //�׸��� ���� ���� �����ָ� ��.
        float sum = 0;

        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        //���� ���� Mathf.Sqrt�� ���ϸ� �ȴ�.
        rmsValue =Mathf.Sqrt(sum / samples.Length);
        rmsValue = rmsValue * modulate;
        rmsValue = Mathf.Clamp(rmsValue, 0, 100);
        resultValue = Mathf.RoundToInt(rmsValue);//�Ҽ��� �����ش�.
        
        //������ �־ �����Ǵ� ���� ���� �߶��ش�.
        if (resultValue < cutValue)
        {
            resultValue = 0;
        }

    }
}
