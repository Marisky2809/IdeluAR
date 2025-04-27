using UnityEngine;

public class Rotar : MonoBehaviour
{
    [Tooltip("Grados de rotación en cada eje (local)")]
    public Vector3 rotacionDeseada;

    [Tooltip("Duración de la rotación en segundos")]
    public float duracionRotacion = 1f;

    private bool rotando = false;

    void Start()
    {
        IniciarRotacion();
    }
    public void IniciarRotacion()
    {
        if (!rotando)
        {
            StartCoroutine(RotarEnTiempo());
        }
    }

    private System.Collections.IEnumerator RotarEnTiempo()
    {
        rotando = true;

        Quaternion rotacionInicial = transform.localRotation;
        Quaternion rotacionFinal = rotacionInicial * Quaternion.Euler(rotacionDeseada);

        float tiempo = 0f;

        while (tiempo < duracionRotacion)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracionRotacion);
            transform.localRotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, t);
            yield return null;
        }

        transform.localRotation = rotacionFinal;
        rotando = false;
    }
}
