// Attach this script to a GameObject called "ZoomManager"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZoomManager : MonoBehaviour
{


    public Camera mainCamera;
    public float zoomInSize = 2f;
    public float zoomOutSize = 5f;
    public float zoomSpeed = 5f;

    public Button zoomInBtn;
    public Button zoomOutBtn;

    private RectTransform selectedBranch;
    private Vector3 originalCamPosition;
    private float originalCamSize;
    public bool isZoomed = false;

    void Start()
    {
        zoomInBtn.gameObject.SetActive(false);
        zoomOutBtn.gameObject.SetActive(false);

        zoomInBtn.onClick.AddListener(ZoomIn);
        zoomOutBtn.onClick.AddListener(ZoomOut);

        originalCamPosition = mainCamera.transform.position;
        originalCamSize = mainCamera.orthographicSize;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ignore clicks on UI buttons
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    position = Input.mousePosition
                };

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (var result in results)
                {
                    if (result.gameObject.CompareTag("Branch"))
                    {
                        selectedBranch = result.gameObject.GetComponent<RectTransform>();
                        ShowZoomButtons();
                        return;
                    }

                    // If clicked on zoom buttons, ignore deselection
                    if (result.gameObject == zoomInBtn.gameObject || result.gameObject == zoomOutBtn.gameObject)
                    {
                        return;
                    }
                }
            }

            // If clicked elsewhere, deselect
            DeselectBranch();
        }
    }

    void ShowZoomButtons()
    {
        zoomInBtn.gameObject.SetActive(true);
        zoomOutBtn.gameObject.SetActive(true);
    }

    void DeselectBranch()
    {
        selectedBranch = null;
        zoomInBtn.gameObject.SetActive(false);
        zoomOutBtn.gameObject.SetActive(false);

       // if (isZoomed)
           // ZoomOut();
    }

    void ZoomIn()
    {
        if (selectedBranch == null) return;

        // Convert the UI position of selected branch to world space
        Vector3 worldPos = selectedBranch.position;
        Vector3 targetCamPos = new Vector3(worldPos.x, worldPos.y, originalCamPosition.z);

        StopAllCoroutines();
        StartCoroutine(SmoothZoom(targetCamPos, zoomInSize));
        isZoomed = true;
    }

    public void ZoomOut()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothZoom(originalCamPosition, originalCamSize));
        isZoomed = false;
    }

    IEnumerator SmoothZoom(Vector3 targetPos, float targetSize)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPos) > 0.01f ||
               Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.01f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, Time.deltaTime * zoomSpeed);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        mainCamera.transform.position = targetPos;
        mainCamera.orthographicSize = targetSize;
    }













    //public Camera mainCamera;
    //public Button zoomInButton;
    //public Button zoomOutButton;
    //public float zoomAmount = 2f;           // How much to zoom in
    //public float zoomSpeed = 5f;            // Zoom transition speed
    //public float minZoom = 3f;              // Minimum orthographic size
    //public float maxZoom = 10f;             // Maximum orthographic size

    //private Vector3 defaultCameraPos;
    //private float defaultZoom;

    //private bool isZoomedIn = false;
    //private Transform selectedBranch;

    //void Start()
    //{
    //    defaultCameraPos = mainCamera.transform.position;
    //    defaultZoom = mainCamera.orthographicSize;

    //    zoomInButton.onClick.AddListener(ZoomIn);
    //    zoomOutButton.onClick.AddListener(ZoomOut);

    //    zoomInButton.gameObject.SetActive(false);
    //    zoomOutButton.gameObject.SetActive(false);
    //}

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

    //        if (hit.collider != null && hit.collider.GetComponent<BranchZoomable>())
    //        {
    //            selectedBranch = hit.collider.transform;
    //            zoomInButton.gameObject.SetActive(!isZoomedIn);
    //            zoomOutButton.gameObject.SetActive(isZoomedIn);
    //        }
    //        else
    //        {
    //            ResetZoom();
    //        }
    //    }
    //}

    //void ZoomIn()
    //{
    //    if (selectedBranch == null || isZoomedIn) return;

    //    Vector3 targetPos = selectedBranch.position;
    //    targetPos.z = mainCamera.transform.position.z;

    //    StopAllCoroutines();
    //    StartCoroutine(SmoothZoom(targetPos, Mathf.Max(minZoom, mainCamera.orthographicSize / zoomAmount)));

    //    isZoomedIn = true;
    //    zoomInButton.gameObject.SetActive(false);
    //    zoomOutButton.gameObject.SetActive(true);
    //}

    //void ZoomOut()
    //{
    //    if (!isZoomedIn) return;

    //    StopAllCoroutines();
    //    StartCoroutine(SmoothZoom(defaultCameraPos, defaultZoom));

    //    isZoomedIn = false;
    //    zoomInButton.gameObject.SetActive(true);
    //    zoomOutButton.gameObject.SetActive(false);
    //}

    //void ResetZoom()
    //{
    //    selectedBranch = null;
    //    if (isZoomedIn)
    //    {
    //        ZoomOut();
    //    }
    //    else
    //    {
    //        zoomInButton.gameObject.SetActive(false);
    //        zoomOutButton.gameObject.SetActive(false);
    //    }
    //}

    //System.Collections.IEnumerator SmoothZoom(Vector3 targetPos, float targetZoom)
    //{
    //    while (Vector3.Distance(mainCamera.transform.position, targetPos) > 0.01f || Mathf.Abs(mainCamera.orthographicSize - targetZoom) > 0.01f)
    //    {
    //        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, Time.deltaTime * zoomSpeed);
    //        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    //        yield return null;
    //    }

    //    mainCamera.transform.position = targetPos;
    //    mainCamera.orthographicSize = targetZoom;
    //}
}
