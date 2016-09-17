﻿using UnityEngine;
using System.Collections;

public class MensajesCuriosos : MonoBehaviour {
		string[] mensajes;
		string mensaje;
		public int numeroMensaje;
		bool ver_mensaje;
		float tiempo;
	// Use this for initialization
	void Start () {
				gameObject.GetComponent<MeshRenderer> ().enabled = false;
				ver_mensaje = false;
				mensajes = new string[10];
				mensajes[0] = "Las hojas de palma boba son palmas los cuales crecen en zonas frías,\n son originarias de la región del Sumapaz ";
				mensajes[1] = "El barro es usado en nuestro pueblo como cemento \n para unir las distintas partes de la casa ";
				mensajes[2] = "Esta iglesia la han traído los españoles, donde \n nos enseñaran a practicar su religión católica.";
				mensajes[3] = "Sabias que el pago en monedas de oro como único \n intercambio llego con los españoles.";
				mensajes[4] = "Iglesia reconstruida en 1776, \n mostrando la construcción de la nueva ciudad.";
	}
	
	// Update is called once per frame
	void Update () {
				tiempo -= Time.deltaTime;
				if (tiempo < 0) {
						ver_mensaje = false;
				}
	}

		void OnGUI(){
				if (ver_mensaje) {
						GUIStyle style = new GUIStyle ();
						style.alignment = TextAnchor.MiddleCenter;
						style = GUI.skin.GetStyle ("Box");
						style.fontSize = (int)(20.0f);

						GUI.Box (new Rect (0, 3 * Screen.height / 4, Screen.width, Screen.height / 4), mensajes[numeroMensaje-1]);

				}
		}

		public void OnTriggerEnter (Collider colision)
		{
				if (colision.tag == "Player") {
						ver_mensaje = true;
						tiempo = 10;
				}
		}
}
