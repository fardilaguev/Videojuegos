using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyecto1 : MonoBehaviour
{
    [Range(20,20000)]
    public float Frecuencia;
    public float FrecuenciaMuestreo;
    public float TiempoSegundos = 2.0f;
    public float y, m1, m2, m3, b1, b2, b3;
    int TimeIndex = 0;
    AudioSource fuente;
    // Start is called before the first frame update
    void Start()
    {
        fuente = gameObject.AddComponent<AudioSource>();
        fuente.playOnAwake = false;
        fuente.spatialBlend = 0;
        fuente.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!fuente.isPlaying)
            {
                TimeIndex=0;
                fuente.Play();
                selector = 0;
                print("test");
            }

            else{
                fuente.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!fuente.isPlaying)
            {
                TimeIndex=0;
                fuente.Play();
                selector = 1;
                print("test");
            }

            else{
                fuente.Stop();
            }
        }

                if (Input.GetKeyDown(KeyCode.S))
        {
            if (!fuente.isPlaying)
            {
                TimeIndex=0;
                fuente.Play();
                selector = 2;
                print("test");
            }

            else{
                fuente.Stop();
            }
        }
    }

    int selector = 0;
    void OnAudioFilterRead(float[] data, int channels) {

        if (selector == 0)
        {
            for (int i=0; i < data.Length; i += channels){
                data[i] = CreateSeno(TimeIndex, Frecuencia, FrecuenciaMuestreo);

                if(channels==2)
                    data[i+1] = CreateSeno(TimeIndex,Frecuencia,FrecuenciaMuestreo);
                TimeIndex++;

                if (TimeIndex>=(FrecuenciaMuestreo*TiempoSegundos))
                    TimeIndex = 0;
            }
        }

        else if (selector == 1)
        {
            float Tm = FrecuenciaMuestreo / Frecuencia ;
            for (int i=0; i < data.Length; i += channels)
            {
                data[i] = CreateSquare(TimeIndex, Frecuencia, FrecuenciaMuestreo);

                if(channels==2)
                    data[i+1] = CreateSquare(TimeIndex,Frecuencia,FrecuenciaMuestreo);
                TimeIndex++;

                if  (TimeIndex >= (FrecuenciaMuestreo*TiempoSegundos) )
                    TimeIndex = 0;
            }
        }

        else if (selector == 2)
        {
            float Tm = Frecuencia / FrecuenciaMuestreo;
            for (int i=0; i < data.Length; i += channels)
            {
                data[i] = CreateTriangle(TimeIndex, Frecuencia, FrecuenciaMuestreo,Tm);

                if(channels==2)
                    data[i+1] = CreateTriangle(TimeIndex,Frecuencia,FrecuenciaMuestreo, Tm);
                TimeIndex++;

                if  (TimeIndex>= Tm)
                    TimeIndex = 0;
            }
        }

    }

    
    public float CreateSeno(int TimeIndex, float Frecuencia, float FrecuenciaMuestreo){
        return Mathf.Sin(2*Mathf.PI *Frecuencia * TimeIndex/ FrecuenciaMuestreo);
    }

    public float CreateSquare(int TimeIndex, float Frecuencia, float FrecuenciaMuestreo)
    {
        if (Mathf.Sin(2*Mathf.PI *Frecuencia * TimeIndex/ FrecuenciaMuestreo)> 0)
            return 1;
        else if(Mathf.Sin(2*Mathf.PI *Frecuencia * TimeIndex/ FrecuenciaMuestreo)< 0)
            return -1;
        else
            return 0;
    }
    
     public float CreateTriangle(int TimeIndex, float Frecuencia, float FrecuenciaMuestreo, float Tm)
     {
        //para hallar la pendiente de la primera recta
        float m1 = (1)/((Tm/4.0f));
        //para hallar la pendiente de la segunda recta
        float m2 = (-2)/((Tm*(3/4.0f))-((Tm/4.0f)));
        //para hallar la pendiente de la tercer recta
        float m3 = (1)/(Tm -((Tm*3)/4.0f));
        //para hallar el intercepto de la primera recta
        float b1 = 1 - (m1*(Tm/4));
        //para hallar el intercepto de la segunda recta
        float b2 = 1 - (m2*(Tm/4));
        //para hallar el intercepto de la tercer recta
        float b3 = - (m3*Tm);

        if(TimeIndex<(Tm/4.0f)) return (m1 * TimeIndex + b1);
        else if (TimeIndex>=(Tm/4)&& TimeIndex <= ((Tm * 3)/4)) return (m2 * TimeIndex + b2);
        else return m3 * (TimeIndex + b3);

     }
}

