using UnityEngine;

public class DelayDesplazamiento : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Vector3 desplazamientoFinal = Vector3.zero; // Desplazamiento a sumar
    public Vector3 rotacionFinalEuler = Vector3.zero;  // Rotación final en ángulos de Euler
    public float tiempoMovimiento = 2f;                // Tiempo total del movimiento
    public float delayInicio = 1f;                     // Tiempo de espera antes de empezar

    private Vector3 posicionInicial;
    private Vector3 posicionObjetivo;
    private Quaternion rotacionInicial;
    private Quaternion rotacionObjetivo;

    private float tiempoActual = 0f;
    private float contadorDelay = 0f;
    private bool movimientoIniciado = false;
    private bool movimientoCompleto = false;

    void Start()
    {
        posicionInicial = transform.position;
        posicionObjetivo = posicionInicial + desplazamientoFinal;

        rotacionInicial = transform.rotation;
        rotacionObjetivo = Quaternion.Euler(rotacionFinalEuler);
    }

    void Update()
    {
        if (movimientoCompleto) return;

        if (!movimientoIniciado)
        {
            contadorDelay += Time.deltaTime;
            if (contadorDelay >= delayInicio)
            {
                movimientoIniciado = true;
                tiempoActual = 0f; // reiniciamos el tiempo para el movimiento suave
            }
            return;
        }

        tiempoActual += Time.deltaTime;
        float t = Mathf.Clamp01(tiempoActual / tiempoMovimiento);

        // Movimiento y rotación suaves
        transform.position = Vector3.Slerp(posicionInicial, posicionObjetivo, t);
        transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionObjetivo, t);

        if (t >= 1f)
        {
            movimientoCompleto = true;
        }
    }
}
