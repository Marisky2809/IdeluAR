using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiTracking : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabsToSpawn = new List<GameObject>();

    private ARTrackedImageManager _trackedImageManager;

    private Dictionary<string, GameObject> _artObjects;

    private void Start()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        if (_trackedImageManager == null) return;

        _trackedImageManager.trackablesChanged.AddListener(OnImagesTrackedChanged);
        _artObjects = new Dictionary<string, GameObject>();

        SetUpSceneElements();
    }

    private void OnDestroy()
    {
        _trackedImageManager.trackablesChanged.RemoveListener(OnImagesTrackedChanged);
    }

    private void SetUpSceneElements()
    {
        foreach(var prefab in prefabsToSpawn)
        {
            var arObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            arObject.name = prefab.name;
            arObject.gameObject.SetActive(false);
            _artObjects.Add(arObject.name, arObject);
        }
    }

    private void OnImagesTrackedChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            UpdateTrackedImages(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateTrackedImages(trackedImage);
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            UpdateTrackedImages(trackedImage.Value);
        }
    }

    private void UpdateTrackedImages(ARTrackedImage trackedImage)
    {
        if (trackedImage == null) return;

        string nombre = trackedImage.referenceImage.name;
        GameObject objeto = _artObjects[nombre];

        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            objeto.SetActive(false);
            return;
        }

        objeto.SetActive(true);

        // Aplicar movimiento suave
        DesplazarRotar interpolador = objeto.GetComponent<DesplazarRotar>();
        if (interpolador != null)
        {
            interpolador.MoverAHacia(trackedImage.transform.position, trackedImage.transform.rotation);
        }
        else
        {
            // fallback: movimiento inmediato
            objeto.transform.position = trackedImage.transform.position;
            objeto.transform.rotation = trackedImage.transform.rotation;
        }
    }
}
