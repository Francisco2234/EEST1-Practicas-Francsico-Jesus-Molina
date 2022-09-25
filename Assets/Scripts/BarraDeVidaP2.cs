using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaP2 : MonoBehaviour
{
    //Variables publicas:

    public Slider barraDeslizante2;

    /*
         ---------------------
        |FUNCIÓN SALUD MÁXIMA2|
         ---------------------
    */
    public void SaludMaxim2(int SaludMaxima)
    {
        //Asignandolé el parametro 'SaludMaxima' a 'barraDeslizante2.maxValue' y 'barraDeslizante2.value'.

        barraDeslizante2.maxValue = SaludMaxima;
        barraDeslizante2.value = SaludMaxima;
    }

    /*
         --------------
        |FUNCIÓN SALUD2|
         --------------
    */
    public void Salud2(int Salud)
    {
        //Asignandolé el parametro 'Salud2' a 'barraDeslizante2.maxValue'.

        barraDeslizante2.value = Salud;
    }
}