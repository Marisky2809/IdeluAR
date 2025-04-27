using System.Collections.Generic;
using UnityEngine;

public class PasoMovimiento
{
    public Vector3 desplazamiento;     // Movimiento relativo
    public Vector3 rotacionEuler;      // Rotación relativa
    public float tiempo = 1f;          // Tiempo del paso
}

public class VariosMovimientos : MonoBehaviour
{
    [Header("Configuración Inicial")]
    public Vector3 offsetInicial = Vector3.zero;

    [Header("Listas de pasos (mismo número de elementos)")]
    public List<Vector3> desplazamientos = new List<Vector3>();
    public List<Vector3> rotacionesEuler = new List<Vector3>();
    public List<float> tiempos = new List<float>();

    private int pasoActual = 0;
    private float tiempoActual = 0f;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private Vector3 posicionObjetivo;
    private Quaternion rotacionObjetivo;
    private bool enMovimiento = false;

    void Start()
    {
        // Aplicar offset inicial
        transform.position += offsetInicial;
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;

        if (EsValido(pasoActual))
        {
            PrepararPaso(pasoActual);
            enMovimiento = true;
        }
        else
        {
            Debug.LogWarning("Listas vacías o con tamaños desiguales.");
        }
    }

    void Update()
    {
        if (!enMovimiento || !EsValido(pasoActual)) return;

        tiempoActual += Time.deltaTime;
        float t = Mathf.Clamp01(tiempoActual / tiempos[pasoActual]);

        transform.position = Vector3.Slerp(posicionInicial, posicionObjetivo, t);
        transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionObjetivo, t);

        if (t >= 1f)
        {
            pasoActual++;
            if (EsValido(pasoActual))
            {
                PrepararPaso(pasoActual);
            }
            else
            {
                enMovimiento = false;
            }
        }
    }

    void PrepararPaso(int index)
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;

        posicionObjetivo = posicionInicial + desplazamientos[index];
        rotacionObjetivo = Quaternion.Euler(rotacionesEuler[index]) * rotacionInicial;

        tiempoActual = 0f;
    }

    bool EsValido(int index)
    {
        return index < desplazamientos.Count &&
               index < rotacionesEuler.Count &&
               index < tiempos.Count;
    }
}