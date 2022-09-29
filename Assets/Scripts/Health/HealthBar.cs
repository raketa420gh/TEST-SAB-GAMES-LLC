using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] [Min(0)] private float updateSpeedSeconds = 0.25f;
    [SerializeField] private Health _health;

    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void OnEnable() => 
        _health.OnPercentChanged += OnPercentChanged;

    private void OnDisable() => 
        _health.OnPercentChanged -= OnPercentChanged;

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.Euler(new Vector3(45, 0, transform.rotation.z));;
    }

    private IEnumerator ChangeToCurrentPercent(float currentHealthPercent)
    {
        float preChangePercent = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount =
                Mathf.Lerp(preChangePercent, currentHealthPercent, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = currentHealthPercent;
    }

    private void OnPercentChanged(float currentHealthPercent) => 
        StartCoroutine(ChangeToCurrentPercent(currentHealthPercent));
}