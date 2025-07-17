using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
public class cinemachineTemblarCamara : MonoBehaviour
{
    public static cinemachineTemblarCamara Instance;
    private CinemachineCamera cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin impulseCam;
    private float tiempoMovimiento;
    private float tiempoMovimientoTotal;
    private float intensidadInicial;

    void Awake()
    {
        Instance = this;
        cinemachineCamera = GetComponent<CinemachineCamera>();
        if (cinemachineCamera != null)
        {
            impulseCam = cinemachineCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        }


    }
    public void ShakeCam(float intensidad, float frecuencia, float tiempo)
    {
        impulseCam.AmplitudeGain = intensidad;
        impulseCam.FrequencyGain = frecuencia;

        intensidadInicial = intensidad;
        tiempoMovimiento = tiempo;
        tiempoMovimientoTotal = tiempo;

    }
    private void Update()
    {
        if (tiempoMovimiento > 0)
        {
            tiempoMovimiento -= Time.deltaTime;
            impulseCam.AmplitudeGain = Mathf.Lerp(intensidadInicial, 0, 1 - (tiempoMovimiento / tiempoMovimientoTotal));
        }
    }
}
