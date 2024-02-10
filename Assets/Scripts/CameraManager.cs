using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private bool switchCameras;

    [SerializeField] private GameObject playerSelectPanel;
    [SerializeField] private GameObject modeConfirmationPanel;

    // Target position and rotation for camera1
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 targetRotation;

    // Original position and rotation of camera1
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    // Smooth transition speed
    [SerializeField] private float transitionSpeed = 2f;

    private void Start()
    {
        modeConfirmationPanel.SetActive(false);

        // Store the original position and rotation of camera1
        originalPosition = camera1.transform.position;
        originalRotation = camera1.transform.rotation;
    }

    void Update()
    {
        if (switchCameras)
        {
            if (playerSelectPanel != null)
            {
                playerSelectPanel.SetActive(false);
                modeConfirmationPanel.SetActive(true);
            }

            // Smoothly interpolate the position and rotation of camera1 towards the target values
            camera1.transform.position = Vector3.Lerp(camera1.transform.position, targetPosition, Time.deltaTime * transitionSpeed);
            camera1.transform.rotation = Quaternion.Lerp(camera1.transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * transitionSpeed);
        }
        else
        {
            // Reset camera1 to its original position and rotation
            camera1.transform.position = Vector3.Lerp(camera1.transform.position, originalPosition, Time.deltaTime * transitionSpeed);
            camera1.transform.rotation = Quaternion.Lerp(camera1.transform.rotation, originalRotation, Time.deltaTime * transitionSpeed);

            if (playerSelectPanel != null)
            {
                playerSelectPanel.SetActive(true);
                modeConfirmationPanel.SetActive(false);
            }
        }
    }
}
