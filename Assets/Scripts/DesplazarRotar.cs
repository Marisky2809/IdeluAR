using UnityEngine;

public class DesplazarRotar : MonoBehaviour
{
    public float duracion = 1f;

    [Header("Offset desde posición actual")]
    public Vector3 desplazamientoOffset = Vector3.zero;

    [Header("Offset extra de rotación (Euler)")]
    public Vector3 rotacionOffsetEuler = Vector3.zero;

    private Vector3 posicionInicial;
    private Vector3 posicionObjetivo;

    private Quaternion rotacionInicial;
    private Quaternion rotacionObjetivo;

    private float tiempo = 0f;
    private bool interpolando = false;

    public void MoverAHacia(Vector3 nuevaPosicionBase, Quaternion nuevaRotacionBase)
    {
        posicionInicial = transform.position;
        posicionObjetivo = nuevaPosicionBase + desplazamientoOffset;

        rotacionInicial = transform.rotation;
        rotacionObjetivo = nuevaRotacionBase * Quaternion.Euler(rotacionOffsetEuler);

        tiempo = 0f;
        interpolando = true;
    }

    void Update()
    {
        if (!interpolando) return;

        tiempo += Time.deltaTime;
        float t = Mathf.Clamp01(tiempo / duracion);

        transform.position = Vector3.Lerp(posicionInicial, posicionObjetivo, t);
        transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionObjetivo, t);

        if (t >= 1f)
        {
            interpolando = false;
        }
    }
}
