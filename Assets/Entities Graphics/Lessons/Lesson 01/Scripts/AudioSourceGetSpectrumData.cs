using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceGetSpectrumData : MonoBehaviour
{
    public float[] spectrum;
    public float maxValue;
    void Start()
    {

    }

    void Update()
    {
        spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 0; i < spectrum.Length; i++)
        {
            if (spectrum[i] > maxValue)
            {
                maxValue = spectrum[i];
            }
        }
    }
}